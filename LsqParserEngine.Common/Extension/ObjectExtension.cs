using Newtonsoft.Json;

namespace LsqParserEngine.Common
{
    /// <summary>
    /// 对象的常用扩展方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 转为对应的实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="str">json</param>
        /// <returns>反序列化后的对象</returns>
        public static T ToEntityFromJson<T>(this string str) where T : class
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// 序列化为json
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
