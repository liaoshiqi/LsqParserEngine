using System;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 数字解析器
    /// </summary>
    [Serializable]
    internal class DigitExecutionItem : ExecutionItem
    {
        public DigitExecutionItem(string strSrc, ref int pos)
        {
            base.Text = this.GetNumber(strSrc, ref pos);
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            double num2;
            int result = 0;
            if (int.TryParse(base.Text, out result))
            {
                return new Variant(result);
            }
            if (double.TryParse(base.Text, out num2))
            {
                return new Variant(num2);
            }
            string[] strArray = base.Text.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if ((strArray != null) && (strArray.Length == 3))
            {
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                if ((int.TryParse(strArray[0], out num3) && int.TryParse(strArray[1], out num4)) && int.TryParse(strArray[2], out num5))
                {
                    return new Variant(new TimeSpan(num3, num4, num5));
                }
            }
            throw new CalcException("Bad digital format");
        }

        private string GetNumber(string strSrc, ref int pos)
        {
            int startIndex = pos;
            int num2 = 0;
            int num3 = 0;
            while ((pos < strSrc.Length) && ((char.IsDigit(strSrc[pos]) || (strSrc[pos] == '.')) || (strSrc[pos] == ':')))
            {
                if (strSrc[pos] == '.')
                {
                    num2++;
                }
                else if (strSrc[pos] == ':')
                {
                    num3++;
                }
                pos++;
            }
            if ((num2 > 1) || ((num3 != 0) && (num3 != 2)))
            {
                throw new CalcException(string.Format("表达式{0}不合法", strSrc.Substring(startIndex, pos - startIndex)));
            }
            return strSrc.Substring(startIndex, pos - startIndex);
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Number;
            }
        }
    }
}

