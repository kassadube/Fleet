using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet.Configuration
{
    public class SESSIONSettings
    {
        public string ACCOUNT;
        public string ONLINE_VEHICLES;
        public string ONLINE_TASK_LAST_REQUEST;// holds the last request datetime

        public SESSIONSettings()
        {
            ACCOUNT = "SESSION_ACCOUNT_ID_{0}";
            ONLINE_VEHICLES = "SESSION_ONLINE_VEHICLES_{0}_{1}";
            ONLINE_TASK_LAST_REQUEST = "SESSION_ONLINE_TASKS_{0}_{1}"; 
        }
    }
}
