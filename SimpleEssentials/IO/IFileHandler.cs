using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.IO.Types;
using SimpleEssentials.IO.Writers;

namespace SimpleEssentials.IO
{
    public interface IFileHandler : IHandler
    {
        IFile Create(string fileName, IFolder parentFolder);
        bool Write(IFile file, string content, bool append);
        void Write<T>(IFile file, T obj, IFileWriter fileWriter, bool append);
        void Write<T>(IFile file, IEnumerable<T> obj, IFileWriter fileWriter, bool append);
        string Read(IFile file);
        T Read<T>(IFile file, IFileReader fileReader);
        IEnumerable<T> ReadAll<T>(IFile file, IFileReader fileReader);
        IEnumerable<T> ReadBy<T>(IFile file, Func<T, bool> predicate, IFileReader fileReader);
    }
}