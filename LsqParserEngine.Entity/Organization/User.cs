namespace LsqParserEngine.Entity.Organization
{
    /// <summary>
    /// 人员
    /// </summary>
    public class User : Unit
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string Code { get; set; }

        public string Password { get; set; }
        /// <summary>
        /// 主管领导
        /// </summary>
        public string ManagerID { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
    }
}
