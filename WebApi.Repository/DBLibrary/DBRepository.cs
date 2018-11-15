using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace WebApi.Data
{
    public class DBRepository<T> : IDBRepository<T> where T : class
    {
        /// <summary>
        /// Get model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(object id)
        {
            using (var con = ConnectionFactory.CreateSqlConnection())
            {
                return con.Get<T>(id);
            }
        }
        /// <summary>
        /// Get model of list
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetList()
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.GetAll<T>();
            }
        }
        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T Insert(T model)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                conn.Insert(model);
                return model;
            }
        }
        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T Update(T model)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                conn.Update(model);
                return model;
            }
        }
        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Delete(T t)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Delete(t);
            }
        }

    }
}
