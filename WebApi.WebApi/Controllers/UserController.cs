using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using WebApi.Model;
using WebApi.Service;

namespace WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> logger;
        public UserController(ILogger<UserController> _logger)
        {
            logger = _logger;
        }
        /// <summary>
        /// Get User list
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserList")]
        [ApiExplorerSettings(GroupName = "ManagementV1")]//It needs to be consistent with the document name
        public List<User> GetUserList()
        {
            logger.LogDebug("{0}", "test");
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
            logger.LogDebug("id:{0}", id);
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
            logger.LogDebug("model:{0}", JsonConvert.SerializeObject(model));
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
            logger.LogDebug("model:{0}", JsonConvert.SerializeObject(model));
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
            logger.LogDebug("model:{0}", JsonConvert.SerializeObject(model));
            return new UserService().UserDelete(model);
        }
    }
}
