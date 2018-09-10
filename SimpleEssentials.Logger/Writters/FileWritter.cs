using System;
using System.Linq;
using SimpleEssentials.IO;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.Log.Writters
{
    public class FileWritter : IWriter
    {
        private readonly IFolderHandler _folderHandler = new FolderHandler();
        private readonly IFileHandler _fileHandler = new FileHandler();

        public bool Write(string message)
        {
            var logFolder = GetLogFolder();
            var logFile = GetLogFile(logFolder);

            return _fileHandler.Write(logFile, message, true);
        }

        public IFile GetLogFile(IFolder logFolder)
        {
            var logFile = _folderHandler.GetChildFiles(logFolder).FirstOrDefault(x => x.Name.Contains(GetDateString()));
            if(logFile == null || !logFile.Loaded)
            {
                logFile = _fileHandler.Create(GetDateString() + ".txt", logFolder);
            }
            return logFile;
        }

        public IFolder GetLogFolder()
        {
            var folder = (IFolder)_folderHandler.Get("logs");
            if (!folder.Loaded)
            {
                folder = _folderHandler.Create("logs", true);
            }
            return folder;
        }

        public string GetDateString()
        {
            return DateTime.Now.ToShortDateString().Replace("/", "-");
        }
    }
}
