using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class Payment
    {
        [Required]
        [RegularExpression("^4[0-9]{12}(?:[0-9]{3})?",ErrorMessage = "Credit number must be contain 16 digits")]
        //[StringLength(16, MinimumLength = 16, ErrorMessage = "Credit number must be contain 16 digits")]
        public long CreditNumber { get; set; }


  

        [Required]
        [RegularExpression("[0-12]")]
        public int MonthExpiration { get; set; }



        [Required]
        //[RegularExpression("[2021-2049]")]
        public int YearExpiration { get; set; }


        [Required]
       // [RegularExpression("[100-9999]", ErrorMessage = "cvv number must contain 3 or 4 digits")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "cvv number must contain 3 or 4 digits")]
        public int cvvNumber { get; set; }



    }
}