using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLMayLanh
{
    class KetNoi
    {
        private SqlConnection connect;
        private string strConnect, strServerName, strDataBaseName, strUserName, strPassword;
        private DataSet dataSet;
        public KetNoi()
        {
            StrServerName = @"NIDOL\SQLEXPRESS";
            StrDataBaseName = "QLNhanvien";
            StrUserName = "sa";
            StrPassword = "123";
            StrConnection = @"Data Source=" + StrServerName + ";Initial Catalog=" + StrDataBaseName + ";Integrated Security=True";
            Connect = new SqlConnection(StrConnection);
            DataSet = new DataSet(StrDataBaseName);
        }
        public SqlDataReader getDataReader(string strSQL)
        {
            openConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connect;
            cmd.CommandText = strSQL;
            SqlDataReader data = cmd.ExecuteReader();
            return data;
        }

        public SqlDataAdapter getDataAdapter(string strSQL, string tableName)
        {
            openConnection();
            SqlDataAdapter ada = new SqlDataAdapter(strSQL, Connect);
            ada.Fill(DataSet, tableName);
            closeConnection();
            return ada;
        }

        public DataTable getDataTable(string strSQL)
        {
            openConnection();
            SqlDataAdapter ada = new SqlDataAdapter(strSQL, Connect);
            ada.Fill(DataSet);
            closeConnection();
            return DataSet.Tables[0];
        }

        public DataTable getDataTable(string strSQL, string tableName)
        {
            openConnection();
            SqlDataAdapter ada = new SqlDataAdapter(strSQL, Connect);
            ada.Fill(DataSet, tableName);
            closeConnection();
            return DataSet.Tables[tableName];
        }
        public KetNoi(string pStrCon, string dataSetName)
        {
            StrServerName = @"NIDOL\SQLEXPRESS";
            StrDataBaseName = dataSetName;
            StrUserName = "sa";
            StrPassword = "123";
            StrConnection = "@" + pStrCon;
            Connect = new SqlConnection();
            Connect.ConnectionString = StrConnection;
            DataSet = new DataSet(dataSetName);
        }
        public KetNoi(string strServerName, string strDataBaseName, string strUserName, string strPassword)
        {
            StrServerName = strServerName;
            StrDataBaseName = strDataBaseName;
            StrUserName = strUserName;
            StrPassword = strPassword;
            StrConnection = @"Data Source=" + StrServerName + ";Initial Catalog=" + StrDataBaseName + ";Integrated Security=True";
            Connect = new SqlConnection();
            Connect.ConnectionString = StrConnection;
            DataSet = new DataSet(strDataBaseName);
        }

        public void openConnection()
        {
            if (Connect.State == ConnectionState.Closed)
                Connect.Open();
        }

        public void closeConnection()
        {
            if (Connect.State == ConnectionState.Open)
                Connect.Close();
        }

        public void disposeConnection()
        {
            if (Connect.State == ConnectionState.Open)
                Connect.Close();
            Connect.Dispose();
        }

        public void updateToDataBase(string strSQL)
        {
            openConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connect;
            cmd.CommandText = strSQL;
            cmd.ExecuteNonQuery();
            closeConnection();
        }

        public int getCount(string strSQL)
        {
            openConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connect;
            cmd.CommandText = strSQL;
            int count = (int)cmd.ExecuteScalar();
            closeConnection();
            return count;
        }

        public bool checkForExistence(string strSQL)
        {
            return getCount(strSQL) > 0 ? true : false;
        }

        public string StrServerName
        {
            get { return strServerName; }
            set { strServerName = value; }
        }

        public string StrDataBaseName
        {
            get { return strDataBaseName; }
            set { strDataBaseName = value; }
        }

        public string StrUserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }

        public string StrPassword
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        public SqlConnection Connect
        {
            get { return connect; }
            set { connect = value; }
        }

        public string StrConnection
        {
            get { return strConnect; }
            set { strConnect = value; }
        }

        public DataSet DataSet
        {
            get { return dataSet; }
            set { dataSet = value; }
        }

       
      
       
    }
}
