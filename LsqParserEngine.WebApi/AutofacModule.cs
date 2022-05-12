using Autofac;
using Microsoft.DotNet.PlatformAbstractions;
using System.IO;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Autofac.Extras.DynamicProxy;
using LsqParserEngine.WebApi.Config;

namespace LsqParserEngine.WebApi
{
    /// <summary>
    /// 依赖注入的配置模块
    /// </summary>
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsImplementedInterfaces();

            builder.RegisterType<AppStart>().As<IStartable>();

            var basePath = ApplicationEnvironment.ApplicationBasePath;

            //应用
            var appFilePath = Path.Combine(basePath, "LsqParserEngine.Application.dll");
            var assemblysApp = Assembly.LoadFrom(appFilePath).GetExportedTypes();
            builder.RegisterTypes(assemblysApp).AsImplementedInterfaces()
                      .InstancePerDependency()
                      .PropertiesAutowired()
                      .EnableInterfaceInterceptors();//引用Autofac.Extras.DynamicProxy;

            //注入仓储
            var domainFile = Path.Combine(basePath, "LsqParserEngine.Domain.dll");
            var assemblysDomain = Assembly.LoadFrom(domainFile);
            builder.RegisterAssemblyTypes(assemblysDomain)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency()
                .EnableInterfaceInterceptors();//引用Autofac.Extras.DynamicProxy;

            //支持属性注入（不用写在构造里了）
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }
    }
}
