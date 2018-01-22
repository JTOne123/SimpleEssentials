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

        public string Key { get; set; }
        public CacheStorage StorageType
        {
            get => _storageType ?? CacheStorage.Normal;
            set => _storageType = value;
        }
        public TimeSpan? LifeSpan { get; set; }
    }
}