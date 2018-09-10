using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.IO.Types;
using SimpleEssentials.IO.Writers;

namespace SimpleEssentials.IO
{
    public class FileHandler : IFileHandler
    {
        public IFileType Create(string path)
        {
            var file = new File(path);
            if (file.Loaded) return file;

            using (var sr = System.IO.File.Create(path))
            {
            }
            

            file.Load(path);
            return file;
        }

        public IFile Create(string fileName, IFolder parentFolder)
        {
            var finalPath = parentFolder.FullPath + System.IO.Path.DirectorySeparatorChar + fileName;

            var file = new File(finalPath);
            if (file.Loaded) return file;

            using (var sr = System.IO.File.Create(finalPath))
            {
            }
            file.Load(finalPath);
            return file;
        }

        public bool Move(ref IFileType file, string newPath)
        {
            var destPath = newPath + file.Name + ((IFile) file).Extension;
            System.IO.File.Move(file.FullPath, destPath);
            file.FullPath = destPath;
            return true;
        }

        public bool Rename(ref IFileType file, string newName)
        {
            var filePath = new System.IO.FileInfo(file.FullPath).Directory?.FullName;
            var newFilePath = filePath + System.IO.Path.DirectorySeparatorChar + newName + ((IFile)file).Extension;
            System.IO.File.Move(file.FullPath, newFilePath);
            file.Name = newName;
            file.FullPath = newFilePath;
            return file.Load(file.FullPath);
        }

        public string Read(IFile file)
        {
            return System.IO.File.ReadAllText(file.FullPath);
            
        }

        public T Read<T>(IFile file, IFileReader fileReader)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ReadAll<T>(IFile file, IFileReader fileReader)
        {
            return fileReader?.ReadAll<T>(file.FullPath);
        }

        public IEnumerable<T> ReadBy<T>(IFile file, Func<T, bool> predicate, IFileReader fileReader)
        {

            return fileReader?.ReadAll<T>(file.FullPath).Where(predicate);
        }

        public bool Write(IFile file, string content, bool append)
        {
            if (append)
            {
                using (var strem = System.IO.File.AppendText(file.FullPath))
                {
                    strem.WriteLine(content);
                }
            }
            else
            {
                using (var strem = System.IO.File.CreateText(file.FullPath))
                {
                    strem.WriteLine(content);
                }
            }
            
            return true;
        }

        public void Write<T>(IFile file, T obj, IFileWriter fileWriter, bool append)
        {
            fileWriter?.Write(file.FullPath, obj, append);
        }

        public void Write<T>(IFile file, IEnumerable<T> obj, IFileWriter fileWriter, bool append)
        {
            fileWriter?.Write(file.FullPath, obj, append);
        }

        public IFileType Get(string path)
        {
            return new File(path);
        }
    }
}