using LsqParserEngine.Entity;
using LsqParserEngine.Entity.Base;
using LsqParserEngine.Entity.DataTable;
using LsqParserEngine.Entity.Input;
using LsqParserEngine.Entity.Input.SchedulerTask;
using LsqParserEngine.Entity.Output.SchedulerTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LsqParserEngine.IDAL
{
    public interface ISchedulerTaskDAL
    {
        /// <summary>
        /// 添加调度任务
        /// </summary>
        /// <param name="schedulerTask"></param>
        /// <returns></returns>
        Task<string> AddSchedulerTaskAsync(AddSchedulerTaskInput schedulerTask);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="schedulerTask"></param>
        /// <returns></returns>
        Task<bool> UpdateSchedulerTaskAsync(UpdateSchedulerTaskInput schedulerTask);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> DeleteSchedulerTaskAsync(BaseIdListInput input);

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetSchedulerTaskOutput> GetSchedulerTaskOutputAsync(BaseGuidEntity input);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResult<GetSchedulerTaskPageOutput>> GetSchedulerTaskPageResultAsync(GetSchedulerTaskPageInput input);

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        Task<List<GetSchedulerTaskOutput>> GetAllSchedulerTaskListAsync();

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PauseTaskAsync(BaseIdListInput input);

        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> RenewTaskAsync(BaseIdListInput input);
    }
}
