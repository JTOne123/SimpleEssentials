using System.Collections.Generic;
using CrackerBarrel.Foundation.IO.Types;

namespace CrackerBarrel.Foundation.IO
{
    public class FolderHandler : Handler, IFolderHandler
    {
        public override IFileType Create(string path)
        {
            System.IO.Directory.CreateDirectory(path);
            return new Folder(path);
        }

        public override bool Rename(IFileType file, string newName)
        {
            var newFilePath = file.FullPath + System.IO.Path.DirectorySeparatorChar + newName;
            var tempFilePath = file.FullPath + "_tmpfile";
            System.IO.Directory.Move(file.FullPath, tempFilePath);
            System.IO.Directory.Move(tempFilePath, newFilePath);
            return file.Load(newFilePath);
        }

        public IEnumerable<IFileType> GetAllFiles(string path)
        {
            var files = new List<IFileType>();
            files.AddRange(GetChildFiles(path));
            files.AddRange(GetChildFolders(path));

            return files;
        }

        public IEnumerable<IFileType> GetAllFiles(IFolder parentFolder)
        {
            return GetAllFiles(parentFolder.FullPath);
        }

        public override bool Move(IFileType file, string newPath)
        {
            System.IO.File.Move(file.FullPath, newPath);
            return file.Load(newPath);
        }

        private static IEnumerable<IFileType> GetChildFiles(string dirPath)
        {
            var files = new List<IFileType>();
            var filePaths = System.IO.Directory.GetFiles(dirPath);

            foreach (var filePath in filePaths)
            {
                var file = new File(filePath);
                if(file.Loaded)
                    files.Add(file);
            }

            return files;

        }

        private static IEnumerable<IFileType> GetChildFolders(string dirPath)
        {
            var files = new List<IFileType>();
            var filePaths = System.IO.Directory.GetDirectories(dirPath);

            foreach (var filePath in filePaths)
            {
                var file = new Folder(filePath);
                if (file.Loaded)
                    files.Add(file);
            }

            return files;
        }
    }
}