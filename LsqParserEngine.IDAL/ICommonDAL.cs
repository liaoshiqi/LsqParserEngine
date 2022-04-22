using LsqParserEngine.Entity.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LsqParserEngine.IDAL
{
    public interface ICommonDAL
    {
        /// <summary>
        /// 初始化数据表
        /// </summary>
        /// <returns></returns>
        void InitDataTable();
    }
}
