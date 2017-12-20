using System.Collections;
using System.Collections.Generic;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO
{
    public interface IFolderHandler : IHandler
    {
        IFileType Create(string path, bool relative);
        IEnumerable<IFileType> GetChildren(IFolder parentFolder);
        IEnumerable<IFile> GetChildFiles(IFolder parentFolder);
        IEnumerable<IFolder> GetChildFolders(IFolder parentFolder);
    }
}