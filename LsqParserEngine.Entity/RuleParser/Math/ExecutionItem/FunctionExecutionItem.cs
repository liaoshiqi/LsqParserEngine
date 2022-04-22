using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace LsqParserEngine.Entity
{
    [Serializable]
    public class FunctionExecutionItem : ExecutionItem
    {
        private string _functionName;
        private List<ExecutionQueue> _paramExecutionQueues;

        public FunctionExecutionItem(string functionName, string strSrc, ref int pos) : this(functionName, strSrc, ref pos, null, null, false)
        {
        }

        public FunctionExecutionItem(string functionName, string strSrc, ref int pos, Dictionary<string, Function> functionDictionary, List<string> variableNames, bool validateParameters)
        {
            this._paramExecutionQueues = new List<ExecutionQueue>();
            this._functionName = functionName;
            StringCollection function = this.GetFunction(strSrc, ref pos);
            if ((strSrc.Length < pos) || (strSrc[pos - 1] != ')'))
            {
                throw new CalcException($"表达式[{this.FunctionName}]格式错误!");
            }
            if (((function == null) || (function.Count == 0)) || ((function.Count == 1) && ((function[0] == null) || string.IsNullOrEmpty(function[0].Trim()))))
            {
                this._paramExecutionQueues = new List<ExecutionQueue>();
            }
            else
            {
                int num = 0;
                foreach (string str in function)
                {
                    num++;
                    ExecutionQueue item = new Parser().Parse(str, validateParameters, functionDictionary, variableNames);
                    this._paramExecutionQueues.Add(item);
                }
            }
            base.Text = strSrc.Substring(0, pos);
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack stack, IVariableTable variables)
        {
            FunctionExpression fD = new FunctionExpression(this.FunctionName);
            foreach (ExecutionQueue queue in this._paramExecutionQueues)
            {
                Calculator calculator = new Calculator(functionFactory, variables);
                fD.Add(calculator.Calculate(queue));
            }
            return functionFactory.Parse(fD, variables);
        }

        public override string Describe(FunctionFactory functionFactory, Stack<string> stack)
        {
            List<string> fD = new List<string>();
            foreach (ExecutionQueue queue in this._paramExecutionQueues)
            {
                Describer describer = new Describer(functionFactory);
                fD.Add(describer.Describe(queue));
            }
            return functionFactory.Describe(this.FunctionName, fD);
        }

        public override string DescribeAsHtml(FunctionFactory functionFactory, Stack<string> stack, IDictionary variables)
        {
            List<string> fD = new List<string>();
            foreach (ExecutionQueue queue in this._paramExecutionQueues)
            {
                Describer describer = new Describer(functionFactory);
                fD.Add(describer.Describe(queue));
            }
            return functionFactory.DescribeAsHtml(this.FunctionName, fD);
        }

        private StringCollection GetFunction(string strSrc, ref int pos)
        {
            int startIndex = ++pos;
            bool flag = true;
            int num2 = 1;
            StringCollection strings = new StringCollection();
            while ((strSrc.Length > pos) && (((strSrc[pos] != ')') || (num2 != 1)) || !flag))
            {
                switch (strSrc[pos])
                {
                    case '(':
                    case '[':
                        if (flag)
                        {
                            num2++;
                        }
                        break;

                    case ')':
                    case ']':
                        if (flag)
                        {
                            num2--;
                        }
                        break;

                    case ',':
                        if ((num2 == 1) && flag)
                        {
                            strings.Add(strSrc.Substring(startIndex, pos - startIndex));
                            startIndex = pos + 1;
                        }
                        break;

                    case '"':
                        flag = !flag;
                        break;
                }
                pos++;
            }
            strings.Add(strSrc.Substring(startIndex, pos - startIndex));
            pos++;
            return strings;
        }

        public string FunctionName
        {
            get
            {
                return this._functionName;
            }
        }

        public ExecutionQueue[] ParamExecutionQueues
        {
            get
            {
                return this._paramExecutionQueues.ToArray();
            }
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Function;
            }
        }
    }
}

