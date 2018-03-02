using SimpleEssentials.Cache;
using SimpleEssentials.Diagnostics;
using SimpleEssentials.IO;
using SimpleInjector;

namespace SimpleEssentials
{
    public static class Factory
    {
        private static Container _container;

        public static FileHandler FileHandler = new FileHandler();
        public static FolderHandler FolderHandler = new FolderHandler();
        public static Log Log = new Log();
        public static Container Container
        {
            get
            {
                if (_container != null) return _container;

                _container = new Container();
                RegisterDefaults();
                return _container;
            }
        }

        public static void RegisterDefaults()
        {
            _container.ResolveUnregisteredType += (sender, e) =>
            {
                if (e.UnregisteredServiceType == typeof(ILogFileHandler))
                    e.Register(() => new LogFileByDateHandler());
                if (e.UnregisteredServiceType == typeof(ICacheManager))
                    e.Register(() => new MemoryCacheManager());
            };
        }
    }
}
