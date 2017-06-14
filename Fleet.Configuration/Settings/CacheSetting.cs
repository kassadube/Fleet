using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Fleet.Configuration
{
    public class CACHESetting
    {
        public string ACCOUNT;
        public string ACCOUNT_FLEET_ACCOUNT_DETAILS;
        public string ACCOUNT_GROUPS;
        public string ACCOUNT_GEOFENCE;
        public string ACCOUNT_LAYERS;
        public string ACCOUNT_USERS;
        public string ACCOUNT_DRIVERS;
        public string ACCOUNT_TEMPLATES;
        public string ACCOUNT_CUSTOM_FIELDS;
        public string ACCOUNT_CUSTOM_FIELDS_ITEMS;
        public string ACCOUNT_ALERTS;
        public string ACCOUNT_ALERT_VEHICLES;
        public string ACCOUNT_ALERT_DEFINITION;
        public string ACCOUNT_USER_MENU;
        public string ACCOUNT_SHIFTS;
        public string ACCOUNT_VEHICLES_XML;
        public string ACCOUNT_POLYGONS;
        public string ACCOUNT_VEHICLE_GROUPS;
        public string ACCOUNT_DRIVER_GROUPS;
        public string ACCOUNT_SUB_ACCOUNTS;
        public string ACCOUNT_VEHICLES;

         public string VEHICLE_DETAILS;
         public string VEHICLE_DETAILS_IN_ACCOUNT;
         public string GROUP_VEHICLES;
         public string GROUP_DRIVERS;
       
        public string VEHICLE_COLORS;
        public string VEHICLE_MANUFECTURERS;
        public string VEHICLE_MANUFECTURE_MODELS;
        public string VEHICLE_ICONS;
        public string MDT_TYPES;
        public string LICENSE_TYPE_NAMES;
        public string SYSTEM_PARAM;
        
        public string ALL_ACCOUNTS;

        //IOManage Configuration
        public string IO_ATTRIBUTES;
        public string IO_CONFIGURATION;
        public string IO_GROUP;
        public string UNIT_IO_CONFIGURATION_NAMES;
        public string CMU_SUB_TYPE_LIST;
        public string CMU_MODEL_TYPE_LIST;
        public string UNIT_ALERT_TYPE_LIST;
        public string PARENT_ACCOUNT_CACHE_SUB_ACCOUNT_LIST;
        public string FLEET_REPORTS;
        public string IO_REPORT;

        public CACHESetting()
        {
            string prefix = System.Configuration.ConfigurationManager.AppSettings["CACHE_PREFIX"];
            if (string.IsNullOrEmpty(prefix)) prefix = "";
            ACCOUNT = prefix +  "_CACHE_ACCOUNT_ID_{0}";
            ACCOUNT_FLEET_ACCOUNT_DETAILS = prefix + "_CACHE_ACCOUNT_FLEET_ACCOUNT_DETAILS_{0}";
            ACCOUNT_GROUPS = prefix + "_CACHE_ACCOUNT_GROUPS_{0}";
            ACCOUNT_GEOFENCE = prefix + "_CACHE_ACCOUNT_GEOFENCE_{0}";
            ACCOUNT_LAYERS = prefix + "_CACHE_ACCOUNT_LAYERS_{0}";
            ACCOUNT_USERS = prefix + "_CACHE_ACCOUNT_USERS_{0}";
            ACCOUNT_DRIVERS = prefix + "_CACHE_ACCOUNT_DRIVERS_{0}";
            ACCOUNT_TEMPLATES = prefix + "_CACHE_ACCOUNT_TEMPLATES_{0}_LANG_{1}";
            ACCOUNT_CUSTOM_FIELDS = prefix + "_CACHE_ACCOUNT_CUSTOM_FIELDS_{0}";
            ACCOUNT_CUSTOM_FIELDS_ITEMS = prefix + "_CACHE_ACCOUNT_CUSTOM_FIELDS_ITEMS_{0}_{1}";
            ACCOUNT_ALERTS = prefix + "_CACHE_ACCOUNT_ALERTS_{0}";
            ACCOUNT_ALERT_VEHICLES = prefix + "_CACHE_ACCOUNT_ALERT_VEHICLES_{0}";
            ACCOUNT_ALERT_DEFINITION = prefix + "_CACHE_ACCOUNT_ALERT_DEFINITION_{0}";
            ACCOUNT_USER_MENU = prefix + "_CACHE_ACCOUNT_USER_MENU_{0}_{1}";
            ACCOUNT_SHIFTS = prefix + "_CACHE_ACCOUNT_SHIFTS_{0}";
            ACCOUNT_VEHICLES_XML = prefix + "_CACHE_ACCOUNT_VEHICLES_XML_{0}";
            ACCOUNT_POLYGONS = prefix + "_CACHE_ACCOUNT_POLYGONS_{0}";
            ACCOUNT_VEHICLE_GROUPS = prefix + "_CACHE_ACCOUNT_VEHICLE_GROUPS_{0}";
            ACCOUNT_DRIVER_GROUPS = prefix + "_CACHE_ACCOUNT_DRIVER_GROUPS_{0}";
            ACCOUNT_SUB_ACCOUNTS = prefix + "_CACHE_ACCOUNT_SUB_ACCOUNTS_{0}";
            ACCOUNT_VEHICLES = prefix + "_CACHE_ACCOUNT_VEHICLES_{0}";

            VEHICLE_DETAILS = prefix + "_CACHE_VEHICLE_DETAILS_{0}";
            VEHICLE_DETAILS_IN_ACCOUNT = prefix + "_CACHE_VEHICLE_DETAILS_{0}_{1}";//vehicleId,accountId
            GROUP_VEHICLES = prefix + "_CACHE_ACCOUNT_GROUP_VEHICLES_{0}_{1}";
            GROUP_DRIVERS = prefix + "_CACHE_GROUP_DRIVERS_{0}";


            IO_ATTRIBUTES = prefix +  "_CACHE_IO_ATTRIBUTES_LANG_ID_{0}";
            IO_CONFIGURATION =prefix +   "_CACHE_IO_CONFIGURATION_LANG_ID_{0}";
            IO_GROUP = prefix +  "_CACHE_IO_GROUP_{0}";
            VEHICLE_COLORS = prefix +  "_CACHE_VEHICLE_COLOR_{0}"; 
            VEHICLE_MANUFECTURERS = prefix +  "_CACHE_VEHICLE_MANUFECTURERS_{0}";
            VEHICLE_MANUFECTURE_MODELS = prefix +  "_CACHE_VEHICLE_MANUFECTURE_MODELS_{0}_{1}";// manufectureId
            VEHICLE_ICONS = prefix +  "_CACHE_VEHICLE_ICONS";
            MDT_TYPES = prefix +  "_CACHE_MDT_TYPES";
            LICENSE_TYPE_NAMES = prefix +  "_CACHE_LICENSE_TYPE_NAMES_{0}_{1}"; // 0 = accountId, 1 = langId
            UNIT_IO_CONFIGURATION_NAMES = prefix +  "_CACHE_UNIT_IO_CONFIGURATION_NAMES_{0}"; // unitSysId value
            CMU_MODEL_TYPE_LIST = prefix +  "_CACHE_CMU_MODEL_TYPE_LIST"; // CMU Model type list(iomanage)
            CMU_SUB_TYPE_LIST = prefix +  "_CACHE_CMU_SUB_TYPE_LIST_{0}"; // CMU Sub type list(iomanage)
            ALL_ACCOUNTS = prefix +  "_CACHE_ALL_ACCOUNTS"; // ALL account use for tree in admin section
            SYSTEM_PARAM = prefix +  "_CACHE_SYSTEM_PARAM"; // All SystemParam
            UNIT_ALERT_TYPE_LIST = prefix +  "_CACHE_UNIT_ALERT_TYPE_LIST_{0}_{1}"; // 
            PARENT_ACCOUNT_CACHE_SUB_ACCOUNT_LIST = prefix +  "_CACHE_PARENT_ACCOUNT_LIST_{0}"; //// 0 = accountId a list of subaccount ids exists in cache 
            FLEET_REPORTS = prefix +  "FLEET_REPORTS_CACHE";
            IO_REPORT = prefix + "IO_REPORT_TABLE_{0}_{1}"; //IO_REPORT_TABLE_{accountId}_{resId} resId is the main id of the last execution use in order to get chart and table one after another
        }

        public bool USE_CACHE { get { return ConfigurationManager.AppSettings["USE_CACHE"] == "1"; } }
    }
}
