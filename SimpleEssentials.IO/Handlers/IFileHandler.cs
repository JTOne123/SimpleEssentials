using System;
using System.Collections.Generic;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.IO.Types;
using SimpleEssentials.IO.Writers;

namespace SimpleEssentials.IO.Handlers
{
    internal interface IFileHandler : IHandler
    {
        bool Write(IFile file, string content, bool append);
        void Write<T>(IFile file, T obj, IFileWriter fileWriter, bool append);
        void Write<T>(IFile file, IEnumerable<T> obj, IFileWriter fileWriter, bool append);
        string Read(IFile file, Dictionary<string, string> metaData = null);
        T Read<T>(IFile file, IFileReader fileReader, Dictionary<string, string> metaData = null) where T : class, new();
        IEnumerable<T> ReadToList<T>(IFile file, IFileReader fileReader, Dictionary<string, string> metaData = null) where T : class, new();
        IEnumerable<T> ReadBy<T>(IFile file, Func<T, bool> predicate, IFileReader fileReader, Dictionary<string, string> metaData = null) where T : class, new();
    }
}