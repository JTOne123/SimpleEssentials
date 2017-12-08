using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using CrackerBarrel.Foundation.IO.Types;

namespace CrackerBarrel.Foundation.IO
{
    public interface IFileHandler : IHandler
    {
        bool Write(IFile file, string content, bool append);
        bool Write<T>(IFile file, T obj, bool append);
        bool Write<T>(IFile file, IEnumerable<T> obj, bool append);
        string Read(IFile file);
        T Read<T>(IFile file);
        IEnumerable<T> ReadAll<T>(IFile file);
        T ReadBy<T>(IFile file, Expression<Func<T, bool>> expression);
    }
}