using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsqParserEngine.Entity
{

    public abstract class Function
    {
        protected IOrganization Organization;

        public Function(IOrganization organization)
        {
            this.Organization = organization;
        }

        public abstract string Describe(List<string> parameters);
        public virtual string DescribeAsHtml(List<string> parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<a ExecutionType=\"Function\" Expression=\"" + this.FunctionName + "\" >" + this.FunctionName + "</a>");
            builder.Append("(");
            if ((parameters != null) && (parameters.Count > 0))
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append(parameters[i]);
                }
            }
            builder.Append(")");
            return builder.ToString();
        }

        protected string EnsureToId(string codeOrId)
        {
            return codeOrId;
        }

        protected string[] EnsureToIds(string[] codeOrIds)
        {
            List<string> list = new List<string>();
            if ((codeOrIds == null) || (codeOrIds.Length == 0))
            {
                return null;
            }
            foreach (string str in codeOrIds)
            {
                string str2 = this.EnsureToId(str);
                if (!string.IsNullOrEmpty(str2))
                {
                    list.Add(str2);
                }
            }
            return list.ToArray();
        }

        protected string[] EnsureToIds(object codeOrIds)
        {
            string str;
            if (codeOrIds == null)
            {
                return null;
            }
            if (codeOrIds is string)
            {
                str = this.EnsureToId((string)codeOrIds);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                return new string[] { str };
            }
            if (codeOrIds is Variant)
            {
                object obj2 = ((Variant)codeOrIds).Value;
                if (obj2 is Array)
                {
                    return this.EnsureToIds((string[])obj2);
                }
                str = this.EnsureToId(obj2 + string.Empty);
                if (str == null)
                {
                    return null;
                }
                return new string[] { str };
            }
            if (codeOrIds is string[])
            {
                return this.EnsureToIds((string[])codeOrIds);
            }

            if (!(codeOrIds is object[]))
            {
                throw new NotImplementedException();
            }

            return ((object[])codeOrIds).Select(t => t + "").ToArray<string>();
        }

        public abstract FunctionHelper GetHelper();
        protected string GetUnitDisplayName(string Unit)
        {
            return Unit;
        }

        public abstract Variant Parse(FunctionExpression desc, IVariableTable variables);

        public abstract string FunctionName { get; }
    }
}
