using LsqParserEngine.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LsqParserEngine.WebApi.Filter
{
    /// <summary>
    /// 全局权限过滤器
    /// </summary>
    public class UserAuthorzationAttribute : ActionFilterAttribute
    {
        private readonly ICacheHelper _cacheHelper;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="cacheHelper"></param>
        public UserAuthorzationAttribute(ICacheHelper cacheHelper)
        {
            this._cacheHelper = cacheHelper;
        }

        /// <summary>
        /// 方法执行之前进行拦截
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            //if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() == null)
            //{
            //    string token = context.HttpContext.Request.Headers["UserToken"];
            //    if (string.IsNullOrEmpty(token))
            //    {
            //        throw new BusinessException
            //        {
            //            Code = ErrorCode.UnAuthorized,
            //            ErrorMessage = "未授权的请求"
            //        };
            //    }
            //    //去Redis中读取用户信息
            //    //注意此处Get方法如果获取到了用户信息，会自动延时过期时间30分钟
            //    UserInfo userInfo = _cacheHelper.GetEntity<UserInfo>(token, 30);
            //    if (userInfo == null)
            //    {
            //        throw new BusinessException
            //        {
            //            Code = ErrorCode.UnAuthorized,
            //            ErrorMessage = "未授权的请求"
            //        };
            //    }
            //    else
            //    {
            //        //此处可以继续校验用户权限信息，多设备登录等业务
            //    }
            //}
            base.OnActionExecuting(context);
        }
    }
}
