using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ExamMethodLibrary.DAL
{
    public class DBHelper
    {
        private static DBHelper _instance;
        private static readonly object _lock = new object();
        private readonly string _connectionString;
        private SqlConnection _connection;

        private DBHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["YourDBConnection"].ConnectionString;
            _connection = new SqlConnection(_connectionString);
        }

        public static DBHelper Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new DBHelper());
                }
            }
        }

        public SqlConnection GetConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }

}
