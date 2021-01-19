using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class MovieProjection
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        //[StringLength(30, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 30 characters")]
        public String Title { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ProjectionDate { get; set; }


        [Required]
        public DateTime BeginProjectionHour { get; set; }



        [Required]
        public String ProjectionHall { get; set; }


    }
}