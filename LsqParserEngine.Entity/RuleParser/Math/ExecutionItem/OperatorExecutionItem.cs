using System;
using System.Collections;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 操作符(+ - * / = > < && || 等)解析器
    /// </summary>
    [Serializable]
    internal class OperatorExecutionItem : ExecutionItem
    {
        private Operator _itemOperator;

        public OperatorExecutionItem(Operator op)
        {
            base.Text = op.ToString();
            this._itemOperator = op;
        }

        public override Variant Calculate(FunctionFactory functionFactory, CalcStack calcStack, IVariableTable variables)
        {
            Operator itemOperator = this._itemOperator;
            switch (itemOperator)
            {
                case Operator.UnMinus:
                case Operator.UnPlus:
                case Operator.Not:
                case Operator.Nop:
                    if (calcStack.Count < 1)
                    {
                        throw new CalcException(string.Format("计算符{0}后缺少表达式", itemOperator.ToString()));
                    }
                    break;

                default:
                    if (calcStack.Count < 2)
                    {
                        throw new CalcException(string.Format("计算符{0}后缺少表达式", itemOperator.ToString()));
                    }
                    break;
            }
            try
            {
                switch (itemOperator)
                {
                    case Operator.Plus:
                        {
                            Variant variant = calcStack.Pop();
                            Variant variant2 = calcStack.Pop();
                            if ((variant.Value is string[]) || (variant2.Value is string[]))
                            {
                                return new Variant(this.JoinArray(new object[] { variant.Value, variant2.Value }), VariantType.Array);
                            }
                            if (variant.Type == VariantType.Double && variant2.Type == VariantType.Double)
                            {
                                //double相加精度的问题。
                                int shot1 = variant.Value.ToString().Length - variant.Value.ToString().IndexOf('.') - 1;
                                int shot2 = variant2.Value.ToString().Length - variant2.Value.ToString().IndexOf('.') - 1;
                                int maxShot = Math.Max(shot1, shot2);
                                if (maxShot > 0)
                                {
                                    double mom = Math.Pow(10, maxShot);
                                    return new Variant((((double)variant * mom + (double)variant2 * mom) / mom), VariantType.Double);
                                }
                            }

                            return (variant + variant2);
                        }
                    case Operator.Minus:
                        return (calcStack.Pop() - calcStack.Pop());

                    case Operator.Mul:
                        return (calcStack.Pop() * calcStack.Pop());

                    case Operator.Div:
                        return (calcStack.Pop() / calcStack.Pop());

                    case Operator.UnMinus:
                    case Operator.Not:
                        return -calcStack.Pop();

                    case Operator.UnPlus:
                        return null;

                    case Operator.Eq:
                        return (calcStack.Pop() == calcStack.Pop());

                    case Operator.Gr:
                        return (calcStack.Pop() > calcStack.Pop());

                    case Operator.Ls:
                        return (calcStack.Pop() < calcStack.Pop());

                    case Operator.GrEq:
                        return (calcStack.Pop() >= calcStack.Pop());

                    case Operator.LsEq:
                        return (calcStack.Pop() <= calcStack.Pop());

                    case Operator.NtEq:
                        return (calcStack.Pop() != calcStack.Pop());

                    case Operator.And:
                        return (calcStack.Pop() & calcStack.Pop());

                    case Operator.Or:
                        return (calcStack.Pop() | calcStack.Pop());

                    case Operator.Set:
                        {
                            Variant variant3 = calcStack.Pop();
                            Variant variant4 = calcStack.Pop();
                            if (!(!string.IsNullOrEmpty(variant4.Name) && variables.Contains(variant4.Name)))
                            {
                                throw new CalcException(string.Format("操作符\" = \"后的变量{0}不存在", variant4.Name));
                            }
                            variables[variant4.Name] = variant3.Value;
                            return variant3;
                        }
                }
            }
            catch (Exception ex)
            {
                return new Variant(false);
            }

            throw new CalcException("Operator " + itemOperator.ToString() + " is not supported yet");
        }

        public override string Describe(FunctionFactory functionFactory, Stack<string> calcStack)
        {
            Operator itemOperator = this._itemOperator;
            switch (itemOperator)
            {
                case Operator.Plus:
                    return string.Format("{1} + {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.Minus:
                    return string.Format("{1} - {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.Mul:
                    return string.Format("{1} * {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.Div:
                    return string.Format("{1} / {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.UnMinus:
                case Operator.Not:
                    return ("-" + calcStack.Pop());

                case Operator.UnPlus:
                    return null;

                case Operator.Eq:
                    return string.Format("{1} = {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.Gr:
                    return string.Format("{1} > {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.Ls:
                    return string.Format("{1} < {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.GrEq:
                    return string.Format("{1} >= {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.LsEq:
                    return string.Format("{1} <= {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.NtEq:
                    return string.Format("{1} != {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.LeftPar:
                    return ("(" + calcStack.Pop());

                case Operator.RightPar:
                    return (calcStack.Pop() + ")");

                case Operator.And:
                    return string.Format("{1} && {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.Or:
                    return string.Format("{1} || {0}", calcStack.Pop(), calcStack.Pop());

                case Operator.Set:
                    {
                        string str = calcStack.Pop();
                        string str2 = calcStack.Pop();
                        if (string.IsNullOrEmpty(str2))
                        {
                            throw new CalcException(string.Format(string.Format("操作符\" = \"后的变量{0}不存在", str2)));
                        }
                        return string.Format("将值 {0} 赋予给 {1}", str, str2);
                    }
            }
            throw new CalcException("Operator " + itemOperator.ToString() + " is not supported yet");
        }

        public override string DescribeAsHtml(FunctionFactory functionFactory, Stack<string> stack, IDictionary variables)
        {
            return base.DescribeAsHtml(functionFactory, stack, variables);
        }

        private string[] JoinArray(object[] values)
        {
            List<string> list = new List<string>();
            foreach (object obj2 in values)
            {
                if (obj2 != null)
                {
                    if (obj2 is string[])
                    {
                        foreach (string str in (string[])obj2)
                        {
                            list.Add(str);
                        }
                    }
                    else
                    {
                        list.Add(obj2.ToString());
                    }
                }
            }
            return list.ToArray();
        }

        public override ExecutionItemType Type
        {
            get
            {
                return ExecutionItemType.Operator;
            }
        }
    }
}

