using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WebApi.Data
{
    public static class ConnectionFactory
    {
        public static string CONN_STRING = "";
        /// <summary>
        /// global variate, default database
        /// </summary>
        public static string SQLTYPE = "MSSQL";
        public static IDbConnection CreateConnection<T>() where T : IDbConnection, new()
        {
            IDbConnection connection = new T();
            connection.ConnectionString = CONN_STRING;
            connection.Open();
            return connection;
        }
        /// <summary>
        /// Create connection by SQLTYPE or sqltype
        /// </summary>
        /// <param name="sqltype">If this parameter is null, get global variate SQLTYPE</param>
        /// <returns></returns>
        public static IDbConnection CreateSqlConnection(string sqltype = null)
        {
            if (sqltype is null) sqltype = SQLTYPE;
            switch (sqltype)
            {
                case "MSSQL":
                    return CreateConnection<SqlConnection>();
                default:
                    throw new Exception("Unsupported database type");
            }

        }
    }
}
