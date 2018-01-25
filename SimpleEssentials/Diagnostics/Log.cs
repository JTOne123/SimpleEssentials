using System;
using System.Globalization;

namespace SimpleEssentials.Diagnostics
{
    public class Log : ILog
    {
        private ILogFileHandler _logFileHandler => Factory.Container.GetInstance<ILogFileHandler>();
        
        public void Debug(string msg)
        {
            _logFileHandler.Insert(FormatedMessage(msg, "DEBUG"));
        }

        public void Error(string msg)
        {
            _logFileHandler.Insert(FormatedMessage(msg, "ERROR"));
        }

        public void Error(string msg, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(string msg)
        {
            _logFileHandler.Insert(FormatedMessage(msg, "INFO"));
        }

        private static string FormatedMessage(string msg, string type)
        {
            return $"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} [{type}] - {msg}";
        }
    }
}
