using LsqParserEngine.Common;
using Microsoft.AspNetCore.Mvc;

namespace LsqParserEngine.WebApi.Controllers
{
    /// <summary>
    /// 基础的api控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// 可以直接使用属性注入
        /// </summary>
        public ICacheHelper CacheHelper { get; set; }
    }
}
