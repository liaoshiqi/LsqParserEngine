using System.Collections.Generic;
using System.Text.RegularExpressions;
using LsqParserEngine.Entity;
using LsqParserEngine.Common.Convertor;
using System;

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

        /// <summary>
        /// 校验表达式合法性
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Validate(string formula, ref string errorMessage)
        {
            bool result = true;
            List<string> errors = new List<string>();
            if (!string.IsNullOrEmpty(formula))
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                string key = Guid.NewGuid().ToString();
                dictionary.Add(key, formula);
                List<string> variableNames = new List<string>();
                Function[] functionArray = FunctionFactory.Create(_organization);
                Dictionary<string, Function> functionDictionary = new Dictionary<string, Function>();
                if (functionArray != null)
                {
                    foreach (Function function in functionArray)
                    {
                        functionDictionary.Add(function.FunctionName, function);
                    }
                }
                result = FormulaParser.Validate(formula, functionDictionary, variableNames, ref errors);
                errorMessage = string.Join(";", errors);
            }
            return result;
        }
    }
}
