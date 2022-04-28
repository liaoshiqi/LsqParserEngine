using LsqParserEngine.Application;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LsqParserEngine.WebApi.Controllers
{
    /// <summary>
    /// 规则解析
    /// </summary>
    public class RuleController : BaseApiController
    {
        public IRuleParserManager RuleParserManager { get; set; }

        public RuleController()
        {
        }

        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="ruleText">规则</param>
        /// <returns></returns>
        [HttpGet("{rule}")]
        public string GetRuleResult(string ruleText)
        {
            return RuleParserManager.Calculate<string>(ruleText);
        }

        /// <summary>
        /// 带数据的表达式解析
        /// </summary>
        /// <param name="ruleText">规则</param>
        /// <param name="datas">数据</param>
        /// <returns></returns>
        [HttpPost("{ruleText}")]
        public string GetRuleResult([FromRoute] string ruleText, [FromBody] Dictionary<string, object> datas)
        {
            return RuleParserManager.Calculate<string>(ruleText, datas);
        }
    }
}
