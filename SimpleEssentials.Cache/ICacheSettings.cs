using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.Cache
{
    public interface ICacheSettings
    {
        string Key { get; set; }
        TimeSpan? LifeSpan { get; set; }
    }
}
