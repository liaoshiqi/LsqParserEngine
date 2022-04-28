using LsqParserEngine.Entity.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 组织机构接口
    /// </summary>
    public interface IOrganization
    {
        public string GetNameByCode(string code);

        public Unit GetUnit(string objectId);
    }
}
