using LsqParserEngine.Entity.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace LsqParserEngine.WebApi.Filter
{
    /// <summary>
    /// 全局的参数验证
    /// </summary>
    public class GlobalValidationFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 拦截参数不合法的请求
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)//验证未通过
            {
                dynamic result = context.Result;
                Dictionary<string, string[]> errors = result.Value.Errors;
                string errorMsg = string.Empty;
                foreach (var item in errors)
                {
                    string msg = $"{item.Key}:{string.Join(",", item.Value)}";
                    errorMsg += msg + ";";
                }
                context.Result = new JsonResult(new
                {
                    Code = ErrorCode.InvalidParameter,
                    ErrorMessage = errorMsg
                });
            }
            base.OnResultExecuting(context);
        }

    }
}
