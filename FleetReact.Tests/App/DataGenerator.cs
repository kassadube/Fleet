using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Fleet.Model;
using Toolkit.Xml;
using Toolkit.Extensions;
using Toolkit.Serialization;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace Fleet.Tests
{
    public class DataGenerator
    {
        public static string IOReportFile;
        public static string EXReportFile;
        public static string VehicleFile;
        public static string DriverFile;
        public static string AlertFile;
        public static string AccountFile;
        private static XElement IO_XML;
        private static XElement EX_XML;
        private static XElement V_XML;
        private static XElement D_XML;
        private static XElement A_XML;
        private static XElement AC_XML;

        static DataGenerator()
        {
            IOReportFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(),"../../TEST_DATA/IOReportDATA.xml");
            EXReportFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "../../TEST_DATA/EXReportDATA.xml");
            VehicleFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "../../TEST_DATA/VehicleDATA.xml");
            DriverFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "../../TEST_DATA/DriverDATA.xml");
            AlertFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "../../TEST_DATA/AlertDATA.xml");
            AccountFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "../../TEST_DATA/AccountDATA.xml");
            IO_XML = XElement.Load(IOReportFile);
            EX_XML = XElement.Load(EXReportFile);
            V_XML = XElement.Load(VehicleFile);
            D_XML = XElement.Load(DriverFile);
            A_XML = XElement.Load(AlertFile);
            AC_XML = XElement.Load(AccountFile);
        }

        public static NameValueCollection GetIOReportFormCollection(int itemId)
        {
            return GetFormCollection(itemId, IO_XML);
        }

        public static NameValueCollection GetEXReportFormCollection(int itemId)
        {
            return GetFormCollection(itemId, EX_XML);
        }

        public static NameValueCollection GetVehicleCollection(int itemId)
        {
            return GetFormCollection(itemId, V_XML);
        }

        public static NameValueCollection GetDriverCollection(int itemId)
        {
            return GetFormCollection(itemId, D_XML);
        }

        public static NameValueCollection GetAlertCollection(int itemId)
        {
            return GetFormCollection(itemId, A_XML);
        }
        public static NameValueCollection GetAccountCollection(int itemId)
        {
            return GetFormCollection(itemId, AC_XML);
        }

        private static NameValueCollection GetFormCollection(int itemId, XElement xml)
        {
            XElement x = TestConfig.GetXMLItem(xml, itemId);
            return x.ToNameValueCollection();
        }

    }
}
