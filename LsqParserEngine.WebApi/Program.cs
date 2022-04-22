using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LsqParserEngine.WebApi
{
    /// <summary>
    /// �������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ����վ������
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureLogging((context, logBuilder) =>
            {
                logBuilder.AddFilter("System", LogLevel.Warning);
                logBuilder.AddFilter("Microsoft", LogLevel.Warning);
                logBuilder.AddLog4Net(); //ע��log4net
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseUrls("http://*:5000")
                .UseStartup<Startup>();
            });
    }
}
