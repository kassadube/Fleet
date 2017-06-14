using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Toolkit.Extensions;
using Toolkit.Xml;


namespace Fleet.Tests
{
    internal class TestConfig
    {
       // public static DataGenerator DataGenerator;
        static TestConfig()
        {
            //DataGenerator = new DataGenerator();
            
        }

        public static XElement GetXMLItem(XElement xml, int itemId)
        {
           
            var b = (from a in xml.Descendants("ITEM")
                    where a.ReadIntAttr("id") == itemId
                     select a.Elements().FirstOrDefault()).FirstOrDefault();
            
            return b;
       
        }
    }
}
