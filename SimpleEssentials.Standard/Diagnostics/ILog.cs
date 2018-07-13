using System;

namespace SimpleEssentials.Diagnostics
{
    public interface ILog
    {
        void Info(string msg);
        void Error(string msg);
        void Error(string msg, Exception exception);
        void Debug(string msg);
    }
}