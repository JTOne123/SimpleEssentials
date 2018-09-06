using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.Cache
{
    public class CacheObject : ICacheObject
    {
        public object Data { get; set; }
        public TimeSpan LifeSpan { get; set; }
        public DateTime CreateDateTime { get; }

        public CacheObject()
        {
            CreateDateTime = DateTime.UtcNow;
        }
    }
}
