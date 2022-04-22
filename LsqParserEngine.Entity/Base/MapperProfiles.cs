namespace LsqParserEngine.Entity.Base
{
    /// <summary>
    /// 配置需要转换的映射关系
    /// </summary>
    public class MapperProfiles : AutoMapper.Profile
    {
        /// <summary>
        /// 配置需要转换的映射关系
        /// </summary>
        public MapperProfiles()
        {
            //CreateMap<AppSetting, BuildFrameworkInput>().ReverseMap();
        }
    }
}
