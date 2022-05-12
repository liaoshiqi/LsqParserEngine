using LsqParserEngine.Entity;
using LsqParserEngine.Entity.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace LsqParserEngine.WebApi.Filter
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<GlobalExceptionFilter> _logger;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="logger"></param>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 异常捕捉
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var operation = context.HttpContext.Request.RouteValues["controller"] + "/" + context.HttpContext.Request.RouteValues["action"];

            //拦截处理
            if (context.Exception is BusinessException exception)
            {
                context.Result = new JsonResult(new
                {
                    exception.Code,
                    exception.ErrorMessage
                });
            }
            else
            {
                //记录错误日志
                _logger.LogError(context.Exception, $"{operation}  Exception:" + context.Exception.Message);
                context.Result = new JsonResult(new
                {
                    Code = ErrorCode.ServerError,
                    ErrorMessage = context.Exception.Message
                });
            }
            context.ExceptionHandled = true;
        }
    }
}
