using System;

namespace LsqParserEngine.Entity.Base
{
    /// <summary>
    /// 自定义业务异常
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// 业务错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 错误代码（方便管理可以使用枚举）
        /// </summary>
        public ErrorCode Code { get; set; }

    }
}
