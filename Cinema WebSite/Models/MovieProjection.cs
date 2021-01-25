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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ProjectionDate { get; set; }





        [Required]
        [DataType(DataType.Time)]
        //[Range(typeof(TimeSpan), "03:00:00", "22:00:00")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan BeginProjectionHour { get; set; }


        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan EndProjectionHour { get; set; }



        [Required]
        public String ProjectionHall { get; set; }

        public static implicit operator TimeSpan(MovieProjection v)
        {
            throw new NotImplementedException();
        }
    }
}