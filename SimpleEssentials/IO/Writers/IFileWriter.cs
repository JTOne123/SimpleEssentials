using System.Collections.Generic;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO.Writers
{
    public interface IFileWriter
    {
        void Write<T>(string filePath, T obj, bool append);
        void Write<T>(string filePath, IEnumerable<T> obj, bool append);
    }
}