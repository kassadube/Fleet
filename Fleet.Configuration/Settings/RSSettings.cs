using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet.Configuration
{
    public class RSSettings
    {
        public RSSettings()
        {
            Credentials = new RSCredentialsSettings();
        }
        public string ReportServer { get; set; }
        public string ReportServiceUrl { get; set; }
        public string ReportServiceExecutionUrl { get; set; }
        public int MaxVehiclesPerReport { get; set; }
        public RSCredentialsSettings Credentials { get; set; }
        public List<int> FleetReports { get; set; }
        public string ExcelFormat { get; set; }
    }
    public class RSCredentialsSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; } 

        

    }
}
