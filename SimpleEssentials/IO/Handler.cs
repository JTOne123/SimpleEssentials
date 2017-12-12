using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO
{
    public abstract class Handler : IHandler
    {
        public abstract IFileType Create(string path);

        public T Get<T>(string path) where T : IFileType, new()
        {
            T file = new T();
            file.Load(path);

            return file;
        }
        public abstract bool Move(IFileType file, string newPath);

        public abstract bool Rename(IFileType file, string newName);
    }
}