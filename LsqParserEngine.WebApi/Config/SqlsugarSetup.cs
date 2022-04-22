using LsqParserEngine.Common;
using LsqParserEngine.Entity.Base;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace LsqParserEngine.WebApi
{
    /// <summary>
    /// 注入sqlsugar
    /// </summary>
    public static class SqlsugarSetup
    {
        /// <summary>
        /// 注入sqlsugar
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appSetting"></param>
        public static void AddSqlsugarSetup(this IServiceCollection services, AppSetting appSetting)
        {
            DbType dbType = DbType.MySql;
            switch (appSetting.DataBaseType)
            {
                case DataBaseType.SqlServer:
                    dbType = DbType.SqlServer;
                    break;
                case DataBaseType.MySql:
                    dbType = DbType.MySql;
                    break;
                case DataBaseType.Oracle:
                    dbType = DbType.Oracle;
                    break;
            }

            ConnectionConfig connectionConfig = new()
            {
                ConnectionString = appSetting.ConnStr,
                //设置数据库类型
                DbType = dbType,
                //自动释放数据务，如果存在事务，在事务结束后释放
                IsAutoCloseConnection = true,
                //从实体特性中读取主键自增列信息
                InitKeyType = InitKeyType.Attribute
            };

            services.AddScoped<ISqlSugarClient>((x) =>
            {
                return new SqlSugarClient(connectionConfig);
            });
        }
    }
}
