using Fleet.Model;
using SiteLogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Serialization;

namespace Fleet.Data
{
    public class AccountRepository :BaseRepository, IAccountRepository
    {
        private const string SQL_GET_ACCOUNT_DETAILS = "Fleet.dbo.spAccountGet";
        public BaseResultInfo GetAccountDetails(int accountId)
        {
            DataResultInfo<FleetAccountInfo> result = new DataResultInfo<FleetAccountInfo>() { ResultObject = new FleetAccountInfo() };
            SqlParameterList commandParams = new SqlParameterList();
            commandParams.Add("@AccountId", SqlDbType.Int, accountId);
            try
            {
                using (SqlConnection conn = new SqlConnection(base.ConnectionString))
                {
                    using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, SQL_GET_ACCOUNT_DETAILS, commandParams.ToArray()))
                    {
                        if (dr.Read())
                        {
                            //XElement x = dr.ConvertSqlDataReaderToXElement();
                            // LoggingFactory.GetLogger.Info("ACCOUNT XML = {0}".StringFormat(x.ToString()));
                            FleetAccountInfo acc = Serializer.DeSerialize<FleetAccountInfo>(dr);
                            result.ResultObject = acc;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ILogger elogger = LoggingFactory.GetLogger;
                LogItem elog = new LogItem() { LogType = LOGTYPE.Error, Message = ex.Message, Trace = SQL_GET_ACCOUNT_DETAILS, AddMissingItems = true };
                elogger.Error(elog);
                result.Error = new BaseErrorInfo();
            }
            return result;
        }

    }
}
