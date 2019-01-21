using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SimpleEssentials.IO.Handlers;
using SimpleEssentials.IO.Types;
using File = SimpleEssentials.IO.Types.File;

namespace SimpleEssentials.IO
{
    public static class FolderHandler
    {
        private static readonly IFolderHandler FolderHandlerInstance = new Handlers.FolderHandler();

        public static IFolder Create(string path)
        {
            return (IFolder)FolderHandlerInstance.Create(path);
        }

        public static IFolder Create(string path, bool relative)
        {
            return FolderHandlerInstance.Create(path, relative);
        }

        public static IFolder Create(string path, IFolder parent)
        {
            return (IFolder)FolderHandlerInstance.Create(path, parent);
        }

        public static bool Rename(ref IFileType file, string newName)
        {
            return FolderHandlerInstance.Rename(ref file, newName);
        }

        public static IEnumerable<IFileType> GetChildren(IFolder parentFolder)
        {
            return FolderHandlerInstance.GetChildren(parentFolder);
        }

        public static bool Move(ref IFileType file, string newPath)
        {
            return FolderHandlerInstance.Move(ref file, newPath);
        }

        public static IEnumerable<IFile> GetChildFiles(IFolder parentFolder)
        {
            return FolderHandlerInstance.GetChildFiles(parentFolder);
        }

        public static IEnumerable<IFolder> GetChildFolders(IFolder parentFolder)
        {
            return FolderHandlerInstance.GetChildFolders(parentFolder);
        }

        public static IFolder Get(string path)
        {
            return (IFolder)FolderHandlerInstance.Get(path);
        }

    }
}
