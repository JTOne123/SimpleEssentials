using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO.Handlers
{
    public interface IHandler
    {
        bool Move(ref IFileType file, string newPath);
        IFileType Create(string path);
        IFileType Create(string path, IFolder parentFolder);
        IFileType Get(string path);
        bool Rename(ref IFileType file, string newName);
    }
}