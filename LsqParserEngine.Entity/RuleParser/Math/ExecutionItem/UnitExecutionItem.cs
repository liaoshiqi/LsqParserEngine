using System;
using System.Collections;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    [Serializable]
    internal class UnitExecutionItem : ExecutionItem
    {
        public const char EndTag = ')';
        public const string StartTag = "U(";

        public UnitExecutionItem(string strSrc, ref int pos)
        {
            base.Text = this.GetString(strSrc, ref pos);
            if (string.IsNullOrEmpty(base.Text))
            {
                throw new CalcException("组织表达式\"U()\"的参数为空");
            }
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            string name = functionFactory.Organization.GetNameByCode(base.Text);
            //Unit userByCode = FunctionFactory.Organization.GetUnit(base.Text);
            //if (userByCode == null)
            //{
            //    userByCode = FunctionFactory.Organization.GetUserByCode(base.Text);
            //}
            //if (userByCode == null)
            //{
            //    return null;
            //}
            return new Variant(name, VariantType.Unit);
        }

        public override string DescribeAsHtml(FunctionFactory functionFactory, Stack<string> stack, IDictionary variables)
        {
            return ("U(" + base.Text + ')');
        }

        private string GetString(string strSrc, ref int pos)
        {
            int num = pos;
            pos += "U(".Length;
            while ((pos < strSrc.Length) && (strSrc[pos] != ')'))
            {
                pos++;
            }
            pos++;
            return strSrc.Substring(num + "U(".Length, ((pos - num) - "U(".Length) - 1);
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Unit;
            }
        }
    }
}

