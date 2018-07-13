using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Cache
{
    public interface ICustomCacheObject<T>
    {
        T Data { get; set; }
        TimeSpan LifeSpan { get; set; }
    }
}
