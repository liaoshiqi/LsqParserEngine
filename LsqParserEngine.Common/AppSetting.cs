namespace LsqParserEngine.Common
{
    /// <summary>
    /// 应用设置
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// 跨域设置
        /// </summary>
        public string AllowedHosts { get; set; }

        /// <summary>
        /// 全局异常捕捉
        /// </summary>
        public bool CatchExcption { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnStr { get; set; }

        /// <summary>
        /// 返回统一的格式
        /// </summary>
        public bool CommonResult { get; set; }

        /// <summary>
        /// 数据库命名空间，用于codefirst初始化数据库
        /// </summary>
        public string DataTableNameSpace { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DataBaseType { get; set; }

        /// <summary>
        /// 返回结果命名规范
        /// </summary>
        public Naming Naming { get; set; }

        /// <summary>
        /// Redis配置
        /// </summary>
        public RedisConfig RedisConfig { get; set; }

        /// <summary>
        /// SwaggerUI
        /// </summary>
        public SwaggerUI SwaggerUI { get; set; }

        /// <summary>
        /// 用于数据加密的token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// token过期时间（单位：秒）
        /// </summary>
        public int TokenExpire { get; set; }

        /// <summary>
        /// 启用性能分析
        /// </summary>
        public bool EnableMiniProfiler { get; set; }

        /// <summary>
        /// 启用调度任务
        /// </summary>
        public bool EnableSchedulerTask { get; set; }

        /// <summary>
        /// 启用Redis消息队列
        /// </summary>
        public bool EnableRedisMq { get; set; }
    }


    #region 枚举

    /// <summary>
    /// .net版本
    /// </summary>
    public enum DotNetVersion
    {
        /// <summary>
        /// .netcore3.1
        /// </summary>
        DotNetCore3_1 = 0,

        /// <summary>
        /// .net5
        /// </summary>
        NET5 = 1
    }

    /// <summary>
    /// 框架结构
    /// </summary>
    public enum FrameworkModel
    {
        /// <summary>
        /// 简单模式
        /// </summary>
        Simple = 0,

        /// <summary>
        /// 仓储模式
        /// </summary>
        Repository = 1
    }

    /// <summary>
    /// 数据访问层
    /// </summary>
    public enum DataAccessType
    {
        /// <summary>
        /// sqlsugar
        /// </summary>
        SqlSugar = 0,

        /// <summary>
        /// entity framework
        /// </summary>
        Ef = 1
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// SqlServer
        /// </summary>
        SqlServer = 0,

        /// <summary>
        /// Mysql
        /// </summary>
        MySql,

        /// <summary>
        /// Oracle
        /// </summary>
        Oracle
    }

    /// <summary>
    /// swaggerui
    /// </summary>
    public enum SwaggerUI
    {
        /// <summary>
        /// 禁用
        /// </summary>
        Disable = 0,

        /// <summary>
        /// 启用
        /// </summary>
        Enable = 1,

        /// <summary>
        /// 仅开发版使用
        /// </summary>
        Dev = 2
    }

    /// <summary>
    /// 命名规范
    /// </summary>
    public enum Naming
    {
        /// <summary>
        /// 帕斯卡
        /// </summary>
        Pascal = 0,

        /// <summary>
        /// 驼峰
        /// </summary>
        CamelCase = 1,

        /// <summary>
        /// 与后端保持一致
        /// </summary>
        ForServer = 2
    }
    #endregion

    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
    }
}
