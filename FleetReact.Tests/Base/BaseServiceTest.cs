using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Fleet.Tests
{
    public class BaseServiceTest
    {
        public BaseServiceTest()
        {
           Bootstrapper.Run();
            SimpleWorkerRequest request = new SimpleWorkerRequest("", "", "", null, new StringWriter());
            HttpContext context = new HttpContext(request);
            HttpContext.Current = context;
            Fleet.Configuration.ConfigManager.Init();
            
        }
    }
}
