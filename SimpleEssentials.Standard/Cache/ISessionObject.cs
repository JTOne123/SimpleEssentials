using System;

namespace SimpleEssentials.Cache
{
    public interface ISessionObject<T>
    {
        T Data { get; set; }
        DateTime? Expiration { get; set; }
    }
}