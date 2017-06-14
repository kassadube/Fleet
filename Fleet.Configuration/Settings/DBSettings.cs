using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet.Configuration
{
    public class DBSettings
    {
        public string ConnectionString { get; set; } 
        public string READConnectionString { get; set; }
        public string SafetyConnectionString { get; set; }
        public string TableStorageConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
        public int Timeout { get; set; }
        public int DWTimeout { get; set; }
        public string BlobStorageConnectionString { get; set; }
    }
}
