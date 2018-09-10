using System;

namespace SimpleEssentials.Log
{
    public interface ILogger
    {
        void Info(string msg);
        void Error(string msg);
        void Error(string msg, Exception exception);
        void Debug(string msg);
    }
}