namespace LsqParserEngine.Entity.Base
{
    /// <summary>
    /// 异常分类枚举
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,

        /// <summary>
        /// 参数不合法
        /// </summary>
        InvalidParameter = 203,

        /// <summary>
        /// 未授权
        /// </summary>
        UnAuthorized = 401,

        /// <summary>
        /// 未知服务器异常
        /// </summary>
        ServerError = 500,

        /// <summary>
        /// 数据已删除
        /// </summary>
        DataDeleted = 1004,
    }
}
