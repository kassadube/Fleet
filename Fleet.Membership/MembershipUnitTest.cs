using Fleet.Configuration;
using Fleet.Data;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using Toolkit.Extensions;
using Toolkit.Serialization;

namespace Fleet.Membership
{
    /// <summary>
    /// USED ONLY FOR UNIT TESTING
    /// </summary>
    public class MembershipUnitTest
    {
        
        private const string SQL_GET_USER = @"SELECT U.*,P.Permissions as TspPermissions, A.Name as AccountName,A.ParentAccount,A.Switches,A.Status as AccountStatus FROM Fleet..Users U JOIN Fleet..AccountsDetails A ON A.AccountId = U.AccountId  LEFT JOIN AMUsers P ON U.UserId = P.UserId WHERE UserName=@UserName";
        private string  connectionString = ConfigManager.DBSETTINGS.ConnectionString; //ConnectionStringSettings.ConnectionString;
            //Get encryption and decryption key information from the configuration.
            System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
         //   private MachineKeySection machineKey = cfg.GetSection("system.web/machineKey") as MachineKeySection;
      
        public string GetUser(string username)
        {
            string membershipUser = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlParameterList commandParams = new SqlParameterList();
            commandParams.Add("@UserName", SqlDbType.VarChar, username);
            try
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlConnection, CommandType.Text, SQL_GET_USER, commandParams.ToArray()))
                {
                    if (dr.Read())
                    {
                        NameValueCollection coll = dr.ConvertSqlDataReaderToNameValueCollection();

                        membershipUser = GetUserFromReader(coll);// GetUserFromReader(Serializer.DeSerialize<UserInfo>(dr));
                    }
                }
            }
            catch (Exception)
            {

            }
            return membershipUser;
        }
        private string GetUserFromReader(NameValueCollection user)
        {
            string userID = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", user["UserId"], user["AccountId"], user["UserType"], user["AccountName"], user["ParentAccount"], user["Switches"], user["Permissions"], user["TSPPermissions"], user["Login"]);
            string username = user["UserName"];
            string email = user["Email"];
            //string name = user["Name"];
            string passwordQuestion = String.Empty;
            string comment = user["Name"];
            bool isApproved = user["AccountStatus"] == "1";
            DateTime creationDate = user["CreationDat"].StringToDateTime();
            DateTime lastLoginDate = DateTime.Now;
            DateTime lastActivityDate = DateTime.Now;
            DateTime lastPasswordChangedDate = DateTime.Now;
            DateTime lastLockedOutDate = DateTime.Now;
            return userID;
            /*MembershipUser membershipUser = new MembershipUser(
             "FleetMembershipProvider",
             username,
             userID,
             email,
             passwordQuestion,
             comment,
             isApproved,
             isLockedOut,
             creationDate,
             lastLoginDate,
             lastActivityDate,
             lastPasswordChangedDate,
             lastLockedOutDate
              );

            return membershipUser;*/

        }
    }
}
