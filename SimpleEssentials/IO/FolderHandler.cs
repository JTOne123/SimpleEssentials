using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SimpleEssentials.IO.Types;
using File = SimpleEssentials.IO.Types.File;

namespace SimpleEssentials.IO
{
    public class FolderHandler : IFolderHandler
    {
        public IFileType Create(string path)
        {
            System.IO.Directory.CreateDirectory(path);
            return new Folder(path);
        }

        public IFolder Create(string path, bool relative)
        {
            if (!relative)
                return (IFolder)Create(path);

            var filePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var finalpath = Path.GetDirectoryName(filePath) + System.IO.Path.DirectorySeparatorChar + path;
            System.IO.Directory.CreateDirectory(finalpath);
            var finalResult = System.IO.Path.GetDirectoryName(finalpath);
            return new Folder(finalResult);
        }

        public IFolder Create(string path, IFolder parent)
        {
            var parentPath = parent.FullPath;
            var finalPath = System.IO.Path.GetDirectoryName(parentPath + path);
            return (IFolder) Create(finalPath);
        }

        public bool Rename(ref IFileType file, string newName)
        {
            var newFilePath = file.FullPath + System.IO.Path.DirectorySeparatorChar + newName;
            var tempFilePath = file.FullPath + "_tmpfile";
            System.IO.Directory.Move(file.FullPath, tempFilePath);
            System.IO.Directory.Move(tempFilePath, newFilePath);
            return file.Load(newFilePath);
        }

        public IEnumerable<IFileType> GetChildren(IFolder parentFolder)
        {
            var files = new List<IFileType>();
            files.AddRange(GetChildFiles(parentFolder));
            files.AddRange(GetChildFolders(parentFolder));

            return files;
        }

        public bool Move(ref IFileType file, string newPath)
        {
            System.IO.File.Move(file.FullPath, newPath);
            return file.Load(newPath);
        }

        public IEnumerable<IFile> GetChildFiles(IFolder parentFolder)
        {
            var files = new List<IFile>();
            var filePaths = System.IO.Directory.GetFiles(parentFolder.FullPath);

            foreach (var filePath in filePaths)
            {
                var file = new File(filePath);
                if(file.Loaded)
                    files.Add(file);
            }

            return files;

        }

        public IEnumerable<IFolder> GetChildFolders(IFolder parentFolder)
        {
            var files = new List<IFolder>();
            var filePaths = System.IO.Directory.GetDirectories(parentFolder.FullPath);

            foreach (var filePath in filePaths)
            {
                var file = new Folder(filePath);
                if (file.Loaded)
                    files.Add(file);
            }

            return files;
        }

        public IFileType Get(string path)
        {
            return new Folder(path);
        }

        
    }
}