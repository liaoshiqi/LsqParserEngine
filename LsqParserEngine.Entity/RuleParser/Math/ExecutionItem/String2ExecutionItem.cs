using System;

namespace LsqParserEngine.Entity
{
    [Serializable]
    internal class String2ExecutionItem : StringExecutionItem
    {
        public String2ExecutionItem(string strSrc, ref int pos)
        {
            base.Text = this.GetString2(strSrc, ref pos);
        }

        private string GetString2(string strSrc, ref int pos)
        {
            int num = pos;
            pos++;
            while ((pos < strSrc.Length) && (strSrc[pos] != '\''))
            {
                pos++;
            }
            pos++;
            return strSrc.Substring(num + 1, (pos - num) - 2);
        }
    }
}

