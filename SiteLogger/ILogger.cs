using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteLogger
{
    public interface ILogger
    {
        bool IsValid { get; }
        void Info(string message);
        void Warn(string message);
        void Debug(string message);
        void Trace(string message);
        void Error(string message);
        void Error(Exception x);
        void Fatal(string message);
        void Fatal(Exception x);
        void Info(LogItem item);
        void Warn(LogItem item);
        void Debug(LogItem item);
        void Trace(LogItem item);
        void Error(LogItem item);
        void Fatal(LogItem item);
        void Custom(LogItem item);
        

    }
}
