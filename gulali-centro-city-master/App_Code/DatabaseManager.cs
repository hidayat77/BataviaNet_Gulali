using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;

namespace General
{
    /// <summary>Defines methods to access database</summary>
    public class DatabaseManager : IDisposable
    {
        /// <summary>private class variable to represent sqlcommand object</summary>
        private SqlCommand sqlCommand = null;

        /// <summary>private class variable to represent sqlconnection object</summary>
        private SqlConnection sqlConnection = null;

        /// <summary>Represents a Transact-SQL statement or stored procedure to execute against the database</summary>
        public SqlCommand Command
        {
            get { return this.sqlCommand; }
        }

        /// <summary>Represents a connection to the database</summary>
        public SqlConnection Connection
        {
            get { return this.sqlConnection; }
        }

        /// <summary>Creates a new database object with default connection string</summary>
        public DatabaseManager()
        {
            this.OpenConnection(ConfigurationManager.ConnectionStrings["DevelopmentConnectionString"].ConnectionString);
        }

      
        /// <summary>Creates a new database with specified connection string</summary>
        /// <param name="connectionString">connection string for the sql connection object</param>
        public DatabaseManager(string connectionString)
        {
            this.OpenConnection(connectionString);
        }

        #region "IDisposable Support"

        /// <summary>Releases all resources for the database connection</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases all resources for the database connection</summary>
        /// <param name="isDisposing">close database connection if true</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (true == isDisposing)
            {
                this.CloseConnection();
            }
        }

        ~DatabaseManager()
        {
            this.Dispose(false);
        }

        #endregion

        /// <summary>Initialize database connection</summary>
        /// <param name="connectionString"></param>
        private void OpenConnection(string connectionString)
        {
            this.sqlConnection = new SqlConnection();
            this.sqlConnection.ConnectionString = connectionString;
            this.sqlConnection.Open();

            this.sqlCommand = new SqlCommand();
            this.sqlCommand.Connection = this.sqlConnection;
        }

        /// <summary>Close database connection and release resources</summary>
        private void CloseConnection()
        {
            if (null != this.sqlConnection)
            {
                if (ConnectionState.Closed != this.sqlConnection.State)
                {
                    this.sqlConnection.Close();
                    this.sqlConnection.Dispose();
                }
            }

            if (null != this.sqlCommand)
            {
                this.sqlCommand.Dispose();
            }
        }

        /// <summary>Loads DataSet from the SqlCommand</summary>
        /// <param name="dataSetIn"></param>
        public void LoadDataSet(DataSet dataSetIn)
        {
            new SqlDataAdapter(this.sqlCommand).Fill(dataSetIn);
        }

        /// <summary>Loads DataSet from the specified sql query</summary>
        /// <param name="query">sql query</param>
        /// <param name="dataSetIn"></param>
        public void LoadDataSet(string query, DataSet dataSetIn)
        {
            this.sqlCommand.CommandText = query;
            this.sqlCommand.CommandType = CommandType.Text;
            new SqlDataAdapter(this.sqlCommand).Fill(dataSetIn);
        }

        /// <summary>Loads DataTable from the SqlCommand</summary>
        /// <param name="dataTableIn"></param>
        public void LoadDataTable(DataTable dataTableIn)
        {
            new SqlDataAdapter(this.sqlCommand).Fill(dataTableIn);
        }

        /// <summary>Loads DataTable from the specified sql query</summary>
        /// <param name="query">sql query</param>
        /// <param name="dataTableIn"></param>
        public void LoadDataTable(string query, DataTable dataTableIn)
        {
            this.sqlCommand.CommandText = query;
            this.sqlCommand.CommandType = CommandType.Text;
            new SqlDataAdapter(this.sqlCommand).Fill(dataTableIn);
        }


        /// <summary>Loads DataTable from the specified sql query</summary>
        /// <param name="query">sql query</param>
        /// <param name="dataTableIn"></param>
        public void LoadDataTableForStoredProcedure(string storedProcedureName, DataTable dataTableIn)
        {
            this.sqlCommand.CommandText = storedProcedureName;
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            new SqlDataAdapter(this.sqlCommand).Fill(dataTableIn);
        }



        /// <summary>Gets DataReader from the specified sql query</summary>
        /// <param name="query"></param>
        /// <param name="dataReaderIn"></param>
        public SqlDataReader GetDataReader(string query)
        {
            this.sqlCommand.CommandText = query;
            this.sqlCommand.CommandType = CommandType.Text;
            return this.sqlCommand.ExecuteReader();
        }
        public SqlDataReader GetDataReader()
        {
           return this.sqlCommand.ExecuteReader();
        }

        public int getScopeIdentity()
        {
            return Convert.ToInt16(this.sqlCommand.ExecuteScalar());

        }

        public string simpleQueryWithStringReturn()
        {
            return sqlCommand.ExecuteScalar().ToString();
        }
        public void simpleInsertQuery()
        {
            this.sqlCommand.ExecuteNonQuery();

        }

    }
}
