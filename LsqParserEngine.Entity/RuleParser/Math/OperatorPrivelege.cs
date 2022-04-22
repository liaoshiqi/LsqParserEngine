namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 操作符优先级
    /// </summary>
    internal class OperatorPrivelege
    {
        public static int GetPrivelege(Operator op)
        {
            switch (op)
            {
                case Operator.Plus:
                case Operator.Minus:
                    return 5;

                case Operator.Mul:
                case Operator.Div:
                    return 6;

                case Operator.UnMinus:
                case Operator.UnPlus:
                case Operator.Not:
                    return 7;

                case Operator.Eq:
                case Operator.Gr:
                case Operator.Ls:
                case Operator.GrEq:
                case Operator.LsEq:
                case Operator.NtEq:
                    return 4;

                case Operator.LeftPar:
                    return 0;

                case Operator.RightPar:
                    return 1;

                case Operator.And:
                    return 3;

                case Operator.Or:
                    return 2;

                case Operator.Set:
                    return -1;
            }
            return 7;
        }
    }
}

