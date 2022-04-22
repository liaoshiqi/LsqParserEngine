using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LsqParserEngine.Common
{
    public class EncryptionHelper
    {
        #region 简单加密（转16进制）
        /// <summary>
        /// 将Base64解码为字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64ToString(string str)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(str.ToString())).Replace("%2b", "+");
        }
        /// <summary>
        /// 将字符串转换为Base64
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string StringToBase64(string str)
        {
            //返回转后后的字符串
            return Convert.ToBase64String(Encoding.Default.GetBytes(str)).Replace("+", "%2b");
        }
        #endregion

        #region Des加密
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DesEncode(string data)
        {
            byte[] byKey = Encoding.ASCII.GetBytes("xiaojj#$");/*8位的公钥*/
            byte[] byIV = Encoding.ASCII.GetBytes("fly4516.");/*8位的密码*/
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DesDecode(string data)
        {
            byte[] byKey = Encoding.ASCII.GetBytes("xiaojj#$");/*8位的公钥*/
            byte[] byIV = Encoding.ASCII.GetBytes("fly4516.");/*8位的密码*/
            byte[] byEnc = Convert.FromBase64String(data);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
        #endregion

        #region MD5
        /// <summary>
        /// 获取加密后的MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5Str(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(str);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion

    }
}
