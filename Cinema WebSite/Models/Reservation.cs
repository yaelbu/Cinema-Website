using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderNumber { get; set; }

        public String Email { get; set; }

        public String Title { get; set; }
        public int Projection_Id { get; set; }

        public int SeatNumber { get; set; }

        public String OrderStatus { get; set; }

        public int TotalPrice { get; set; }

        public int Price { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ProjectionDate { get; set; }


        [DataType(DataType.Time)]
        public TimeSpan BeginProjectionHour { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan EndProjectionHour { get; set; }

    }
}