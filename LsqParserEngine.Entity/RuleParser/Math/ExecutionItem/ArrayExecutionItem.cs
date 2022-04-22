using System.Text;
using System;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 数组解析器
    /// </summary>
    [Serializable]
    internal class ArrayExecutionItem : ExecutionItem
    {
        private List<ExecutionQueue> Elements;
        public const char End = ']';
        public const char Splittor = ',';
        public const char Start = '[';

        public ArrayExecutionItem(string strSrc, ref int pos) : this(strSrc, ref pos, null, null, false)
        {
        }

        public ArrayExecutionItem(string strSrc, ref int pos, Dictionary<string, Function> functionDictionary, List<string> variableNames, bool validateParameters)
        {
            this.Elements = new List<ExecutionQueue>();
            List<string> elements = this.GetElements(strSrc, ref pos);
            if (elements != null)
            {
                foreach (string str in elements)
                {
                    ExecutionQueue item = new Parser().Parse(str, validateParameters, functionDictionary, variableNames);
                    this.Elements.Add(item);
                }
            }
            base.Text = strSrc.Substring(0, pos);
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            List<object> list = new List<object>();
            foreach (ExecutionQueue queue in this.Elements)
            {
                Variant variant = new Calculator(functionFactory, variables).Calculate(queue);
                if (variant.Value != null)
                {
                    list.Add(variant.Value);
                }
            }
            return new Variant(list.ToArray());
        }

        public override string Describe(FunctionFactory functionFactory, Stack<string> stack)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ExecutionQueue queue in this.Elements)
            {
                Describer describer = new Describer(functionFactory);
                if (builder.Length > 0)
                {
                    builder.Append(',');
                }
                builder.Append(describer.Describe(queue));
            }
            char ch = '[';
            ch = ']';
            return (ch.ToString() + builder + ch.ToString());
        }

        private List<string> GetElements(string strSrc, ref int pos)
        {
            int startIndex = ++pos;
            bool flag = true;
            int num2 = 1;
            List<string> list = new List<string>();
        Label_00A7:;
            if (((strSrc.Length > pos) && (((strSrc[pos] != ']') || (num2 != 1)) || !flag)) && (strSrc.Length > pos))
            {
                char ch = strSrc[pos];
                if (ch != '"')
                {
                    switch (ch)
                    {
                        case '[':
                            if (flag)
                            {
                                num2++;
                            }
                            break;

                        case ']':
                            if (flag)
                            {
                                num2--;
                            }
                            break;

                        case ',':
                            if ((num2 == 1) && flag)
                            {
                                list.Add(strSrc.Substring(startIndex, pos - startIndex));
                                startIndex = pos + 1;
                            }
                            break;
                    }
                }
                else
                {
                    flag = !flag;
                }
                pos++;
                goto Label_00A7;
            }
            if ((pos >= strSrc.Length) || (strSrc[pos] != ']'))
            {
                throw new CalcException("数组缺少关闭符号\"]\"");
            }
            list.Add(strSrc.Substring(startIndex, pos - startIndex));
            pos++;
            return list;
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Array;
            }
        }
    }
}

