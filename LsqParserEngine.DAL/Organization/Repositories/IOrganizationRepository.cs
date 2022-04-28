using LsqParserEngine.Entity.Organization;

namespace LsqParserEngine.Domain.Organization
{
    public interface IOrganizationRepository
    {
        string GetNameByCode(string code);

        Unit GetUnit(string objectId);
    }
}