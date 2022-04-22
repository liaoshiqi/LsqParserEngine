using System;
using System.IO;

namespace LsqParserEngine.Common
{
    /// <summary>
    /// 用于读取customSetting.json
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获取自定义设置项
        /// </summary>
        /// <returns></returns>
        public static AppSetting GetAppSetting()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AppSetting>(File.ReadAllText(path));
        }
    }
}
