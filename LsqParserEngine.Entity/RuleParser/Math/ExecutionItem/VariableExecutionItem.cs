using System;
using System.Collections;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    [Serializable]
    internal class VariableExecutionItem : ExecutionItem
    {
        private string _variableName;

        public VariableExecutionItem(string strSrc, ref int pos)
        {
            this._variableName = this.GetVariable(strSrc, ref pos);
            if (string.IsNullOrEmpty(this.VariableName))
            {
                throw new CalcException($"{this.VariableName} is null");
            }
            base.Text = '{' + this._variableName + '}';
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            if (!variables.Contains(this.VariableName))
            {
                string[] strs = this.VariableName.Split('.');
                if (!variables.Contains(strs[0]))
                {
                    throw new CalcException("Bad variable name \"" + this.VariableName + "\".");
                }
            }
            return new Variant(variables[this.VariableName], this.VariableName, variables.GetVariableType(this.VariableName), variables.GetLogicType(this.VariableName));
        }

        public override string DescribeAsHtml(FunctionFactory functionFactory, Stack<string> stack, IDictionary variables)
        {
            string variableName = this.VariableName;
            if ((variables != null) && variables.Contains(this.VariableName))
            {
                variableName = variables[this.VariableName] + string.Empty;
            }
            return string.Concat(new object[] { "<a ExecutionType=\"Varible\" Expression=\"", '{', this.VariableName, '}', "\" >", variableName, "</a>" });
        }

        private string GetVariable(string strSrc, ref int pos)
        {
            int num = pos;
            int num2 = 1;
            pos++;
            while (pos < strSrc.Length)
            {
                switch (strSrc[pos])
                {
                    case '{':
                        num2++;
                        break;

                    case '}':
                        num2--;
                        break;
                }
                pos++;
                if (num2 == 0)
                {
                    break;
                }
            }
            if (strSrc[pos - 1] != '}')
            {
                throw new CalcException("变量符号{未关闭");
            }
            return strSrc.Substring(num + 1, (pos - num) - 2);
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Variable;
            }
        }

        public string VariableName
        {
            get
            {
                return this._variableName;
            }
        }
    }
}

