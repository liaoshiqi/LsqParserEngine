using System;

namespace LsqParserEngine.Common
{
    /// <summary>
    /// 字符串相关扩展方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 转换成数字类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt32(this string s)
        {
            int.TryParse(s, out int result);
            return result;
        }

        /// <summary>
        /// 转为小数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string strValue)
        {
            float.TryParse(strValue, out float result);
            return result;
        }

        /// <summary>
        /// 转为小数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s)
        {
            decimal.TryParse(s, out decimal result);
            return result;
        }

        /// <summary>
        /// 转为小数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(this string s)
        {
            double.TryParse(s, out double result);
            return result;
        }

        /// <summary>
        /// 转为日期类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            DateTime.TryParse(s, out DateTime result);
            return result;
        }

    }
}
