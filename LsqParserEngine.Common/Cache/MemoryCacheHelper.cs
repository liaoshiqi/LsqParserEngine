using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;

namespace LsqParserEngine.Common
{
    /// <summary>
    /// 使用内存的缓存
    /// </summary>
    public class MemoryCacheHelper : ICacheHelper
    {
        private MemoryCache _cache;

        /// <summary>
        /// 构造
        /// </summary>
        public MemoryCacheHelper()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns>是否存在</returns>
        public bool Exists(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        /// <summary>
        /// 获取字符串类型的值
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="expireTime">滑动过期时间；单位（分钟）</param>
        /// <returns>缓存的结果，可能为null</returns>
        public string GetStringValue(string key, int expireTime = 10)
        {
            string val = _cache.Get<string>(key);
            if (!string.IsNullOrEmpty(val) && expireTime > 0)
            {
                _cache.Set<string>(key, val, new TimeSpan(0, expireTime, 0));
            }
            return val;
        }

        /// <summary>
        /// 获取复杂类型的缓存值
        /// </summary>
        /// <typeparam name="TEntity">缓存类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="expireTime">滑动过期时间；单位（分钟）</param>
        /// <returns>缓存的结果，可能为null</returns>
        public TEntity GetEntity<TEntity>(string key, int expireTime = 10) where TEntity : class
        {
            var result = _cache.Get<TEntity>(key);
            if (result != null && expireTime > 0)
            {
                _cache.Set<TEntity>(key, result, new TimeSpan(0, expireTime, 0));
                return result;
            }
            return null;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }
        /// <summary>
        /// 设置一个复杂类型的缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存对象</param>
        /// <param name="expireTime">滑动过期时间；单位（分钟）</param>
        public void SetEntity(string key, object value, int expireTime = 10)
        {
            _cache.Set(key, value, new TimeSpan(0, expireTime, 0));
        }

        /// <summary>
        /// 设置一个字符串类型的缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存内容</param>
        /// <param name="expireTime">滑动过期时间；单位（分钟）</param>
        public void SetString(string key, string value, int expireTime = 10)
        {
            _cache.Set<string>(key, value, new TimeSpan(0, expireTime, 0));
        }

        #region 消息队列，memorycache未实现
        public long RedisPublish<T>(string channel, T sendMessage)
        {
            throw new NotImplementedException();
        }

        public void RedisSubscribe<T>(string channel, Action<string> action)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string channel)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeAll()
        {
            throw new NotImplementedException();
        }

        public void RedisSubscribe<T>(string channel, Action<ChannelMessage> action)
        {
            throw new NotImplementedException();
        }

        public void RedisSubscribeAsync<T>(string channel, Action<string> action)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
