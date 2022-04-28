using System.Collections.Generic;
using System.Data;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 执行sql
    /// </summary>
    internal class ExecuteSQLFunction : Function
    {
        public const string Name = "ExecuteSQL";

        public ExecuteSQLFunction(IOrganization organization) : base(organization)
        {
        }

        public override string Describe(List<string> parameters)
        {
            return "ExecuteSQL(\"sql语句\",参数1,...参数N)";
        }

        public override string DescribeAsHtml(List<string> parameters)
        {
            return base.DescribeAsHtml(parameters);
        }

        public override FunctionHelper GetHelper()
        {
            return new FunctionHelper("ExecuteSQL", "执行sql获取返回值,使用方式：ExecuteSQL(\"sql语句\",参数1,...参数N)",
                "ExecuteSQL(\"sql\",{Param1},...{ParamN})",
                new Parameter[] { new Parameter("sql语句", "sql语句", new DataLogicType[] { DataLogicType.String },false),
                new Parameter("Param", "业务方法参数", new DataLogicType[] { DataLogicType.String },false) },
                new Parameter("Return", "返回结果", new DataLogicType[] { DataLogicType.SingleParticipant, DataLogicType.MultiParticipant }));
        }

        public override Variant Parse(FunctionExpression desc, IVariableTable variables)
        {
            if ((desc != null) && (desc.Count >= 1))
            {
                string sql = desc[0].Value + string.Empty;
                string finalSql = sql;
                if (desc.Count > 1)
                {
                    object[] args = new object[desc.Count - 1];
                    for (int i = 0; i < desc.Count - 1; i++)
                    {
                        object arg = desc[i + 1].Value;
                        args[i] = arg;
                    }
                    finalSql = string.Format(sql, args);
                }
                if (!finalSql.Trim().ToLower().StartsWith("select")) return new Variant(new string[] { });   //必须以select开头的sql语句

                //EngineConfig engineConfig = base.Organization.Organization.GetQueryConfig();
                //DataTable dt = Database.ExecuteQuery(engineConfig.DBType, engineConfig.DBConnString, finalSql);
                DataTable dt = new DataTable();
                if (dt == null || dt.Rows.Count == 0) return new Variant("");
                object value = dt.Rows[0][0];
                string dataType = dt.Columns[0].DataType.Name.ToLower();
                if (dataType == "int" || dataType == "double" || dataType == "float" || dataType == "decimal")
                {
                    value = value == null ? 0 : value;
                    return new Variant(value);
                }
                else
                {
                    return new Variant(value + string.Empty);
                }
            }
            return new Variant("");
        }

        public override string FunctionName
        {
            get
            {
                return "ExecuteSQL";
            }
        }
    }
}

