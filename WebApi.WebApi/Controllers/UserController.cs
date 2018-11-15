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
        [HttpGet]
        [ApiExplorerSettings(GroupName = "ManagementV1")]//It needs to be consistent with the document name
        public List<User> GetUserList()
        {
            return new UserService().GetUserList();
        }
    }
}
