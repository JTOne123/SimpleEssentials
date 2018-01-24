using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Cache
{
    public class CacheSettings
    {
        private CacheStorage? _storageType;
        private TimeSpan? _lifeSpan;

        public string Key { get; set; }
        public CacheStorage StorageType
        {
            get => _storageType ?? CacheStorage.Normal;
            set => _storageType = value;
        }

        public TimeSpan LifeSpan
        {
            get => _lifeSpan ?? new TimeSpan(0, 2, 0, 0);
            set => _lifeSpan = value;
        }

        public CacheSettings()
        {
            
        }

        public CacheSettings(string key)
        {
            Key = key;
        }

        public CacheSettings(string key, TimeSpan lifeSpan) : this(key)
        {
            LifeSpan = lifeSpan;
        }

        public CacheSettings(string key, TimeSpan lifeSpan, CacheStorage storageType) : this(key, storageType, lifeSpan)
        {
        }

        public CacheSettings(string key, CacheStorage storageType, TimeSpan lifeSpan) : this(key, lifeSpan)
        {
            StorageType = storageType;
        }

        
    }
}