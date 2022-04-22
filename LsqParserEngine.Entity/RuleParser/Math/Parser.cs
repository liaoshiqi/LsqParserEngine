using System;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    public class Parser
    {
        private static char[] strDividers;

        static Parser()
        {
            char[] chArray = new char[] { '+', '-', '*', '/', '(', ')', '<', '>', '\n', '\t', '!', '&', '|', '\0', '\0' };
            chArray[13] = Convert.ToChar(0x20);
            chArray[14] = Convert.ToChar(160);
            strDividers = chArray;
        }

        private ExecutionItem GetOperand(string strSrc, ref int pos, bool validate, Dictionary<string, Function> functionDictionary, List<string> variableNames)
        {
            char c = strSrc[pos];
            switch (c)
            {
                case '"':
                    if (DateExecutionItem.CanParse(strSrc, pos))
                    {
                        return new DateExecutionItem(strSrc, ref pos);
                    }
                    return new String1ExecutionItem(strSrc, ref pos);

                case '\'':
                    return new String2ExecutionItem(strSrc, ref pos);

                case '{':
                    {
                        VariableExecutionItem item = new VariableExecutionItem(strSrc, ref pos);
                        if (validate && !((variableNames != null) && variableNames.Contains(item.VariableName)))
                        {
                            throw new CalcException(string.Format("变量名\"{0}\"不存在", item.VariableName));
                        }
                        return item;
                    }
            }
            if (char.IsNumber(c) || (c == '.'))
            {
                return new DigitExecutionItem(strSrc, ref pos);
            }
            if (((c == "U("[0]) && (strSrc.Length > (pos + 1))) && (strSrc[pos + 1] == "U("[1]))
            {
                return new UnitExecutionItem(strSrc, ref pos);
            }
            if (strSrc[pos] == '[')
            {
                return new ArrayExecutionItem(strSrc, ref pos);
            }
            int length = strSrc.IndexOfAny(strDividers, pos);
            if (length < 0)
            {
                length = strSrc.Length;
            }
            string functionName = strSrc.Substring(pos, length - pos);
            pos = length;
            if ((pos < strSrc.Length) && (strSrc[pos] == '('))
            {
                FunctionExecutionItem item2 = null;
                if (!validate)
                {
                    return new FunctionExecutionItem(functionName, strSrc, ref pos);
                }
                item2 = new FunctionExecutionItem(functionName, strSrc, ref pos, functionDictionary, variableNames, true);
                if (!((functionDictionary != null) && functionDictionary.ContainsKey(item2.FunctionName)))
                {
                    throw new CalcException(string.Format("函数\"{0}\"不存在", item2.FunctionName));
                }
                Function function = functionDictionary[item2.FunctionName];

                //if (!(function is ExecuteBizServiceFunction) && !(function is ExecuteSQLFunction))
                if (true)
                {
                    Parameter[] parameters = function.GetHelper().Parameters;
                    int num2 = parameters.Length;
                    if (num2 == item2.ParamExecutionQueues.Length)
                    {
                        return item2;
                    }
                    int num3 = 0;
                    foreach (Parameter parameter in parameters)
                    {
                        if (parameter.Required)
                        {
                            num3++;
                        }
                    }
                    if ((item2.ParamExecutionQueues.Length < num3) || (item2.ParamExecutionQueues.Length > parameters.Length))
                    {
                        throw new CalcException(string.Format("函数\"{0}\"的参数个数应为{1}", item2.FunctionName, num2));
                    }
                }
                return item2;
            }
            if (string.Compare(functionName, "true", true) == 0)
            {
                return new BoolExecutionItem(true);
            }
            if (string.Compare(functionName, "false", true) == 0)
            {
                return new BoolExecutionItem(false);
            }
            if (string.Compare(functionName, "null", true) != 0)
            {
                throw new CalcException("未识别的表达式\"" + functionName + "\"");
            }
            return new NullExecutionItem();
        }

        private void OperatorStackManager(OperatorStack scOp, Operator op, ExecutionQueue eqResult)
        {
            if (scOp.Count == 0)
            {
                scOp.Push(op);
            }
            else if (op == Operator.LeftPar)
            {
                scOp.Push(op);
            }
            else
            {
                Operator @operator;
                if (op == Operator.RightPar)
                {
                    while (scOp.Count > 0)
                    {
                        @operator = scOp.Pop();
                        if (@operator != Operator.LeftPar)
                        {
                            eqResult.Add(new OperatorExecutionItem(@operator));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    while (scOp.Count > 0)
                    {
                        @operator = scOp.Peek();
                        if (OperatorPrivelege.GetPrivelege(op) <= OperatorPrivelege.GetPrivelege(@operator))
                        {
                            eqResult.Add(new OperatorExecutionItem(scOp.Pop()));
                        }
                        else
                        {
                            break;
                        }
                    }
                    scOp.Push(op);
                }
            }
        }

        public ExecutionQueue Parse(string expression)
        {
            return this.Parse(expression, false, null, null);
        }

        public ExecutionQueue Parse(string expression, bool validate, Dictionary<string, Function> functionDictionary, List<string> variableNames)
        {
            OperatorStack scOp = new OperatorStack();
            ExecutionQueue eqResult = new ExecutionQueue();
            if (expression != null)
            {
                string s = expression.Trim();
                while (s.IndexOf(" (") > -1)
                {
                    s = s.Replace(" (", "(");
                }
                scOp.Clear();
                int pos = 0;
                bool isFirstOperator = true;
                int num2 = 0;
                int num3 = 0;
                bool flag2 = false;
                BoolMatchValue unspecified = BoolMatchValue.Unspecified;
                Operator nop = Operator.Nop;
                while (pos < s.Length)
                {
                    Operator @operator;
                    if ((@operator = OperatorParser.IsOperator(s, isFirstOperator, ref pos)) == Operator.Nop)
                    {
                        goto Label_0185;
                    }
                    if (@operator == Operator.Blank)
                    {
                        goto Label_01C8;
                    }
                    if (((unspecified == BoolMatchValue.True) && ((((nop != Operator.LeftPar) || ((@operator != Operator.UnPlus) && (@operator != Operator.UnMinus))) && (nop != Operator.RightPar)) && (@operator != Operator.LeftPar))) && ((nop != Operator.Not) && (nop != Operator.Blank)))
                    {
                        throw new CalcException("多个运算符堆积无意义");
                    }
                    this.OperatorStackManager(scOp, @operator, eqResult);
                    switch (@operator)
                    {
                        case Operator.RightPar:
                            num3++;
                            if (num3 > num2)
                            {
                                throw new CalcException("未预期的\")\"");
                            }
                            isFirstOperator = false;
                            goto Label_017E;

                        case Operator.LeftPar:
                            num2++;
                            break;
                    }
                    isFirstOperator = true;
                Label_017E:
                    unspecified = BoolMatchValue.True;
                    goto Label_01C8;
                Label_0185:
                    eqResult.Add(this.GetOperand(s, ref pos, validate, functionDictionary, variableNames));
                    flag2 = true;
                    if (unspecified == BoolMatchValue.False)
                    {
                        throw new CalcException("表达式间缺少运算符");
                    }
                    unspecified = BoolMatchValue.False;
                    isFirstOperator = false;
                Label_01C8:
                    nop = @operator;
                }
                if ((unspecified == BoolMatchValue.True) && (nop != Operator.RightPar))
                {
                    throw new CalcException("多(二)元运算符后缺少表达式");
                }
                while (scOp.Count > 0)
                {
                    eqResult.Add(new OperatorExecutionItem(scOp.Pop()));
                }
                if (num2 > num3)
                {
                    throw new CalcException("未关闭的\"(\"");
                }
                if (!flag2)
                {
                    throw new CalcException("表达式无意义");
                }
            }
            return eqResult;
        }
    }
}

