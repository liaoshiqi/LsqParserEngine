using System;

namespace LsqParserEngine.Entity
{
    internal class OperatorParser
    {
        /// <summary>
        /// 是否是操作符号
        /// </summary>
        /// <param name="s"></param>
        /// <param name="isFirstOperator"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Operator IsOperator(string s, bool isFirstOperator, ref int pos)
        {
            Operator nop = Operator.Nop;
            if ((s[pos] == Convert.ToChar(0x20)) || (s[pos] == Convert.ToChar(160)))
            {
                pos++;
                return Operator.Blank;
            }
            char ch = s[pos];
            if (ch <= '/')
            {
                switch (ch)
                {
                    case '\t':
                    case '\n':
                    case ' ':
                        pos++;
                        return Operator.Blank;

                    case '!':
                        if ((s.Length <= (pos + 1)) || (s[pos + 1] != '='))
                        {
                            nop = Operator.Not;
                            pos++;
                            return nop;
                        }
                        nop = Operator.NtEq;
                        pos += 2;
                        return nop;

                    case '"':
                    case '#':
                    case '$':
                    case '%':
                    case '\'':
                    case ',':
                    case '.':
                        return nop;

                    case '&':
                        if ((s.Length <= (pos + 1)) || (s[pos + 1] != '&'))
                        {
                            throw new CalcException(" 不能支持操作符\" &\"");
                        }
                        nop = Operator.And;
                        pos += 2;
                        return nop;

                    case '(':
                        nop = Operator.LeftPar;
                        pos++;
                        return nop;

                    case ')':
                        nop = Operator.RightPar;
                        pos++;
                        return nop;

                    case '*':
                        nop = Operator.Mul;
                        pos++;
                        return nop;

                    case '+':
                        if (isFirstOperator)
                        {
                            nop = Operator.UnPlus;
                        }
                        else
                        {
                            nop = Operator.Plus;
                        }
                        pos++;
                        return nop;

                    case '-':
                        if (!isFirstOperator)
                        {
                            nop = Operator.Minus;
                        }
                        else
                        {
                            nop = Operator.UnMinus;
                        }
                        goto Label_0110;

                    case '/':
                        nop = Operator.Div;
                        pos++;
                        return nop;
                }
                return nop;
            }
            switch (ch)
            {
                case '<':
                    if ((s.Length <= (pos + 1)) || (s[pos + 1] != '='))
                    {
                        if ((s.Length > (pos + 1)) && (s[pos + 1] == '>'))
                        {
                            nop = Operator.NtEq;
                            pos += 2;
                            return nop;
                        }
                        nop = Operator.Ls;
                        pos++;
                        return nop;
                    }
                    nop = Operator.LsEq;
                    pos += 2;
                    return nop;

                case '=':
                    if (s.Length <= (pos + 1))
                    {
                        throw new CalcException("不能支持操作符\"=\"");
                    }
                    if (s[pos + 1] != '=')
                    {
                        nop = Operator.Set;
                        pos++;
                        return nop;
                    }
                    nop = Operator.Eq;
                    pos += 2;
                    return nop;

                case '>':
                    if ((s.Length <= (pos + 1)) || (s[pos + 1] != '='))
                    {
                        nop = Operator.Gr;
                        pos++;
                        return nop;
                    }
                    nop = Operator.GrEq;
                    pos += 2;
                    return nop;

                case '|':
                    if ((s.Length <= (pos + 1)) || (s[pos + 1] != '|'))
                    {
                        throw new CalcException("不能支持操作符\"|\"");
                    }
                    nop = Operator.Or;
                    pos += 2;
                    return nop;

                default:
                    return nop;
            }
        Label_0110:
            pos++;
            return nop;
        }
    }
}

