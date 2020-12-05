using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class User
    {
        [Required]
        [StringLength(15,MinimumLength =2, ErrorMessage="FirstName must be between 2 and 15 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2,ErrorMessage ="LastName must be between 2 and 15 characters")]
        public string LastName { get; set; }

        [Key]
        [Required]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }

       // [Key]
      //  [Required]
      //  [StringLength(12,MinimumLength = 5, ErrorMessage = "Username must be between 5 and 12 characters")]
     //   public string Username { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 12 characters")]
        public string Password { get; set; }

        public string Status { get; set; }

    }
}