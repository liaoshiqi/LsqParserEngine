using System.Collections.Generic;
using System.Text.RegularExpressions;
using LsqParserEngine.Entity;
using LsqParserEngine.Common.Convertor;

namespace LsqParserEngine.Domain.RuleParser
{
    /// <summary>
    /// 规则表达式解析的领域
    /// </summary>
    public class RuleParserService : IRuleParserService
    {
        private IOrganization _organization;
        public RuleParserService(IOrganization organization)
        {
            _organization = organization;
        }
        /// <summary>
        /// 表达式解析
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="formula">表达式</param>
        /// <returns>表达式解析结果</returns>
        public T Calculate<T>(string formula)
        {
            return this.Calculate<T>(null, formula, null);
        }
        /// <summary>
        /// 表达式解析
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="formula">表达式</param>
        /// <param name="dataDic">字典类型数据源</param>
        /// <returns>表达式解析结果</returns>
        public T Calculate<T>(string formula, Dictionary<string, object> dataDic)
        {
            return this.Calculate<T>(null, formula, dataDic);
        }

        private T Calculate<T>(Function[] functions, string formula, Dictionary<string, object> dataDic)
        {
            object source = this.Calculate(functions, formula, dataDic);
            return Convertor.Convert<T>(source);
        }

        private object Calculate(Function[] functions, string formula, Dictionary<string, object> dataDic)
        {
            KeyValueTable variables = new KeyValueTable(dataDic);
            Variant variant = new FormulaParser(formula).Calculate(this._organization, functions, variables);
            return ((variant is null) ? null : variant.Value);
        }
    }
}
