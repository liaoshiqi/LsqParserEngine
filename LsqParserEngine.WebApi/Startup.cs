using System;
using System.Linq;
using System.Reflection;
using Autofac;
using LsqParserEngine.Common;
using LsqParserEngine.Entity.Base;
using LsqParserEngine.WebApi.Config;
using LsqParserEngine.WebApi.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Quartz;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using LsqParserEngine.WebApi.Models;

namespace LsqParserEngine.WebApi
{
    /// <summary>
    /// 起步
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// webHost
        /// </summary>
        public IWebHostEnvironment Env { get; }


        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //依赖注入
            builder.RegisterModule(new AutofacModule());
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var appSetting = ConfigHelper.GetAppSetting();

            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                if (appSetting.Naming == Naming.CamelCase)
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
                else
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                }
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }); ;

            services.AddAutoMapper(typeof(MapperProfiles).Assembly);

            services.AddMvc(x =>
            {
                if (appSetting.CatchExcption)
                {
                    x.Filters.Add(typeof(GlobalValidationFilter));
                    x.Filters.Add(typeof(GlobalExceptionFilter)); //添加异常过滤
                }

                if (appSetting.CommonResult)
                {
                    x.Filters.Add(typeof(GlobalApiResultFilter));//返回统一的结果
                }
            });

            services.AddSqlsugarSetup(appSetting);
            services.AddScoped<UserAuthorzationAttribute>();

            //跨域设置
            var hosts = appSetting.AllowedHosts.Trim(',').Split(',');
            services.AddCors(options =>
            {
                options.AddPolicy("LimitRequests",
                    builder => builder
                    .WithOrigins(hosts)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    );
            });

            services.AddSingleton<IMemoryCache>(x =>
            {
                return new MemoryCache(new MemoryCacheOptions());
            });

            //添加性能监控
            if (appSetting.EnableMiniProfiler)
            {
                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler";
                    options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
                    options.PopupShowTimeWithChildren = true;
                });
            }

            if (appSetting.SwaggerUI != SwaggerUI.Disable)
            {
                //集成swagger文档
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "WebAPI", Version = "v1" });

                    #region swagger中集成简单的用户权限
                    options.AddSecurityDefinition("Token", new OpenApiSecurityScheme()
                    {
                        Description = "在这里输入Token，可以自动添加到header中",
                        Name = "Token",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Token"
                                }
                            },
                            new string[] { }
                        }
                    });
                    #endregion

                    options.CustomSchemaIds(type => type.FullName);
                    options.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}/LsqParserEngine.WebApi.xml");
                    options.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}/LsqParserEngine.Entity.xml");
                });
            }

            if (appSetting.EnableSchedulerTask)
            {
                services.AddQuartz(quartz =>
                {
                    quartz.UseMicrosoftDependencyInjectionScopedJobFactory(x =>
                    {
                        x.AllowDefaultConstructor = false;
                        x.CreateScope = true;
                    });
                });
            }
        }

        /// <summary>
        /// 配置HTTP相关
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var appSetting = ConfigHelper.GetAppSetting();

            switch (appSetting.SwaggerUI)
            {
                case SwaggerUI.Disable:
                    break;
                case SwaggerUI.Enable:
                    //启用swagger
                    app.UseSwagger();
                    break;
                case SwaggerUI.Dev:
                    if (env.IsDevelopment())
                    {
                        app.UseSwagger();
                    }
                    break;
            }

            //设置swaggerui集成性能监控
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI");
                //这里是配合MiniProfiler进行性能监控的
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("LsqParserEngine.WebApi.index.html");
                //这里是启动页面直接进入swagger文档
                c.RoutePrefix = "";
            });

            //添加log4
            loggerFactory.AddLog4Net();

            //跨域设置
            app.UseCors("LimitRequests");

            //启用静态文件
            app.UseStaticFiles();

            //启用路由
            app.UseRouting();

            //启用权限验证
            app.UseAuthorization();

            //启用性能监控器
            if (appSetting.EnableMiniProfiler)
            {
                app.UseMiniProfiler();
            }

            //设置路由为传统mvc样式
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
            });
        }
    }
}
