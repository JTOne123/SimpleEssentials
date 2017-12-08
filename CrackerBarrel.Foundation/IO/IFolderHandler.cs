using System.Collections;
using System.Collections.Generic;
using CrackerBarrel.Foundation.IO.Types;

namespace CrackerBarrel.Foundation.IO
{
    public interface IFolderHandler : IHandler
    {
        IEnumerable<IFileType> GetAllFiles(string path);
        IEnumerable<IFileType> GetAllFiles(IFolder parentFolder);
    }
}