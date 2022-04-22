using System.Collections;

namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 数据源（可以是字典、数据模型等）类型
    /// </summary>
    public interface IVariableTable : IDictionary, ICollection, IEnumerable
    {
        VariantType GetVariableType(string variableName);

        DataLogicType GetLogicType(string variableName);

    }
}