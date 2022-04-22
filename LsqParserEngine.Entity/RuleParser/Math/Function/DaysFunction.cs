using System;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// �����ֶ��ظ���֤
    /// </summary>
    internal class DaysFunction : Function
    {
        public const string Name = "Days";

        public DaysFunction(IOrganization organization) : base(organization)
        {
        }

        public override string Describe(List<string> Parameters)
        {
            return string.Format("��ȡ�����������������", Parameters[0], Parameters[1]);
        }

        public override string DescribeAsHtml(List<string> Parameters)
        {
            if (Parameters.Count > 1)
            {
                return string.Format("<a>{0}</a>(<a>{1}</a>,<a>{2}</a>,<a>{3}</a>)", this.FunctionName, Parameters[0], Parameters[1]);
            }
            return base.DescribeAsHtml(Parameters);
        }

        public override FunctionHelper GetHelper()
        {
            return new FunctionHelper(this.FunctionName,
               "({Data1},{Data2})",
                this.FunctionName + "()", new Parameter[0],
                new Parameter("Return", "����",
                new DataLogicType[] { DataLogicType.Int }));

        }

        public override Variant Parse(FunctionExpression desc, IVariableTable variables)
        {
            if (desc == null || desc.Count < 1 || desc.Count > 2)
            {
                throw new CalcException("The function \"" + this.FunctionName + "\" must have two parameter.");
            }

            Variant variant = desc[0];
            Variant variant2 = desc[1];
            //if (variant.Type != variant2.Type)
            //{
            //    throw new CalcException(string.Format(Global.ResourceManager.GetString("DaysFunction_Mssg"), "Days"));
            //}
            DateTime def1 = DateTime.Now;
            if (variant.Value == null || string.IsNullOrEmpty(variant.Value.ToString()) || !DateTime.TryParse(variant.Value.ToString(), out def1))
            {
                return new Variant(-1);
            }

            DateTime def2 = DateTime.Now;
            if (variant2.Value == null || string.IsNullOrEmpty(variant2.Value.ToString()) || !DateTime.TryParse(variant2.Value.ToString(), out def2))
            {
                return new Variant(0);
            }

            DateTime t1 = Convert.ToDateTime(def1.ToShortDateString());
            DateTime t2 = Convert.ToDateTime(def2.ToShortDateString());
            TimeSpan ts = t1.Subtract(t2);
            double diffInDays = ts.TotalDays;
            return new Variant(diffInDays);
        }

        public override string FunctionName
        {
            get
            {
                return "Days";
            }
        }
    }
}
