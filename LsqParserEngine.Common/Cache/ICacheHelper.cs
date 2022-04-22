using StackExchange.Redis;
using System;

namespace LsqParserEngine.Common
{
    /// <summary>
    /// 缓存帮助类（使用MemoryCache和StackExchangeRedis实现，Startup.cs中已注入）
    /// </summary>
    public interface ICacheHelper
    {
        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>是否存在</returns>
        bool Exists(string key);

        /// <summary>
        /// 获取字符串类型的值
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="expireTime">滑动过期时间；单位（分钟）</param>
        /// <returns>缓存的结果，可能为null</returns>
        string GetStringValue(string key, int expireTime = 10);

        /// <summary>
        /// 获取复杂类型的缓存值
        /// </summary>
        /// <typeparam name="TEntity">缓存类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="expireTime">滑动过期时间；单位（分钟）</param>
        /// <returns>缓存的结果，可能为null</returns>
        TEntity GetEntity<TEntity>(string key, int expireTime = 10) where TEntity : class;

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        void Remove(string key);

        /// <summary>
        /// 设置一个字符串类型的缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存内容</param>
        /// <param name="expireTime">滑动过期时间；单位（分钟）</param>
        void SetEntity(string key, object value, int expireTime = 10);

        /// <summary>
        /// 设置一个复杂类型的缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存对象</param>
        /// <param name="expireTime">滑动过期时间；单位（分钟）</param>
        void SetString(string key, string value, int expireTime = 10);

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel">发送的管道</param>
        /// <param name="sendMessage">发送的消息内容</param>
        /// <returns></returns>
        long RedisPublish<T>(string channel, T sendMessage);

        /// <summary>
        /// 订阅消息（强制队列）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel">管道</param>
        /// <param name="action">接收消息之后执行的方法</param>
        void RedisSubscribe<T>(string channel, Action<ChannelMessage> action);

        /// <summary>
        /// 订阅消息（不会严格按照队列的方式执行）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel">管道</param>
        /// <param name="action">接收消息之后执行的方法</param>
        void RedisSubscribeAsync<T>(string channel, Action<string> action);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel">订阅通道</param>
        void Unsubscribe(string channel);

        /// <summary>
        /// 取消所有订阅
        /// </summary>
        void UnsubscribeAll();
    }
}
