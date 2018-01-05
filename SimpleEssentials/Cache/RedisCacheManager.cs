using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CachingFramework.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SimpleEssentials.Cache
{
    public class RedisCacheManager : BaseCacheManager
    {
        private static Context _context;

        public RedisCacheManager(string serverConfiguration = "localhost:6379")
        {
            _context = new Context(serverConfiguration);
        }

        public override void Add<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            _context.Cache.SetObject(cacheKey, data);
        }

        public override void AddHash<T>(IEnumerable<T> data, string cacheKey, string fieldKey, DateTime? expiration = null)
        {
            _context.Cache.SetHashed(cacheKey, fieldKey, data);
        }

        public override void Delete(string cacheKey)
        {
            _context.Cache.Remove(cacheKey);
        }

        public override void DeleteHash(string cacheKey, string fieldKey)
        {
            _context.Cache.RemoveHashed(cacheKey, fieldKey);
        }

        public override T Get<T>(string cacheKey)
        {
            return _context.Cache.GetObject<T>(cacheKey);
        }

        public override IEnumerable<T> GetList<T>(string cacheKey)
        {
            return _context.Cache.GetObject<IEnumerable<T>>(cacheKey);
        }

        public override T GetHash<T>(string cacheKey, string fieldKey)
        {
            return _context.Cache.GetHashed<T>(cacheKey, fieldKey);
        }

        public override void Invalidate(string cacheKey)
        {
            Delete(cacheKey);
        }

        public override void Update<T>(T data, string cacheKey, DateTime? expiration = null)
        {
            Add(data, cacheKey, expiration);

            var redis = ConnectionMultiplexer.Connect("");
            var db = redis.GetDatabase();
        }

        public void test()
        {
            var redis = ConnectionMultiplexer.Connect("");
            var db = redis.GetDatabase();

        }
    }
}
