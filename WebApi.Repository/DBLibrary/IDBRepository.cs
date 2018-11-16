using System.Collections.Generic;

namespace WebApi.Repository
{
    interface IDBRepository<T> where T : class
    {
        IEnumerable<T> GetList();

        T Get(object id);

        bool Update(T t);

        long Insert(T apply);

        bool Delete(T t);
    }
}
