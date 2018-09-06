using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.Cache
{
    public interface ICacheObject
    {
        object Data { get; set; }
        TimeSpan LifeSpan { get; set; }
        DateTime CreateDateTime { get; }
    }
}
