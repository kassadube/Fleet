using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;

namespace SiteLogger
{
    public class LoggerHelper
    {
        public static void AddMissingItems(LogItem item)
        {
            if (string.IsNullOrEmpty(item.SiteId))
                item.SiteId = GetCurrentMainId();
            if (string.IsNullOrEmpty(item.Url))
                item.Url = GetCurrentUrl();
            if (!item.AccountId.HasValue)
                item.AccountId = GetCurrentAccountId();
            if (!item.UserId.HasValue)
                item.UserId = GetCurrentUserId();

            if (string.IsNullOrEmpty(item.UserAgent))
                item.UserAgent = GetCurrentUserAgent();
        }

        public static string GetCurrentMainId()
        {
            try
            {
                if (System.Web.HttpContext.Current == null) return null;
                IPrincipal user = System.Web.HttpContext.Current.User;
                return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').ElementAtOrDefault(9);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetCurrentUrl()
        {
            try
            {
                if (System.Web.HttpContext.Current == null) return null;
                return System.Web.HttpContext.Current.Request.RawUrl;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetCurrentUserAgent()
        {
            try
            {
                if (System.Web.HttpContext.Current == null) return null;
                HttpBrowserCapabilities bc = System.Web.HttpContext.Current.Request.Browser;
                return string.Format("Browser = {0} , Version = {1}, IsMobile = {2}, IP = {3}", bc.Browser, bc.Version, bc.IsMobileDevice, GetUserIP());
                //return  System.Web.HttpContext.Current.Request.UserAgent;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int? GetCurrentUserId()
        {
            try
            {
                if (System.Web.HttpContext.Current == null) return null;
                IPrincipal user = System.Web.HttpContext.Current.User;
                if (!user.Identity.IsAuthenticated) return null;

                string val = ((user.Identity as FormsIdentity).Ticket.UserData.Split('|').FirstOrDefault());
                if (string.IsNullOrEmpty(val))
                    return null;
                else
                    return int.Parse(val);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int? GetCurrentAccountId()
        {
            try
            {
                if (System.Web.HttpContext.Current == null) return null;
                IPrincipal user = System.Web.HttpContext.Current.User;
                if (!user.Identity.IsAuthenticated) return null;

                string val = ((user.Identity as FormsIdentity).Ticket.UserData.Split('|').ElementAtOrDefault(1));
                if (string.IsNullOrEmpty(val))
                    return null;
                else
                    return int.Parse(val);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string GetUserIP()
        {
            try
            {
                if (System.Web.HttpContext.Current == null) return null;
                string ipList = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipList))
                {
                    return ipList.Split(',')[0];
                }
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
