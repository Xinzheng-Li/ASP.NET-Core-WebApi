using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace WebApi.Repository
{
    public static class ConnectionFactory
    {
        public static string CONN_STRING_MSSQL = "";//Default Sql_String
        public static string CONN_STRING_MYSQL = "";
        /// <summary>
        /// global variate, default database
        /// </summary>
        public static string SQLTYPE = "MSSQL";
        public static IDbConnection CreateConnection<T>(string conStr = null) where T : IDbConnection, new()
        {
            if (conStr is null) conStr = CONN_STRING_MSSQL;
            IDbConnection connection = new T();
            connection.ConnectionString = CONN_STRING_MSSQL;
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
                    return CreateConnection<SqlConnection>(CONN_STRING_MSSQL);
                case "MYSQL":
                    return CreateConnection<MySqlConnection>(CONN_STRING_MYSQL);
                default:
                    throw new Exception("Unsupported database type");
            }

        }
    }
}
