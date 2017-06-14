using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Toolkit.Extensions;
using Toolkit.Xml;
using System.Security.Principal;
using System.Web.Security;


namespace Fleet.Tests
{
    public static class Helper
    {
       
        public static int? GetCurrentUserType(this IPrincipal user)
        {
            return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').ElementAtOrDefault(2).StringToNumbaleInt();
        }

        public static string GetCurrentUserPassword(this IPrincipal user)
        {
            return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').ElementAtOrDefault(8);
        }
    }
}
