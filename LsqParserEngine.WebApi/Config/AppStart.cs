using Autofac;
using System;

namespace LsqParserEngine.WebApi.Config
{
    /// <summary>
    /// 初始化数据库
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
        public async void Start()
        {
            //_commonDAL.InitDataTable();
            //var appSetting = ConfigHelper.GetAppSetting();
            //if (appSetting.EnableSchedulerTask)
            //{
            //    await InitSchedulerTask();
            //}
            //if (appSetting.EnableRedisMq)
            //{
            //    /*
            //        消息订阅
            //        T:参数类型;
            //        channel:管道名称
            //    */
            //    _cacheHelper.RedisSubscribe<BuildFrameworkInput>("buildframework", (inputJson) =>
            //    {

            //    });
            //}
            Console.WriteLine("App Start...");
        }
    }
}
