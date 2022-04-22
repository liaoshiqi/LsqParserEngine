using System;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 日期类型解析器
    /// </summary>
    [Serializable]
    internal class DateExecutionItem : ExecutionItem
    {
        public DateExecutionItem(string strSrc, ref int pos)
        {
            base.Text = this.GetDate(strSrc, ref pos);
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            DateTime time;
            try
            {
                time = DateTime.Parse(base.Text);
            }
            catch
            {
                throw new CalcException("错误的日期格式");
            }
            return new Variant(time);
        }

        public static bool CanParse(string strSrc, int pos)
        {
            DateTime time;
            int num = pos;
            pos++;
            while ((pos < strSrc.Length) && (strSrc[pos] != '"'))
            {
                pos++;
            }
            pos++;
            return DateTime.TryParse(strSrc.Substring(num + 1, (pos - num) - 2), out time);
        }

        private string GetDate(string strSrc, ref int pos)
        {
            int num = pos;
            pos++;
            while ((pos < strSrc.Length) && (strSrc[pos] != '"'))
            {
                pos++;
            }
            pos++;
            return strSrc.Substring(num + 1, (pos - num) - 2);
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Date;
            }
        }
    }
}

