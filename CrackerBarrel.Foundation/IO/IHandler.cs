using System.Collections;
using CrackerBarrel.Foundation.IO.Types;

namespace CrackerBarrel.Foundation.IO
{
    public interface IHandler
    {
        T Get<T>(string path) where T : IFileType, new();
        bool Move(IFileType file, string newPath);
        IFileType Create(string path);
        bool Rename(IFileType file, string newName);
    }
}