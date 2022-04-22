using System.Collections.Generic;


namespace LsqParserEngine.Entity
{
    /// <summary>
    /// IIF函数
    /// </summary>
    internal class IIFFunction : Function
    {
        public IIFFunction(IOrganization organization) : base(organization)
        {
        }

        public override string Describe(List<string> parameters)
        {
            return string.Format("如果 {0}；那么返回 {1}；否则返回 {2}", parameters[0], parameters[1], parameters[2]);
        }

        public override FunctionHelper GetHelper()
        {
            return new FunctionHelper(this.FunctionName, this.FunctionName, this.FunctionName + "({B}, {V1}, {V2})", new Parameter[] { new Parameter("B", "一个True/False的表达式或者值", new DataLogicType[] { DataLogicType.Bool }), new Parameter("V1", "如果为True，则返回V1", new DataLogicType[] { DataLogicType.Bool, DataLogicType.DateTime, DataLogicType.Double, DataLogicType.Int, DataLogicType.Long, DataLogicType.ShortString, DataLogicType.SingleParticipant, DataLogicType.MultiParticipant, DataLogicType.String, DataLogicType.TimeSpan }), new Parameter("V2", "如果为False，则返回V2", new DataLogicType[] { DataLogicType.Bool, DataLogicType.DateTime, DataLogicType.Double, DataLogicType.Int, DataLogicType.Long, DataLogicType.ShortString, DataLogicType.SingleParticipant, DataLogicType.MultiParticipant, DataLogicType.String, DataLogicType.TimeSpan }) }, new Parameter("Return", "如果为True，则返回V1，否则返回V2", new DataLogicType[] { DataLogicType.Bool, DataLogicType.DateTime, DataLogicType.Double, DataLogicType.Int, DataLogicType.Long, DataLogicType.ShortString, DataLogicType.SingleParticipant, DataLogicType.MultiParticipant, DataLogicType.String, DataLogicType.TimeSpan }));
        }

        public override Variant Parse(FunctionExpression fd, IVariableTable variables)
        {
            if (fd.Count < 3)
            {
                throw new CalcException(string.Format("函数\"IIF\"的参数个数不足", "IIF"));
            }
            if (fd[0].Value.ToString().ToLower() == "true")
            {
                return fd[1];
            }
            return fd[2];
        }

        public override string FunctionName
        {
            get
            {
                return "IIF";
            }
        }
    }
}

