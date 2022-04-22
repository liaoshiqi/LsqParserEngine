using LsqParserEngine.Entity.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LsqParserEngine.WebApi.Filter
{
    /// <summary>
    /// 返回统一的格式
    /// </summary>
    public class GlobalApiResultFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 方法执行完成后
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //没有异常
            if (context.Exception == null)
            {
                ObjectResult contentResult = context.Result as ObjectResult;
                OkObjectResult okObject = new OkObjectResult(new
                {
                    Code = ErrorCode.Success,
                    Result = contentResult?.Value
                });
                context.Result = okObject;
            }
            base.OnActionExecuted(context);
        }
    }
}
