using LsqParserEngine.Entity;
using System.Collections.Generic;

namespace LsqParserEngine.Application
{
    public class RuleParserManager : IRuleParserManager
    {
        /// <summary>
        /// 规则解析
        /// </summary>
        public IRuleParserService RuleParserService { get; set; }

        public RuleParserManager()
        {
        }

        public T Calculate<T>(string formula)
        {
            return RuleParserService.Calculate<T>(formula);
        }

        public T Calculate<T>(string formula, Dictionary<string, object> dataDic)
        {
            return RuleParserService.Calculate<T>(formula, dataDic);

        }
    }
}
