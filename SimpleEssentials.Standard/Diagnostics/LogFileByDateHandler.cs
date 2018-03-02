using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.Diagnostics
{
    public class LogFileByDateHandler : ILogFileHandler
    {
        private IFile _logFile;

        public LogFileByDateHandler()
        {
            _logFile = Factory.FileHandler.Create(GetFileName(), Factory.FolderHandler.Create("logs", true));
        }

        public void Insert(string msg)
        {
            Factory.FileHandler.Write(_logFile, msg, true);
        }

        private string GetFileName()
        {
            return DateTime.Now.ToString("d").Replace("/", "-").Replace(":", "-") + ".txt";
        }
    }
}
