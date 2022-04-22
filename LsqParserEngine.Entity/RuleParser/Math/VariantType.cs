namespace LsqParserEngine.Entity
{
    /// <summary>
    /// 变量类型
    /// </summary>
    public enum VariantType
    {
        Unknown = -1,
        Bool = 1,
        Int = 2,
        Double = 3,
        String = 4,
        DateTime = 5,
        TimeSpan = 6,
        Unit = 7,
        Null = 8,
        SubSchemaProperty = 19,
        Array = 20,
    }
}
