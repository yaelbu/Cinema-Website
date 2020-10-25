using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class UsersLogin
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

    }
}