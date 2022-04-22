using LsqParserEngine.Entity.Base;
using LsqParserEngine.Entity.DataTable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LsqParserEngine.IDAL
{
    public interface ISchedulerTaskLogDAL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns>主键</returns>
        Task<string> AddSchedulerTaskLogAsync(SchedulerTaskLog input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> DeleteSchedulerTaskLogAsync(BaseIdListInput input);

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SchedulerTaskLog> GetSchedulerTaskLogAsync(BaseGuidEntity input);

        /// <summary>
        /// 获取指定任务的执行日志
        /// </summary>
        /// <param name="input">任务id</param>
        /// <returns></returns>
        Task<List<SchedulerTaskLog>> GetSchedulerTaskLogListByTaskIdAsync(BaseGuidEntity input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="taskId">老id</param>
        /// <param name="taskId">新id</param>
        /// <returns></returns>
        Task<bool> UpdateSchedulerTaskLogByTaskIdAsync(string taskId, string newId);
    }
}
