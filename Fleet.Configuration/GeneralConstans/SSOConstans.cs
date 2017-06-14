using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet.Configuration
{
    public class SSOConstans
    {
        private static string _sso_users_session_id;
        private static string _sso_users_cookie_id;

        static SSOConstans()
        {
            _sso_users_session_id = "SSO_USERS";
            _sso_users_cookie_id = "SSOUSERCOOKIE{0}";
        }

        public static string SSO_USERS_SESSION_ID { get { return _sso_users_session_id; } }
        public static string SSO_USERS_COOKIE_ID { get { return _sso_users_cookie_id; } }
    }
}
