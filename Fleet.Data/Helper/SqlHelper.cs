using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Security.Principal;
using System.Web.Security;
using Toolkit.Extensions;
using Fleet.Configuration;
using SiteLogger;
using System.Web;

namespace Fleet.Data
{
    public abstract class SqlHelper
    {

        //Database connection strings
        //public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.ConnectionStrings["SQLConnString1"].ConnectionString;
        //public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString2"].ConnectionString;
        //public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString3"].ConnectionString;
        //public static readonly string ConnectionStringProfile = ConfigurationManager.ConnectionStrings["SQLProfileConnString"].ConnectionString;

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                LogItem log = PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters,null);
                int val;
                try
                {
                     val = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ILogger elogger = LoggingFactory.GetLogger;
                    LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                    elogger.Error(elog);
                    throw ex;
                }
                cmd.Parameters.Clear();
                ILogger logger = LoggingFactory.GetDBLogger;
                log.Trace = (DateTime.Now - StartTime).ToString();
                logger.Custom(log);
                return val;
            }
        }

        public static int ExecuteNonQuery(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return trans == null ? ExecuteNonQuery(conn, cmdType, cmdText, commandParameters) : ExecuteNonQuery(trans, cmdType, cmdText, commandParameters);
        }
        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();

            LogItem log = PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters,null);
            int val;
            try
            {
                val = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                elogger.Error(elog);
                throw ex;
            }
            cmd.Parameters.Clear();
            ILogger logger = LoggingFactory.GetDBLogger;
            log.Trace = (DateTime.Now - StartTime).ToString();
            logger.Custom(log);
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            LogItem log = PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters,null);
            int val = 0;
            try
            {
                val = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems= true };
                elogger.Error(elog);

                elogger.Error(log);
                throw ex;
            }
            ILogger logger = LoggingFactory.GetDBLogger;
            log.Trace = (DateTime.Now - StartTime).ToString();
            logger.Custom(log);
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                // we use a try/catch here because if the method throws an exception we want to 
                // close the connection throw code, because no datareader will exist, hence the 
                // commandBehaviour.CloseConnection will not work
                try
                {
                    return ExecuteReader(conn, cmdType, cmdText, commandParameters);
                }
                catch(Exception ex)
                {
                    ILogger elogger = LoggingFactory.GetLogger;
                    LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                    elogger.Error(elog);
                    throw ex;
                }
            }
        }
        
        public static SqlDataReader ExecuteReader(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return trans == null ? ExecuteReader(conn, cmdType, cmdText, commandParameters) : ExecuteReader(trans, cmdType, cmdText, commandParameters);
        }
        
        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(conn, cmdType, cmdText,null, commandParameters);

        }
        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText,int? timeout, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            LogItem log = PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters, timeout);
            SqlDataReader rdr;
            try
            {
                 rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                elogger.Error(elog);
                throw ex;
            }
            cmd.Parameters.Clear();
            ILogger logger = LoggingFactory.GetDBLogger;
            log.Trace = (DateTime.Now - StartTime).ToString();
            logger.Custom(log);
            return rdr;

        }
        public static SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            LogItem log = PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters,null);
            SqlDataReader rdr;
            try
            {
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                elogger.Error(elog);
                throw ex;
            }
            cmd.Parameters.Clear();
            ILogger logger = LoggingFactory.GetDBLogger;
            log.Trace = (DateTime.Now - StartTime).ToString();
            logger.Custom(log);
            return rdr;

        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                LogItem log = PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters,null);
                object val;
                try
                {
                    val = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    ILogger elogger = LoggingFactory.GetLogger;
                    LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                    elogger.Error(elog);
                    throw ex;
                }
                cmd.Parameters.Clear();
                ILogger logger = LoggingFactory.GetDBLogger;
                log.Trace = (DateTime.Now - StartTime).ToString();
                logger.Custom(log);
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            LogItem log = PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters,null);
            object val;
            try
            {
                val = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                elogger.Error(elog);
                throw ex;
            }
            cmd.Parameters.Clear();
            ILogger logger = LoggingFactory.GetDBLogger;
            log.Trace = (DateTime.Now - StartTime).ToString();
            logger.Custom(log);
            return val;
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            LogItem log = PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters,null);
            XmlReader val ;
            try
            {
                val = cmd.ExecuteXmlReader();
            }
            catch (Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                elogger.Error(elog);
                throw ex;
            }
            cmd.Parameters.Clear();
            ILogger logger = LoggingFactory.GetDBLogger;
            log.Trace = (DateTime.Now - StartTime).ToString();
            logger.Custom(log);
            return val;
        }

        public static string ExecuteStringXmlReader(SqlConnection connection,SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            return trans == null ? ExecuteStringXmlReader(trans, cmdType, cmdText, commandParameters) : ExecuteStringXmlReader(connection, cmdType, cmdText, commandParameters);
            
        }
        
        public static string ExecuteStringXmlReader(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            LogItem log = PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters,null);
            XmlReader val;
            try
            {
                val = cmd.ExecuteXmlReader();
            }
            catch (Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                elogger.Error(elog);
                throw ex;
            }
            cmd.Parameters.Clear();
            StringBuilder res = new StringBuilder("");
            val.Read();
            while (val.ReadState != ReadState.EndOfFile)
            {
                res.Append(val.ReadOuterXml());
            }

            val.Close();
            ILogger logger = LoggingFactory.GetDBLogger;
            log.Trace = (DateTime.Now - StartTime).ToString();
            logger.Custom(log);
            return res.ToString();
        }
        
        public static string ExecuteStringXmlReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var StartTime = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            LogItem log = PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters,null);
            XmlReader val;
            try
            {
                val = cmd.ExecuteXmlReader();
            }
            catch (Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = cmdText, AddMissingItems = true };
                elogger.Error(elog);
                throw ex;
            }
            cmd.Parameters.Clear();
            string res = "";
            val.Read();
            while (val.ReadState != ReadState.EndOfFile)
            {
                res += val.ReadOuterXml();
            }

            val.Close();
            ILogger logger = LoggingFactory.GetDBLogger;
            log.Trace = (DateTime.Now - StartTime).ToString();
            logger.Custom(log);
            return res;
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        public static LogItem PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms, int? timeout)
        {

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = timeout.HasValue? timeout.Value: ConfigManager.DBSETTINGS.Timeout;
            if (trans != null)
            {
                int? userId = MemberShipHelper.GetCurrentUserId();
                using (SqlCommand cmdTmp = new SqlCommand())
                {
                    try
                    {
                        cmdTmp.Connection = conn;
                        cmdTmp.Transaction = trans;
                        cmdTmp.CommandText = "DECLARE @BinVar binary(128) SET @BinVar = CAST( {0} as binary(128) ) SET CONTEXT_INFO @BinVar".StringFormat(userId.Value);
                        cmdTmp.CommandType = CommandType.Text;
                        cmdTmp.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
                cmd.Transaction = trans;
            }

            cmd.CommandType = cmdType;
            StringBuilder paramsStr =new StringBuilder (cmdText);
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    paramsStr.AppendFormat(" {0} = {1},",parm.ParameterName,parm.Value);
                    cmd.Parameters.Add(parm);
                }
                
            }

           // ILogger logger = LoggingFactory.GetDBLogger;
          //  logger.Trace(string.Format("CMDCommand = {0}, UserId = {1} , _id = {2}, URL = {3}", cmdText, MemberShipHelper.GetCurrentUserId(), MemberShipHelper.GetCurrentMainId(),MemberShipHelper.GetCurrentContext()));
            LogItem log = new LogItem()
            {
                SiteId = MemberShipHelper.GetCurrentMainId(),
                UserId = MemberShipHelper.GetCurrentUserId(),
                AccountId = MemberShipHelper.GetCurrentAccountId(),
                Message = paramsStr.ToString(),
                Url = MemberShipHelper.GetCurrentContext(),
                LogType = LOGTYPE.DBTrace,
                
            };
            //logger.Custom(log);
            return log;
        }
    }

    
}

public static class MemberShipHelper
{
    public static int? GetCurrentUserId(this IPrincipal user)
    {
        return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').FirstOrDefault().StringToNumbaleInt();
    }
    public static int? GetCurrentUserId()
    {
        try
        {
            if (System.Web.HttpContext.Current == null) return null;
            IPrincipal user = System.Web.HttpContext.Current.User;
            if (!user.Identity.IsAuthenticated) return null;
            return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').FirstOrDefault().StringToNumbaleInt();
        }
        catch(Exception)
        {
            return null;
        }
    }
    public static int? GetCurrentAccountId(this IPrincipal user)
    {
        return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').ElementAtOrDefault(1).StringToNumbaleInt();
    }

    public static int? GetCurrentAccountId()
    {
        try
        {
            if (System.Web.HttpContext.Current == null) return null;
            IPrincipal user = System.Web.HttpContext.Current.User;
            return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').ElementAtOrDefault(1).StringToNumbaleInt();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static int? GetCurrentUserType()
    {
        if (System.Web.HttpContext.Current == null) return null;
        IPrincipal user = System.Web.HttpContext.Current.User;
        return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').ElementAtOrDefault(2).StringToNumbaleInt();
    }

    public static string GetCurrentMainId()
    {
        try
        {
            if (System.Web.HttpContext.Current == null) return null;
            IPrincipal user = System.Web.HttpContext.Current.User;
            return (user.Identity as FormsIdentity).Ticket.UserData.Split('|').ElementAtOrDefault(9);
        }
        catch(Exception)
        {
            return null;
        }
    }

    public static string GetCurrentContext()
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

    public static string GetUserIP()
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

    public static string GetCurrentBrowser()
    {
        try
        {
            if (System.Web.HttpContext.Current == null) return null;
            HttpBrowserCapabilities bc = System.Web.HttpContext.Current.Request.Browser;
            return string.Format("Browser = {0} , Version = {1}", bc.Browser, bc.Version);
            //return  System.Web.HttpContext.Current.Request.UserAgent;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
