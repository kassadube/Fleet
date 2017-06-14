using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Fleet.Data
{
    public class BaseRepository
    {
        private string _connectionString;
        private string _readConnectionString;
        private string _safetyConnectionString;
        
        protected string ConnectionString
        {
            get 
            {
                if (!string.IsNullOrEmpty(_connectionString)) return _connectionString;
                if (string.IsNullOrEmpty(Fleet.Configuration.ConfigManager.DBSETTINGS.ConnectionString))
                    Fleet.Configuration.ConfigManager.Init();
                _connectionString = Fleet.Configuration.ConfigManager.DBSETTINGS.ConnectionString;
                return _connectionString;
            }
        }

        protected string READConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(_readConnectionString)) return _readConnectionString;
                if (string.IsNullOrEmpty(Fleet.Configuration.ConfigManager.DBSETTINGS.READConnectionString))
                    Fleet.Configuration.ConfigManager.Init();
                _readConnectionString = Fleet.Configuration.ConfigManager.DBSETTINGS.READConnectionString;
                return _readConnectionString;
            }
        }
        protected string SafetyConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(_safetyConnectionString)) return _safetyConnectionString;
                if (string.IsNullOrEmpty(Fleet.Configuration.ConfigManager.DBSETTINGS.SafetyConnectionString))
                    Fleet.Configuration.ConfigManager.Init();
                _safetyConnectionString = Fleet.Configuration.ConfigManager.DBSETTINGS.SafetyConnectionString;
                return _safetyConnectionString;
            }
        }

       
    }
    public enum ApplicationContextMode
    {
        WEB, CONSOLE
    }
}
