using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Cache
{
    public class CacheObject : ICacheObject
    {
        public object Data { get; set; }
        public TimeSpan LifeSpan { get; set; }
        public CacheObjectType ObjectType { get; set; }
    }
}
