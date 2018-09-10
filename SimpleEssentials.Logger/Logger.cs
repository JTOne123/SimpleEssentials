using System;
using System.Globalization;
using SimpleEssentials.Log.Writters;

namespace SimpleEssentials.Log
{
    public class Logger : ILogger
    {
        //private ILogFileHandler _logFileHandler => Factory.Container.GetInstance<ILogFileHandler>();
        private readonly IWriter _logWriter;

        public Logger(IWriter logWriter)
        {
            _logWriter = logWriter;
        }
        
        public void Debug(string msg)
        {
            _logWriter.Write(FormatedMessage(msg, "DEBUG"));
        }

        public void Error(string msg)
        {
            _logWriter.Write(FormatedMessage(msg, "ERROR"));
        }

        public void Error(string msg, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(string msg)
        {
            _logWriter.Write(FormatedMessage(msg, "INFO"));
        }

        private static string FormatedMessage(string msg, string type)
        {
            return $"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} [{type}]: {msg}";
        }
    }
}
