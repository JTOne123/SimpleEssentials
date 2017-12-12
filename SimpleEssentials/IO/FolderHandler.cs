using System.Collections.Generic;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO
{
    public class FolderHandler : Handler, IFolderHandler
    {
        public override IFileType Create(string path)
        {
            System.IO.Directory.CreateDirectory(path);
            return new Folder(path);
        }

        public override bool Rename(ref IFileType file, string newName)
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

        public override bool Move(ref IFileType file, string newPath)
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

        public override IFileType Get(string path)
        {
            return new Folder(path);
        }
    }
}