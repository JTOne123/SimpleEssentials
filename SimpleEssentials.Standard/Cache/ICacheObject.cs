using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Cache
{
    public interface ICacheObject
    {
        object Data { get; set; }
        TimeSpan LifeSpan { get; set; }
        CacheObjectType ObjectType { get; set; }
    }
}
