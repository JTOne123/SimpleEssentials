using System;

namespace CrackerBarrel.Foundation.Cache
{
    public class SessionObject<T> : ISessionObject<T>
    {
        public T Data { get; set; }
        public DateTime? Expiration { get; set; }
    }
}