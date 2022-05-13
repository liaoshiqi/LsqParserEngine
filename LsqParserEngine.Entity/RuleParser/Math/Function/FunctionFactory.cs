using LsqParserEngine.Common.Convertor;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 方法函数工厂
    /// </summary>
    public class FunctionFactory
    {
        private IOrganization _Organization;
        private Dictionary<string, Function> FunctionTable = new Dictionary<string, Function>();

        public FunctionFactory(IOrganization organization, Function[] customFunctions)
        {
            this._Organization = Organization;
            Function[] functionArray = ArrayConvertor<Function>.Add(Create(organization), customFunctions);
            foreach (Function function in functionArray)
            {
                if (string.IsNullOrEmpty(function.FunctionName))
                {
                    throw new CalcException("Function name is invalid.");
                }
                if (this.FunctionTable.ContainsKey(function.FunctionName.ToLower()))
                {
                    throw new CalcException("Function name \"" + function.FunctionName + "\" duplicated.");
                }
                this.FunctionTable.Add(function.FunctionName.ToLower(), function);
            }
        }
        /// <summary>
        /// 创建自定义方法函数
        /// </summary>
        /// <param name="organization">组织机构接口</param>
        /// <returns></returns>
        public static Function[] Create(IOrganization organization)
        {
            return new Function[] {
                new IIFFunction(organization),
                new ExecuteSQLFunction(organization),
                new DaysFunction(organization),
                new MAXFunction(organization),
                //new ContainFunction(organization),
            };
        }

        /// <summary>
        /// 获取方法的描述内容
        /// </summary>
        /// <param name="functionName">规则名</param>
        /// <param name="fd"></param>
        /// <returns></returns>
        public string Describe(string functionName, List<string> fd)
        {
            if (string.IsNullOrEmpty(functionName))
            {
                throw new CalcException("Function name is invalid.");
            }
            string key = functionName.ToLower();
            if (!this.FunctionTable.ContainsKey(key))
            {
                throw new CalcException("Can not support function \"" + functionName + "\".");
            }
            Function function = this.FunctionTable[key];
            return function.Describe(fd);
        }

        /// <summary>
        /// 获取方法的描述内容html
        /// </summary>
        /// <param name="functionName">规则名</param>
        /// <param name="fd"></param>
        /// <returns></returns>
        public string DescribeAsHtml(string functionName, List<string> fd)
        {
            if (string.IsNullOrEmpty(functionName))
            {
                throw new CalcException("Function name is invalid.");
            }
            string key = functionName.ToLower();
            if (!this.FunctionTable.ContainsKey(key))
            {
                throw new CalcException("Can not support function \"" + fd + "\".");
            }
            Function function = this.FunctionTable[key];
            return function.DescribeAsHtml(fd);
        }

        public Variant Parse(FunctionExpression fd, IVariableTable variables)
        {
            if (string.IsNullOrEmpty(fd.functionName))
            {
                throw new CalcException("Function name is invalid.");
            }
            string key = fd.functionName.ToLower().Trim();
            if (!this.FunctionTable.ContainsKey(key))
            {
                throw new CalcException("Can not support function \"" + fd.functionName + "\".");
            }
            Function function = this.FunctionTable[key];
            return function.Parse(fd, variables);
        }

        public IOrganization Organization
        {
            get
            {
                return this._Organization;
            }
        }
    }
}

