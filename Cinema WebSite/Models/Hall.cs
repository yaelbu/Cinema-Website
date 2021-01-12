using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class Hall
    {
        [Key]
        [Required]
        public string Hall_Name { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}