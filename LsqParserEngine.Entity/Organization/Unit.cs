namespace LsqParserEngine.Entity.Organization
{
    /// <summary>
    /// 组织机构基类
    /// </summary>
    public abstract class Unit
    {
        public string ObjectID { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public string ParentID { get; set; }

    }
}
