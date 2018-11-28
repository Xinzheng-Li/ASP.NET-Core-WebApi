using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Service;

namespace WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Get User list
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserList")]
        [ApiExplorerSettings(GroupName = "ManagementV1")]//It needs to be consistent with the document name
        public List<User> GetUserList()
        {
            return new UserService().GetUserList();
        }
        /// <summary>
        /// Get User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUser")]
        [ApiExplorerSettings(GroupName = "ManagementV1")]
        public User GetUser(int id)
        {
            return new UserService().GetUser(id);
        }
        /// <summary>
        /// Create a new User and return the user information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UserInsert")]
        [ApiExplorerSettings(GroupName = "ManagementV1")]
        public bool UserInsert(User model)
        {
            return new UserService().UserInsert(model);
        }
        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UserUpdate")]
        [ApiExplorerSettings(GroupName = "ManagementV1")]
        public bool UserUpdate(User model)
        {
            return new UserService().UserUpdate(model);
        }
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("UserDelete")]
        [ApiExplorerSettings(GroupName = "ManagementV1")]
        public bool UserDelete(User model)
        {
            return new UserService().UserDelete(model);
        }
    }
}
