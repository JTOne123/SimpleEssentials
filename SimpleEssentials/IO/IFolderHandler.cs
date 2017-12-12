using System.Collections;
using System.Collections.Generic;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.IO
{
    public interface IFolderHandler : IHandler
    {
        IEnumerable<IFileType> GetAllFiles(string path);
        IEnumerable<IFileType> GetAllFiles(IFolder parentFolder);
    }
}