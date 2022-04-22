using System;
using System.Collections;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    [Serializable]
    public class FormulaParser
    {
        private string _formula;
        private ExecutionQueue _executionQueue = null;

        public FormulaParser(string formula)
        {
            this._formula = formula;
            this._executionQueue = new Parser().Parse(formula);
        }

        public Variant Calculate(IOrganization organization, Function[] customFunctions, IVariableTable variables)
        {
            if (this._executionQueue.Count == 0)
            {
                return new Variant(string.Empty);
            }
            Calculator calculator = new Calculator(organization, customFunctions, variables);
            return calculator.Calculate(this._executionQueue);
        }

        public string Describe(IOrganization organization,  Function[] customFunctions)
        {
            Describer describer = new Describer(organization, customFunctions);
            return describer.Describe(this._executionQueue);
        }

        public string DescribeAsHtml(IOrganization organization, Function[] customFunctions, IDictionary variables)
        {
            Describer describer = new Describer(organization, customFunctions, variables);
            return describer.DescribeAsHtml(this._executionQueue);
        }

        public string[] GetVariables()
        {
            ExecutionQueue executionQueue = this._executionQueue;
            List<string> variables = new List<string>();
            if (executionQueue != null)
            {
                foreach (ExecutionItem item in executionQueue)
                {
                    GetVariables(item, variables);
                }
            }
            return variables.ToArray();
        }

        private static void GetVariables(ExecutionItem item, List<string> variables)
        {
            switch (item.Type)
            {
                case ExecutionItemType.Variable:
                    {
                        string variableName = ((VariableExecutionItem)item).VariableName;
                        if (!(string.IsNullOrEmpty(variableName) || variables.Contains(variableName)))
                        {
                            variables.Add(variableName);
                        }
                        break;
                    }
                case ExecutionItemType.Function:
                    {
                        FunctionExecutionItem item2 = (FunctionExecutionItem)item;
                        ExecutionQueue[] paramExecutionQueues = item2.ParamExecutionQueues;
                        if (paramExecutionQueues != null)
                        {
                            foreach (ExecutionQueue queue in paramExecutionQueues)
                            {
                                foreach (ExecutionItem item3 in queue)
                                {
                                    GetVariables(item3, variables);
                                }
                            }
                        }
                        break;
                    }
            }
        }

        private static string[] SplitFormulas(string formula)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(formula))
            {
                int num = 0;
                int startIndex = 0;
                for (int i = 0; i < formula.Length; i++)
                {
                    char ch = formula[i];
                    if (ch == '"')
                    {
                        num++;
                    }
                    else if ((ch == ';') && ((num % 2) == 0))
                    {
                        list.Add(formula.Substring(startIndex, i - startIndex));
                        num = 0;
                        startIndex = i + 1;
                    }
                    if (i == (formula.Length - 1))
                    {
                        list.Add(formula.Substring(startIndex, formula.Length - startIndex));
                    }
                }
                return list.ToArray();
            }
            return new string[0];
        }

        public static bool Validate(string formula, Dictionary<string, Function> functionDictionary, List<string> variableNames, ref List<string> errors)
        {
            Parser parser = new Parser();
            if (string.IsNullOrEmpty(formula))
            {
                return true;
            }
            string[] strArray = SplitFormulas(formula);
            if ((strArray != null) && (strArray.Length > 0))
            {
                foreach (string str in strArray)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        try
                        {
                            bool validate = true;
                            parser.Parse(str, validate, functionDictionary, variableNames);
                        }
                        catch (Exception exception)
                        {
                            errors.Add(exception.Message);
                        }
                    }
                }
            }
            return (errors.Count == 0);
        }

        public string Formula
        {
            get
            {
                return this._formula;
            }
        }
    }
}
