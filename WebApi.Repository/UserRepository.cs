using WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Data
{
    public class UserRepository
    {
        public List<User> GetUserList()
        {
            return new DBRepository<User>().GetList().ToList();
        }
        public User GetUser(int id)
        {
            return new DBRepository<User>().Get(id);
        }
        public long UserInsert(User model)
        {
            return new DBRepository<User>().Insert(model);
        }
        public bool UserUpdate(User model)
        {
            return new DBRepository<User>().Update(model);
        }
        public bool UserDelete(User model)
        {
            return new DBRepository<User>().Delete(model);
        }
    }
}
