namespace LsqParserEngine.Domain.Organization
{
    public interface IOrganizationRepository
    {
        string GetNameByCode(string code);
    }
}