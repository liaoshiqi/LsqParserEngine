namespace LsqParserEngine.Entity
{
    using System;
    using System.Collections.Generic;

    internal class MAXFunction : Function
    {
        public MAXFunction(IOrganization Organization) : base(Organization)
        {
        }

        public override string Describe(List<string> Parameters)
        {
            return string.Format("取 {0} 和 {1} 中的最大值", Parameters[0], Parameters[1]);
        }

        public override FunctionHelper GetHelper()
        {
            return new FunctionHelper(this.FunctionName, "取两个值中的最大值 ", this.FunctionName + "({V1}, {V2})",
                new Parameter[] { 
                    new Parameter("V1", "如果为V1&gt;=V2，则返回V1",new DataLogicType[] { DataLogicType.Bool, DataLogicType.DateTime, DataLogicType.Double, DataLogicType.Int, DataLogicType.Long, DataLogicType.TimeSpan }),
                    new Parameter("V2", "如果为V2&gt;=V1，则返回V2", new DataLogicType[] { DataLogicType.Bool, DataLogicType.DateTime, DataLogicType.Double, DataLogicType.Int, DataLogicType.Long, DataLogicType.TimeSpan }) },
                new Parameter("Return", "最大值", new DataLogicType[] { DataLogicType.Bool, DataLogicType.DateTime, DataLogicType.Double, DataLogicType.Int, DataLogicType.Long, DataLogicType.ShortString, DataLogicType.SingleParticipant, DataLogicType.MultiParticipant, DataLogicType.String, DataLogicType.TimeSpan }));
        }

        public override Variant Parse(FunctionExpression fd, IVariableTable Variables)
        {
            if (fd.Count < 2)
            {
                throw new CalcException(string.Format("函数\"{0}\"的参数个数不足", "MAX"));
            }
            Variant variant = fd[0];
            Variant variant2 = fd[1];
            if (variant.Type != variant2.Type)
            {
                throw new CalcException(string.Format("函数\"{0}\"的第二个参数类型不匹配", "MAX"));
            }
            switch (variant.Type)
            {
                case VariantType.Bool:
                    return new Variant((variant != null) || ((bool)variant2));

                case VariantType.Int:
                    return new Variant(Math.Max((int)variant, (int)variant2));

                case VariantType.Double:
                    return new Variant(Math.Max((double)variant, (double)variant2));

                case VariantType.DateTime:
                    return new Variant((variant > variant2) ? ((DateTime)variant) : ((DateTime)variant2));

                case VariantType.TimeSpan:
                    return new Variant((variant > variant2) ? ((TimeSpan)variant) : ((TimeSpan)variant2));
            }
            throw new CalcException(string.Format("函数\"{0}\"不支持类型:{1}", "MAX", variant.Type));
        }

        public override string FunctionName
        {
            get
            {
                return "MAX";
            }
        }
    }
}

