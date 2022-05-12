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
        /// 表达式解析
        /// </summary>
        /// <param name="ruleText">规则表达式</param>
        /// <returns></returns>
        [HttpGet("{ruleText}")]
        public string GetRuleResult(string ruleText)
        {
            return RuleParserManager.Calculate<string>(ruleText);
        }

        /// <summary>
        /// 带数据的表达式解析
        /// </summary>
        /// <param name="ruleText">规则表达式</param>
        /// <param name="datas">数据</param>
        /// <returns></returns>
        [HttpPost("{ruleText}")]
        public string GetRuleResult([FromRoute] string ruleText, [FromBody] Dictionary<string, object> datas)
        {
            return RuleParserManager.Calculate<string>(ruleText, datas);
        }

        /// <summary>
        /// 校验表达式是否合法
        /// </summary>
        /// <param name="ruleText">规则</param>
        /// <returns></returns>
        [HttpGet("{ruleText}")]
        public IActionResult ValidateRule(string ruleText)
        {
            string error = string.Empty;
            bool result = RuleParserManager.Validate(ruleText, ref error);
            if (result)
            {
                return Ok(true);
            }
            return Message(error);
        }
    }
}
