using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace WebApi.Model
{
    [Table("[User]")]
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
