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
    }
}
