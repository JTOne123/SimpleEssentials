using System.Collections;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO
{
    public interface IHandler
    {
        T Get<T>(string path) where T : IFileType, new();
        bool Move(IFileType file, string newPath);
        IFileType Create(string path);
        bool Rename(IFileType file, string newName);
    }
}