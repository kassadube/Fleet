using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Threading;


namespace Fleet.Configuration
{
    public class ConfigManager
    {
        private static GeneralSettings _settings;


        public static void Init()
        {
            
            Init("");
        }


        public static void Init(string iniFilePath)
        {
            _settings = new GeneralSettings();
            //IniFile ini = new IniFile(iniFilePath);

            try
            {                

                _settings.DbSettings.ConnectionString =  System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDB"].ConnectionString;
                _settings.DbSettings.READConnectionString =  System.Configuration.ConfigurationManager.ConnectionStrings["READ_ApplicationDB"].ConnectionString;
                _settings.DbSettings.SafetyConnectionString = _settings.DbSettings.ConnectionString;                    
                
                if (System.Configuration.ConfigurationManager.ConnectionStrings["SafetyDB"] != null)
                    _settings.DbSettings.SafetyConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SafetyDB"].ConnectionString;
                
                if (System.Configuration.ConfigurationManager.ConnectionStrings["TableStorageDB"] != null)
                    _settings.DbSettings.TableStorageConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TableStorageDB"].ConnectionString;
                
                if (System.Configuration.ConfigurationManager.ConnectionStrings["RedisDB"] != null)
                    _settings.DbSettings.RedisConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RedisDB"].ConnectionString;
                if (System.Configuration.ConfigurationManager.ConnectionStrings["BlobStorageDB"] != null)
                {
                    _settings.DbSettings.BlobStorageConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BlobStorageDB"].ConnectionString;
                }
                InitRSSettings();
                if (HttpContext.Current != null)
                {

                    //InitRSSettings();
                    _settings.DateFormat = System.Configuration.ConfigurationManager.AppSettings["DATE_FORMAT"];
                    if (string.IsNullOrEmpty(_settings.DateFormat))
                    {
                        if (Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern.StartsWith("d"))
                            _settings.DateFormat = "dd/MM/y H:mm:ss";
                        else
                            _settings.DateFormat = "MM/dd/y H:mm:ss";
                    }
                    _settings.LoginCookieExpiration = System.Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["LOGIN_COOKIE_EXPIRATION"]);
                    if (_settings.LoginCookieExpiration == 0) _settings.LoginCookieExpiration = 24;
                    string MaxVehiclesPerReport = System.Configuration.ConfigurationManager.AppSettings["MaxVehiclesPerReport"];
                    int tempMax = 0;
                    if (int.TryParse(MaxVehiclesPerReport, out tempMax))
                        _settings.RsSettings.MaxVehiclesPerReport = tempMax;
                    else
                        _settings.RsSettings.MaxVehiclesPerReport = 222; // the max allowed vehicles

                    _settings.RsSettings.Credentials.Domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                    _settings.RsSettings.Credentials.UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                    _settings.RsSettings.Credentials.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                     string isBlobActive=_settings.IsBlobStorageActive = System.Configuration.ConfigurationManager.AppSettings["IsBlobStrageActive"];
                     if (string.IsNullOrEmpty(isBlobActive))
                     {
                         _settings.IsBlobStorageActive = "true";
                     }
                     else
                     {
                         _settings.IsBlobStorageActive = System.Configuration.ConfigurationManager.AppSettings["IsBlobStrageActive"].ToString() == "1" ? "true" : "false";
                     }
                    _settings.SafetySettings.LoginUrl = System.Configuration.ConfigurationManager.AppSettings["SSOTransferUrl"];
                    _settings.SafetySettings.TransferUrl = System.Configuration.ConfigurationManager.AppSettings["TransferUrl"];                   
                    _settings.MercuryWsSettings.SensomatixServiceEndPoint = System.Configuration.ConfigurationManager.AppSettings["SSOServiceEndPoint"];


                    string compress = System.Configuration.ConfigurationManager.AppSettings["USE_COMPRESSION"];
                    _settings.UseCompression = (compress == null || compress == "1");
                    string session = System.Configuration.ConfigurationManager.AppSettings["USE_SESSION"];
                    _settings.UseSession = (session == null || session == "1");
                    string fleetViewSession = System.Configuration.ConfigurationManager.AppSettings["USE_FLEET_VIEW_SESSION"];
                    _settings.UseFleetViewSession = (fleetViewSession == null || fleetViewSession == "1");
                    
                    //CACHE SECTION
                    string cache = System.Configuration.ConfigurationManager.AppSettings["USE_CACHE"];
                    _settings.UseCache = (cache == null || cache == "1");
                    
                    string azureCache = System.Configuration.ConfigurationManager.AppSettings["USE_AZURE_CACHE"];
                    _settings.UseAzureCache = (azureCache == "1");
                    
                    string useStorageTableCache = System.Configuration.ConfigurationManager.AppSettings["USE_STORAGE_TABLE_CACHE"];
                    _settings.UseStorageTableCache = (useStorageTableCache == "1");

                    string cachePre = System.Configuration.ConfigurationManager.AppSettings["CACHE_PREFIX"];
                    _settings.CachePrefix = cachePre == null ? "" : cachePre;
                   
                    int cacheTimeout = 0;
                    if (int.TryParse(System.Configuration.ConfigurationManager.AppSettings["CACHE_TIMEOUT"], out cacheTimeout))
                    {
                        _settings.CacheTimeout = cacheTimeout;
                    }
                    else
                        _settings.CacheTimeout = 120;
                    // END CACHE SECTION
                    string kml = System.Configuration.ConfigurationManager.AppSettings["KML_ENABLE"];
                    _settings.KmlEnable = (kml != null && kml == "1");

                    _settings.GoogleMapAPI = System.Configuration.ConfigurationManager.AppSettings["GoogleMapAPI"];

                    _settings.PAI_SERVER = System.Configuration.ConfigurationManager.AppSettings["PAI_SERVER"];
                    if (string.IsNullOrEmpty(_settings.PAI_SERVER))
                    {
                        _settings.PAI_SERVER = string.Format("http://{0}", HttpContext.Current.Request.Url.Authority);
                    }
                    else
                    {
                        if (!_settings.PAI_SERVER.ToLower().StartsWith("http"))
                            _settings.PAI_SERVER = "http://" + _settings.PAI_SERVER;
                        if (_settings.PAI_SERVER.EndsWith("/"))
                        {
                            _settings.PAI_SERVER = _settings.PAI_SERVER.Remove(_settings.PAI_SERVER.Length - 1, 1);
                        }
                    }
					_settings.LogServer = System.Configuration.ConfigurationManager.AppSettings["LOG_SERVER"];

					_settings.LogType = System.Configuration.ConfigurationManager.AppSettings["LOG_TYPE"];
                    if (string.IsNullOrEmpty(_settings.LogType))
                        _settings.LogType = "1";

                    string sites = System.Configuration.ConfigurationManager.AppSettings["FLEET_SITES"];
                    if (string.IsNullOrEmpty(sites))
                        _settings.FleetSites = new List<string>();
                    else
                    {
                        string[] fleetSites = sites.Split(',');
                        _settings.FleetSites = fleetSites.ToList();
                    }

                    string fleetViewUsers = System.Configuration.ConfigurationManager.AppSettings["FEET_VIEW_USERS_OVERRIDE"];
                    _settings.FleetViewUsersOveride = new List<int>();
                    if (!string.IsNullOrEmpty(fleetViewUsers))
                    {
                        try{
                        string[] arrFleetViewUsers = fleetViewUsers.Split(',');
                        foreach (var item in arrFleetViewUsers)
                        {
                            _settings.FleetViewUsersOveride.Add(int.Parse(item));
                        }
                        }
                        catch(Exception)
                        {
                            _settings.FleetViewUsersOveride = new List<int>();
                        }
                    }
                    try
                    {
                        string resetTaskDataTime = System.Configuration.ConfigurationManager.AppSettings["RESET_TASK_DATA"];
                        if (string.IsNullOrEmpty(resetTaskDataTime))
                            resetTaskDataTime = "05:00";
                        _settings.ResetTaskDataTime = resetTaskDataTime.Split(':');
                    }
                    catch(Exception)
                    {
                        _settings.ResetTaskDataTime = new string[] { "6", "0" };
                    }


                    string xframeoptions = System.Configuration.ConfigurationManager.AppSettings["x-frame-options"];
                    if (!string.IsNullOrEmpty(xframeoptions))
                        _settings.xFrameOptions = xframeoptions;

                    if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SpecialCharRegex"]))
                    {
                        System.Configuration.ConfigurationManager.AppSettings["SpecialCharRegex"] = "^*[-&.%@/:_()=,?\\p{L}\\p{Nl}\\p{Nd}]{0,1000}$";
                        
                    }
                    _settings.SpecialCharRegex = System.Configuration.ConfigurationManager.AppSettings["SpecialCharRegex"];

                    if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["BlobAccountName"]))
                    {
                        System.Configuration.ConfigurationManager.AppSettings["BlobAccountName"] = "pointerpoc1";
                        
                    }
                    _settings.BlobAccountName = System.Configuration.ConfigurationManager.AppSettings["BlobAccountName"];


                    if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["BlobContainerName"]))
                    {
                        System.Configuration.ConfigurationManager.AppSettings["BlobContainerName"] = "webpbrshared";

                    }
                    _settings.BlobContainerName = System.Configuration.ConfigurationManager.AppSettings["BlobContainerName"];

                    if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IsScanEngineActive"]))
                    {
                        System.Configuration.ConfigurationManager.AppSettings["IsScanEngineActive"] = "0";

                    }
                    _settings.IsScanEngineActive = System.Configuration.ConfigurationManager.AppSettings["IsScanEngineActive"]=="1";

                }
            }                
            catch (Exception)
            {
            }
        }

        private static void InitRSSettings()
        {
            _settings.RsSettings.FleetReports = new List<int>();
            string timeout = System.Configuration.ConfigurationManager.AppSettings["DATABASE_TIMEOUT"];
            string DWtimeout = System.Configuration.ConfigurationManager.AppSettings["DW_REPORTS_DATABASE_TIMEOUT"];
            _settings.DbSettings.Timeout = string.IsNullOrEmpty(timeout) ? 30 : System.Convert.ToInt32(timeout);
            _settings.DbSettings.DWTimeout = string.IsNullOrEmpty(DWtimeout) ? 120 : System.Convert.ToInt32(DWtimeout);
            _settings.RsSettings.ReportServer = System.Configuration.ConfigurationManager.AppSettings["REPORT_SERVER"];
            if (string.IsNullOrEmpty(_settings.RsSettings.ReportServer)) return;

            _settings.RsSettings.ReportServiceUrl = Path.Combine(_settings.RsSettings.ReportServer, "ReportService2005.asmx");
            _settings.RsSettings.ReportServiceExecutionUrl = Path.Combine(_settings.RsSettings.ReportServer, "ReportExecution2005.asmx");
            string fleetReports = System.Configuration.ConfigurationManager.AppSettings["FLEET_REPORTS"];            
            if(!string.IsNullOrEmpty(fleetReports))
            {
               string[] reportsId= fleetReports.Split(',');
               foreach (var item in reportsId)
               {
                   try
                   {
                       _settings.RsSettings.FleetReports.Add(System.Convert.ToInt32(item));
                   }
                   catch (Exception)
                   {
                   }
               }
            }
            string excelFormat = System.Configuration.ConfigurationManager.AppSettings["EXCEL_FORMAT"];
            if(string.IsNullOrEmpty(excelFormat))
            {
                _settings.RsSettings.ExcelFormat = "EXCEL";
            }
            else
            {
                _settings.RsSettings.ExcelFormat = excelFormat;
            }
        }

        public static GeneralSettings SETTINGS
        {
            get
            {
                if (_settings == null)
                    Init();
                return _settings;
            }
        }

        public static DBSettings DBSETTINGS
        {
            get
            {
                return SETTINGS.DbSettings;
            }
        }

        public static RSSettings RSSETTINGS
        {
            get
            {
                return SETTINGS.RsSettings;
            }
        }

        private int _cryptoSeed;

        public int CryptoSeed
        {
            get {
                if (_cryptoSeed == 0)
                {
                    int unixTimeStamp = Convert.ToInt32(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                }
                return _cryptoSeed; 
            }
            set { _cryptoSeed = value; }
        }
        

        
    }
}
