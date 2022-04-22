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
    /// ��
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ����
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// webHost
        /// </summary>
        public IWebHostEnvironment Env { get; }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        /// <summary>
        /// ע��
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //����ע��
            builder.RegisterModule(new AutofacModule());
        }

        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var appSetting = ConfigHelper.GetAppSetting();

            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                if (appSetting.Naming == Naming.CamelCase)
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
                else
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                }
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }); ;

            services.AddAutoMapper(typeof(MapperProfiles).Assembly);

            services.AddMvc(x =>
            {
                if (appSetting.CatchExcption)
                {
                    x.Filters.Add(typeof(GlobalValidationFilter));
                    x.Filters.Add(typeof(GlobalExceptionFilter)); //����쳣����
                }

                if (appSetting.CommonResult)
                {
                    x.Filters.Add(typeof(GlobalApiResultFilter));//����ͳһ�Ľ��
                }
            });

            services.AddSqlsugarSetup(appSetting);
            services.AddScoped<UserAuthorzationAttribute>();

            //��������
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

            //������ܼ��
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
                //����swagger�ĵ�
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "WebAPI", Version = "v1" });

                    #region swagger�м��ɼ򵥵��û�Ȩ��
                    options.AddSecurityDefinition("Token", new OpenApiSecurityScheme()
                    {
                        Description = "����������Token�������Զ���ӵ�header��",
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
        /// ����HTTP���
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
                    //����swagger
                    app.UseSwagger();
                    break;
                case SwaggerUI.Dev:
                    if (env.IsDevelopment())
                    {
                        app.UseSwagger();
                    }
                    break;
            }

            //����swaggerui�������ܼ��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI");
                //���������MiniProfiler�������ܼ�ص�
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("LsqParserEngine.WebApi.index.html");
                //����������ҳ��ֱ�ӽ���swagger�ĵ�
                c.RoutePrefix = "";
            });

            //���log4
            loggerFactory.AddLog4Net();

            //��������
            app.UseCors("LimitRequests");

            //���þ�̬�ļ�
            app.UseStaticFiles();

            //����·��
            app.UseRouting();

            //����Ȩ����֤
            app.UseAuthorization();

            //�������ܼ����
            if (appSetting.EnableMiniProfiler)
            {
                app.UseMiniProfiler();
            }

            //����·��Ϊ��ͳmvc��ʽ
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
            });
        }
    }
}
