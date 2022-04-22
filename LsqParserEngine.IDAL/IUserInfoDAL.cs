using LsqParserEngine.Entity.Input.UserInfo;
using LsqParserEngine.Entity.Output.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsqParserEngine.IDAL
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
    public interface IUserInfoDAL
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input">用户名</param>
        /// <returns>前端需要的用户信息</returns>
        UserLoginOutput UserLoginAsync(UserLoginInput input);
    }
}
