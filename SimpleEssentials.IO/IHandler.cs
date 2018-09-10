using System.Collections;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO
{
    public interface IHandler
    {
        bool Move(ref IFileType file, string newPath);
        IFileType Create(string path);
        IFileType Get(string path);
        bool Rename(ref IFileType file, string newName);
    }
}