namespace SimpleEssentials.Cache
{
    public interface ICacheable
    {
        void InvalidateCache();
        void UpdateCache<T>(T obj);
    }
}
