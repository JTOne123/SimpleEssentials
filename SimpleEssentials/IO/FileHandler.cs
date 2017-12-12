using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO
{
    public class FileHandler : Handler, IFileHandler
    {
        public override IFileType Create(string path)
        {
            var newFile = System.IO.File.Create(path);
            newFile.Close();
            return new File(path);
        }

        public override bool Move(ref IFileType file, string newPath)
        {
            var destPath = newPath + file.Name + ((IFile) file).Extension;
            System.IO.File.Move(file.FullPath, destPath);
            file.FullPath = destPath;
            return true;
        }

        public override bool Rename(ref IFileType file, string newName)
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

        public T ReadBy<T>(IFile file, Expression<Func<T, bool>> expression, IFileReader fileReader)
        {
            
            throw new NotImplementedException();
        }

        public bool Write(IFile file, string content, bool append)
        {
            using (System.IO.StreamWriter strem =
                new System.IO.StreamWriter(file.FullPath, append))
            {
                strem.WriteLine(content);
            }
            return true;
        }

        public bool Write<T>(IFile file, T obj, bool append)
        {
            throw new NotImplementedException();
        }

        public bool Write<T>(IFile file, IEnumerable<T> obj, bool append)
        {
            throw new NotImplementedException();
        }

        public override IFileType Get(string path)
        {
            return new File(path);
        }
    }
}