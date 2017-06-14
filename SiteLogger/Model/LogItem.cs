using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteLogger
{
    public class LogItem 
    {
        public string Id { get; set; }
        public string LogServer { get; set; }
        public string SiteId { get; set; }
        public int? AccountId { get; set; }
        public int? UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public string Message { get; set; }
        public LOGTYPE LogType { get; set; }
       
        public string Url { get; set; }
       
        public string Referrer { get; set; }
       
        public string Trace { get; set; }
       
        public string UserAgent { get; set; }

        public bool AddMissingItems { get; set; }

         public bool WriteToDefault { get; set; }

        public override string ToString()
        {
            this.LogServer = System.Configuration.ConfigurationManager.AppSettings["LOG_SERVER"];
            string value = string.Format("¡LOG SERVER = {6}¡ URL = {0}¡ AccountId = {2}¡ MainId = {3}¡ UserAgent = {4}¡ MESSAGE = {1} ¡ TRACE = {5}¡", this.Url, this.Message, this.AccountId, this.SiteId, this.UserAgent, this.Trace, this.LogServer);

            return value;
        }
    }

    public enum LOGTYPE
    {
        Info = 1,
        Debug = 2,
        Warn = 3,
        Trace = 4,
        Error = 5,
        Fatal = 6,
        JSError = 7,
        DBTrace = 8,
        Test = 9,
        Report = 10,
        Cache = 11
    }
}
