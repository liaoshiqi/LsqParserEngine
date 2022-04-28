using LsqParserEngine.Entity.Organization;
using System;

namespace LsqParserEngine.Domain.Organization
{
    public class OrganizationRepository : IOrganizationRepository
    {
        public OrganizationRepository()
        {
        }

        public string GetNameByCode(string code)
        {
            return "大齐齐大";
        }

        public Unit GetUnit(string objectId)
        {
            return new User()
            {
                Name = "大齐齐大",
                Code = "bigqi",
                ParentID = "",
                ObjectID = Guid.NewGuid().ToString(),
            };
        }

    }
}