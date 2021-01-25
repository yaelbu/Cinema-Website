using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class User
    {
       // [Required]
        [Required(ErrorMessage = "Your firstname is required!")]
        [StringLength(15,MinimumLength =2, ErrorMessage="FirstName must be between 2 and 15 characters")]
        public string FirstName { get; set; }

       // [Required]
        [Required(ErrorMessage = "Your lastname is required!")]
        [StringLength(15, MinimumLength = 2,ErrorMessage ="LastName must be between 2 and 15 characters")]
        public string LastName { get; set; }

        [Key]
        //[Required]
        [Required(ErrorMessage = "Your email (username) is required!")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }


        //[Required]
        [Required(ErrorMessage = "Your password is required!")]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 12 characters")]
        public string Password { get; set; }


        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Status { get; set; }

    }
}