using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet.Configuration
{
    public class GeneralSettings
    {
        public GeneralSettings()
        {
            DbSettings = new DBSettings();
            RsSettings = new RSSettings();
            SafetySettings = new SafetySettings();
            MercuryWsSettings = new MercuryWSSettings();
            CacheSettings = new CACHESetting();
            SessionSettings = new SESSIONSettings();
            Version versionInfo = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            VersionNumber = versionInfo.ToString();// "15.5.14";
            DateOfPublish = "07/06/2017"; 
            xFrameOptions = "ALLOW";
        }
        public DBSettings DbSettings { get; set; }
        public RSSettings RsSettings { get; set; }
        public SafetySettings SafetySettings { get; set; }
        public MercuryWSSettings MercuryWsSettings { get; set; }
        public CACHESetting CacheSettings { get; set; }
        public SESSIONSettings SessionSettings { get; set; }

        public bool UseCompression { get; set; }
        public bool UseSession { get; set; }
        public bool UseCache { get; set; }
        public bool UseAzureCache { get; set; }
        public bool UseStorageTableCache { get; set; }
        public int CacheTimeout { get; set; }
        public string PAI_SERVER { get; set; }
		public bool KmlEnable { get; set; }  
        public bool UseFleetViewSession { get; set; }
        public string DateFormat { get; set; }
        public int LoginCookieExpiration { get; set; }
        public string VersionNumber { get; set; }
        public string DateOfPublish { get; set; }
        public string CachePrefix{ get; set; }
        public string LogServer { get; set; }
        public string[] ResetTaskDataTime { get; set; }
		public string LogType { get; set; }
        public string GoogleMapAPI { get; set; }
        public List<string> FleetSites { get; set; }
        public List<int> FleetViewUsersOveride { get; set; }
        public string xFrameOptions { get; set; }
        public string IsBlobStorageActive { get; set; }
        public string SpecialCharRegex { get; set; }
        public string BlobAccountName { get; set; }
        public string BlobContainerName { get; set; }
        public bool IsScanEngineActive { get; set; }
        public bool IsScanEngineMalfunction { get; set; }
    }

    public class SafetySettings
    {
        public string LoginUrl { get; set; }
        public string TransferUrl { get; set; }       
    }
    

    public class MercuryWSSettings
    {
        public string SensomatixServiceEndPoint { get; set; }

        public string UseClientCetificate { get; set; }
    }
}
