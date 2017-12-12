using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO
{
    public abstract class Handler : IHandler
    {
        public abstract IFileType Create(string path);

        public abstract IFileType Get(string path);

        public abstract bool Move(ref IFileType file, string newPath);

        public abstract bool Rename(ref IFileType file, string newName);
    }
}