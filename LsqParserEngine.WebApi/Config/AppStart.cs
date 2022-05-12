using Autofac;
using System;

namespace LsqParserEngine.WebApi.Config
{
    /// <summary>
    /// 初始化
    /// </summary>
    public class AppStart : IStartable
    {
        /// <summary>
        /// 构造注入
        /// </summary>
        public AppStart()
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Start()
        {
            Console.WriteLine("App Start...");
        }
    }
}
