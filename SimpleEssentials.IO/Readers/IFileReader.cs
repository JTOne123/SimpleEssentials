using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.IO.Readers
{
    public interface IFileReader
    {
        T Read<T>(string filePath, Dictionary<string, string> metaData = null) where T : class, new();
        IEnumerable<T> ReadAll<T>(string filePath, Dictionary<string, string> metaData = null) where T : class, new();
    }
}
