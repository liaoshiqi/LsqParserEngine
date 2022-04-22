using System.Collections.Generic;
using System.Text.RegularExpressions;
using LsqParserEngine.Entity;
using LsqParserEngine.Common.Convertor;

namespace LsqParserEngine.Domain.Organization
{
    /// <summary>
    /// 组织机构的领域
    /// </summary>
    public class OrganizationService : IOrganization
    {
        IOrganizationRepository _organizationRepository;
        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public string GetNameByCode(string code)
        {
            return _organizationRepository.GetNameByCode(code);
        }
    }
}