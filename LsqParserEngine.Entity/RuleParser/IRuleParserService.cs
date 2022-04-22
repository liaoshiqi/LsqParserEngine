using System;
using System.Collections.Generic;

namespace LsqParserEngine.Entity
{
    public interface IRuleParserService
    {
        T Calculate<T>(string formula);

        T Calculate<T>(string formula, Dictionary<string, object> dataDic);
    }
}
