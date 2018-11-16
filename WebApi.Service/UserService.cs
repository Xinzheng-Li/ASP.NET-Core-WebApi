using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Repository;
using WebApi.Model;

namespace WebApi.Service
{
    public class UserService
    {
        public List<User> GetUserList()
        {
            return new UserRepository().GetUserList();
        }
        public User GetUser(int id)
        {
            return new UserRepository().GetUser(id);
        }
        public bool UserInsert(User model)
        {
            return new UserRepository().UserInsert(model) > 0;
        }
        public bool UserUpdate(User model)
        {
            return new UserRepository().UserUpdate(model);
        }
        public bool UserDelete(User model)
        {
            return new UserRepository().UserDelete(model);
        }
    }
}
