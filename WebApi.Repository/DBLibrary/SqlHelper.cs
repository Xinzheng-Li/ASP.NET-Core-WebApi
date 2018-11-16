using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace WebApi.Data
{
    public static class SqlHelper
    {
        public static bool ExecuteNonQuery(string sql, object param, CommandType commandType = CommandType.Text, int? commandTimeOut = 5)
        {
            var intResult = 0;
            using (var db = ConnectionFactory.CreateSqlConnection())
            {
                intResult = db.Execute(sql, param, null, commandTimeOut, commandType);
            }
            return intResult > 0;
        }

        public static object ExecuteScalar(string sql, object param, CommandType commandType = CommandType.Text, int? commandTimeOut = 5)
        {
            using (var db = ConnectionFactory.CreateSqlConnection())
            {
                return db.ExecuteScalar(sql, param, null, commandTimeOut, commandType);
            }
        }

        /// <summary>
        /// Execute multiple commands（no Paramater）
        /// </summary>
        /// <param name="SQLStringList">/param>		
        public static void ExecuteSqlTran(List<string> SQLStringList)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                SQLStringList.ForEach(x => conn.Execute(x));
            }
        }
    }
}
