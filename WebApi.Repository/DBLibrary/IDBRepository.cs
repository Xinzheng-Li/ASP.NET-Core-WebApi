using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace WebApi.Data
{
    interface IDBRepository<T> where T : class
    {
        IEnumerable<T> GetList();

        T Get(object id);

        T Update(T t);

        T Insert(T apply);

        bool Delete(T t);
    }
}
