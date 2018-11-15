using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Data;
using WebApi.Model;

namespace WebApi.Service
{
    public class UserService
    {
        public List<User> GetUserList()
        {
            return new UserRepository().GetUserList();
        }
    }
}
