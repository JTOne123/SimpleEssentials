using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEssentials.Cache
{
    public class CacheSettings
    {
        public string Key { get; set; }
        public TimeSpan? LifeSpan { get; set; }

        public CacheSettings(string key)
        {
            Key = key;
        }

        public CacheSettings(string key, TimeSpan lifeSpan) : this(key)
        {
            LifeSpan = lifeSpan;
        }
    }
}
