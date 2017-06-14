using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog.Config;
using NLog;

namespace SiteLogger
{
    public static class LoggingFactory
    {
        private static ILogger _logger;
        private static ILogger _customLoggger;
        private static ILogger _jsLoggger;
        private static ILogger _reportLogger;
        private static ILogger _redisLogger;
        private static ILogger _defaultLogger;
        private static ILogger _mobileLogger;

        private static string LOG_SERVER = null;
         static LoggingFactory()
        {
            LOG_SERVER = System.Configuration.ConfigurationManager.AppSettings["LOG_SERVER"];
            
            SetLogType();
           

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"> type == 1 NLOG , type = 2 MONGO</param>
        public static void SetLogType()
        {

            _logger = new NLogLogger("Current");



        }
       

        internal static void ReInit()
        {
            if (_defaultLogger != null)
            {
                _defaultLogger = new NLogLogger("Current");
            }
            if (_logger != null)
            {
                _logger = new NLogLogger("Current");
            }
            if (_jsLoggger != null)
            {
                _jsLoggger = new NLogLogger("JS");
            }
            if(_customLoggger != null)
            {
                _customLoggger = new NLogLogger("DB");
            }
            if(_redisLogger != null)
            {
                _redisLogger = new NLogLogger("CACHE");
            }
            if (_reportLogger != null)
            {
                _reportLogger = new NLogLogger("REPORT");
            }
            if (_mobileLogger != null)
            {
                _reportLogger = new NLogLogger("MOBILE");
            }

        }

        public static ILogger GetLogger { 
            get {
                if (_logger == null)
                {
                    _logger = new NLogLogger("Current");
                }
                return _logger;
            }
        }


        public static ILogger GetDefaultLogger { 
            get
            {
                if (_defaultLogger == null)
                {

                    _defaultLogger = new NLogLogger("Current");
                   
                }
                return _defaultLogger;
            }
        }

        public static ILogger GetDBLogger
        {
            get
            {
                if (_customLoggger == null)
                {
                   _customLoggger = new NLogLogger("DB");
                }
                return _customLoggger;
            }
        }

        public static ILogger GetJSLogger
        {
            get
            {
                if (_jsLoggger == null)
                {
                    _jsLoggger = new NLogLogger("JS");
                }
                return _jsLoggger;
            }
        }
                
        public static ILogger GetReportLogger
        {
            get
            {
                if (_reportLogger == null)
                {
                   _reportLogger = new NLogLogger("REPORT");
                }
                return _reportLogger;
            }
        }

        public static ILogger GetRedisLogger
        {
            get
            {
                if (_redisLogger == null)
                {
                    _redisLogger = new NLogLogger("CACHE");

                }
                return _redisLogger;
            }
        }

        public static ILogger GetMobileLogger
        {
            get
            {
                if (_mobileLogger == null)
                {
                     _mobileLogger = new NLogLogger("MOBILE");

                }
                return _mobileLogger;
            }
        }


        public static LoggingConfiguration Configure()
        {
            return LogManager.Configuration;

        }

    }
}
