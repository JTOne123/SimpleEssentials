using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleEssentials.IO.Handlers;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.IO.Types;
using SimpleEssentials.IO.Writers;

namespace SimpleEssentials.IO
{
    public static class FileHandler
    {
        private static readonly IFileHandler FileHandlerInstance = new Handlers.FileHandler();

        public static IFile Create(string path)
        {
            return (IFile)FileHandlerInstance.Create(path);
        }

        public static IFile Create(string fileName, IFolder parentFolder)
        {
            return (IFile)FileHandlerInstance.Create(fileName, parentFolder);
        }

        public static bool Move(ref IFileType file, string newPath)
        {
            return FileHandlerInstance.Move(ref file, newPath);
        }

        public static bool Rename(ref IFileType file, string newName)
        {
            return FileHandlerInstance.Rename(ref file, newName);
        }

        public static string Read(IFile file, Dictionary<string, string> metaData = null)
        {
            return FileHandlerInstance.Read(file, metaData);

        }

        public static T Read<T>(IFile file, IFileReader fileReader, Dictionary<string, string> metaData = null) where T : class, new()
        {
            return FileHandlerInstance.Read<T>(file, fileReader, metaData);
        }

        [Obsolete("Deprecated. Use ReadToList instead.")]
        public static IEnumerable<T> ReadAll<T>(IFile file, IFileReader fileReader, Dictionary<string, string> metaData = null) where T : class, new()
        {
            return ReadToList<T>(file, fileReader, metaData);
        }

        public static IEnumerable<T> ReadToList<T>(IFile file, IFileReader fileReader, Dictionary<string, string> metaData = null) where T : class, new()
        {
            return FileHandlerInstance.ReadToList<T>(file, fileReader, metaData);
        }

        public static IEnumerable<T> ReadBy<T>(IFile file, Func<T, bool> predicate, IFileReader fileReader, Dictionary<string, string> metaData = null) where T : class, new()
        {

            return FileHandlerInstance.ReadBy(file, predicate, fileReader, metaData);
        }

        public static bool Write(IFile file, string content, bool append)
        {
            return FileHandlerInstance.Write(file, content, append);
        }

        public static void Write<T>(IFile file, T obj, IFileWriter fileWriter, bool append)
        {
            FileHandlerInstance.Write(file, obj, fileWriter, append);
        }

        public static void Write<T>(IFile file, IEnumerable<T> obj, IFileWriter fileWriter, bool append)
        {
            FileHandlerInstance.Write(file, obj, fileWriter, append);
        }

        public static IFile Get(string path)
        {
            return (IFile)FileHandlerInstance.Get(path);
        }
    }
}
