using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Fleet.Model;
using Fleet.Data;
using System.Web.Hosting;
using System.Web;
using System.Web.Security;
using System.IO;
using Toolkit.Extensions;
using Fleet.Membership;
using System.Threading;
using System.Web.Mvc;
using System.Security.Principal;

namespace Fleet.Tests
{
    public static class ServiceFunctionHelper
    {
        

        public static void SetHttpContext(string userName, int langId, bool isSso)
        {
              //  IAuthenticationService _authService =ObjectFactory.GetInstance<IAuthenticationService>();
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt("379964E5BEFE31824725EC9AF6B71AFDE3F754B267E69959220E4F5414C3D48DF20CB7778604FFEB0DD6855205CFABBF139C2F2759901BB588E4B3B64367C26D45D2CB921C9D88F3F4816A72E7AD009B3A1229CC814B0197A9AC990A21E597D24556DE921C4A0EFDD331701A152DFC82B5C54BAC078C6CD2EA9BD5ECB8B5394B0F47BAC223326F1F40723FA3AAEEC23BC5184AE8E2866E13CEC651346DEA5C215D1E3B77B28480B94281AE1159860C555FF4EDFFB9A5EF944945A4BC5785D06D752AB5CA");
            SimpleWorkerRequest request = new SimpleWorkerRequest("", "", "", null, new StringWriter());
            HttpContext context = new HttpContext(request);
            HttpContext.Current = context;
            FormsAuthentication.Initialize();
            MembershipUnitTest T = new MembershipUnitTest();
            string mUser = T.GetUser(userName);
            string mainId = GenerateMainId();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(Configuration.ConfigManager.SETTINGS.LoginCookieExpiration), true, "{0}|{1}|{2}|{3}".StringFormat(mUser, 1, mainId, isSso ? 1 : 0), FormsAuthentication.FormsCookiePath);
            GenericPrincipal user = new GenericPrincipal(new FormsIdentity(ticket),null);
            HttpContext.Current.User = user;
            Thread.CurrentPrincipal = user;
            var x = System.Security.Principal.GenericPrincipal.Current;
          
        }

        public static void AddContextUserToController(Controller controller)
        {
            var controllerContext = new TestableControllerContext();
            var principal = new GenericPrincipal(Thread.CurrentPrincipal.Identity, null);
            var testableHttpContext = new TestableHttpContext
            {
                User = principal
            };

            controllerContext.HttpContext = testableHttpContext;
            controller.ControllerContext = controllerContext;
        }

        
       

        private static string GenerateMainId()
        {
            string ticks = DateTime.Now.Ticks.ToString();
            return ticks.Substring(ticks.Length - 7);

        }
    }

    public class TestableControllerContext : ControllerContext
    {
        public TestableHttpContext TestableHttpContext { get; set; }
    }

    public class TestableHttpContext : HttpContextBase
    {
        public override IPrincipal User { get; set; }
    }
}
