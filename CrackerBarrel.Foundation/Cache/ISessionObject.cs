using System;

namespace CrackerBarrel.Foundation.Cache
{
    public interface ISessionObject<T>
    {
        T Data { get; set; }
        DateTime? Expiration { get; set; }

    }
}