using System;

namespace LsqParserEngine.Entity
{
    [Serializable]
    internal class String1ExecutionItem : StringExecutionItem
    {
        public String1ExecutionItem(string strSrc, ref int pos)
        {
            base.Text = this.GetString(strSrc, ref pos);
        }

        private string GetString(string strSrc, ref int pos)
        {
            int num = pos;
            pos++;
            while ((pos < strSrc.Length) && (strSrc[pos] != '"'))
            {
                pos++;
            }
            pos++;
            if (pos > strSrc.Length)
            {
                throw new CalcException("引号\"未关闭");
            }
            return strSrc.Substring(num + 1, (pos - num) - 2);
        }
    }
}

