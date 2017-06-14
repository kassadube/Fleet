using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Web;

namespace SiteLogger
{
    public class NLogLogger : ILogger
    {
        private Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger(); 

        }
        public NLogLogger(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);

        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
           _logger.Debug(message);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
            
        }

        public void Error(string message)
        {
            string url = (HttpContext.Current != null && HttpContext.Current.Handler != null) ? HttpContext.Current.Request.Url.ToString() : "NO HANDLER";

            _logger.Error(string.Format("URL : {0} \nMESSAGE : {1}", url, message));
        }
        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }
        public void Fatal(string message)
        {
            _logger.Fatal(message);
            
        }
        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }




        public void Info(LogItem item)
        {
            //string value = string.Format("URL = {0}, MESSAGE = {1} , AccountId = {2}, MainId = {3}", item.Url, item.Message, item.AccountId, item.SiteId);
            _logger.Info(item.ToString());
        }

        public void Warn(LogItem item)
        {
           // string value = string.Format("URL = {0}, MEASSGE = {1} , AccountId = {2}, MainId = {3}", item.Url, item.Message, item.AccountId, item.SiteId);
            _logger.Warn(item.ToString());
        }

        public void Debug(LogItem item)
        {
           // string value = string.Format("URL = {0}, MESSAGE = {1} , AccountId = {2}, MainId = {3}", item.Url, item.Message, item.AccountId, item.SiteId);
            _logger.Debug(item.ToString());
        }

        public void Trace(LogItem item)
        {
            //LogItem log = new LogItem() { Url = this.HttpContext.Request.Url.ToString(), Message = string.Format("TIME: {0}", totalTime) };
            //string value = string.Format("URL = {0}, TIME = {1} , AccountId = {2}, MainId = {3} , UserAgent = {4}", item.Url, item.Message, item.AccountId, item.SiteId, item.UserAgent);
            _logger.Trace(item.ToString());
        }

        public void Error(LogItem item)
        {
             _logger.Error(item.ToString());
        }

        public void Fatal(LogItem item)
        {
           // string value = string.Format("URL = {0}, MESSAGE = {1} , AccountId = {2}, MainId = {3}, UserAgent = {4}", item.Url, item.Message, item.AccountId, item.SiteId, item.UserAgent);
            _logger.Fatal(item.ToString());
        }

        public void Custom(LogItem item)
        {
            if (item.AddMissingItems)
                LoggerHelper.AddMissingItems(item);
            //string value = string.Format("URL = {0}, MESSAGE = {1} , AccountId = {2}, MainId = {3}, UserAgent = {4}", item.Url, item.Message, item.AccountId, item.SiteId, item.UserAgent);
            switch (item.LogType)
            {
                case LOGTYPE.Info:
                    Info(item);
                    break;
                case LOGTYPE.Debug:
                    Debug(item);
                    break;
                case LOGTYPE.Warn:
                    Warn(item);
                    break;
                case LOGTYPE.Trace:
                    Trace(item);
                    break;
                case LOGTYPE.Error:
                    Error(item);
                    break;
                case LOGTYPE.Fatal:
                    Fatal(item);
                    break;
                default:
                    _logger.Info(item.ToString());
                    break;
            }
           
        }

        public bool IsValid
        {
            get { return true; }
        }


       

    }
}
