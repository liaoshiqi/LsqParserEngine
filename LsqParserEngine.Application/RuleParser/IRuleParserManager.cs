
using System.Collections.Generic;

namespace LsqParserEngine.Application
{
    public interface IRuleParserManager
    {
        T Calculate<T>(string formula);

        T Calculate<T>(string formula, Dictionary<string, object> dataDic);

        bool Validate(string formula, ref string errorMessage);

    }
}
