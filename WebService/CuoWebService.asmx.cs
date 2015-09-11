using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;


namespace ConnectUOService
{
    [ScriptService]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [WebService(Namespace = "http://www.connectuo.com/webservice/", Name = "ConnectUO Web Service")]
    public class CuoWebService : WebService
    {
        [WebMethod]
        [ScriptMethod]
        public bool TestConnection()
        {
            return true;
        }

        [WebMethod]
        public void TrackUsage(Guid guid)
        {
            SqlCommand cmd = new SqlCommand("EnsureStatisticsUser");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter("@Guid", SqlDbType.NVarChar, 36);
            parameter.Value = guid.ToString();

            cmd.Parameters.Add(parameter);

            cmd.Connection = CreateConnection();
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        [WebMethod]
        public void UpdateVersionStats(Guid guid, string version)
        {
            SqlCommand cmd = new SqlCommand("UpdateVersionStats");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter guidPparameter = new SqlParameter("@Guid", SqlDbType.NVarChar, 36);
            guidPparameter.Value = guid.ToString();

            cmd.Parameters.Add(guidPparameter);

            SqlParameter versionParameter = new SqlParameter("@Version", SqlDbType.NVarChar, 50);
            versionParameter.Value = version.ToString();

            cmd.Parameters.Add(versionParameter);

            cmd.Connection = CreateConnection();
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        [WebMethod]
        public void UpdatePlayStatistics(Guid guid, int id)
        {
            SqlCommand cmd = new SqlCommand("UpdatePlayStatistics");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter guidPparameter = new SqlParameter("@Guid", SqlDbType.NVarChar, 36);
            guidPparameter.Value = guid.ToString();

            cmd.Parameters.Add(guidPparameter);

            SqlParameter versionParameter = new SqlParameter("@ShardId", SqlDbType.Int);
            versionParameter.Value = id;

            cmd.Parameters.Add(versionParameter);

            cmd.Connection = CreateConnection();
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        [WebMethod]
        [ScriptMethod]
        public DataSet GetPublicServers()
        {
            SqlCommand cmd = new SqlCommand("GetServerList");
            DataSet dataSet = ExecuteDataSet("ArrayOfPublicShard", "PublicShard", cmd);

            return dataSet;
        }

        [WebMethod]
        public DataSet GetAllPatches()
        {
            SqlCommand cmd = new SqlCommand("GetAllPatches");
            DataSet dataSet = ExecuteDataSet("ArrayOfShardPatch", "ShardPatch", cmd);

            return dataSet;
        }

        [WebMethod]
        public DataSet GetServerInformation(int id)
        {
            SqlCommand cmd = new SqlCommand("GetServerById");

            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int);
            parameter.Value = id;

            cmd.Parameters.Add(parameter);

            DataSet dataSet = ExecuteDataSet("ArrayOfPublicShard", "PublicShard", cmd);

            return dataSet;
        }

        [WebMethod]
        public DataSet GetLatestVersion()
        {
            SqlCommand cmd = new SqlCommand("GetLatestVersion");
            DataSet dataSet = ExecuteDataSet("ArrayOfConnectUOVersion", "ConnectUOVersion", cmd);

            return dataSet;
        }

        [WebMethod]
        public DataSet GetPatches(int id)
        {
            SqlCommand cmd = new SqlCommand("GetPatches");

            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.Int);
            parameter.Value = id;

            cmd.Parameters.Add(parameter);
            DataSet dataset = ExecuteDataSet("ArrayOfShardPatch", "ShardPatch", cmd);

            return dataset;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connectuo_sql"].ConnectionString);
        }

        private DataSet ExecuteDataSet(string dataSetName, string tableName, CommandType commandType, SqlCommand cmd)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand.CommandType = commandType;
            adapter.SelectCommand.Connection = CreateConnection();

            DataSet dataSet = new DataSet(dataSetName);
            adapter.Fill(dataSet, tableName);

            return dataSet;
        }

        private DataSet ExecuteDataSet(string dataSetName, string tableName, SqlConnection connection, string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            DataSet dataSet = new DataSet(dataSetName);
            adapter.Fill(dataSet, tableName);

            return dataSet;
        }

        private DataSet ExecuteDataSet(string dataSetName, string tableName, SqlCommand cmd)
        {
            return ExecuteDataSet(dataSetName, tableName, CommandType.StoredProcedure, cmd);
        }
    }
}
