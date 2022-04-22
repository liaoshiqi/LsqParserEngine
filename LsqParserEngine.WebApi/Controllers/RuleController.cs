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

        [HttpGet("{rule}")]
        public string GetRuleResult(string ruleText)
        {
            return RuleParserManager.Calculate<string>(ruleText);
        }

        [HttpPost("{ruleText}")]
        public string GetRuleResult([FromRoute] string ruleText, [FromBody] Dictionary<string, object> datas)
        {
            return RuleParserManager.Calculate<string>(ruleText, datas);
        }
    }
}
