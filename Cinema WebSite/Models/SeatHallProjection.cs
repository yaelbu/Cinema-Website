using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class SeatHallProjection
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Hall_Name { get; set; }

        public int Projection_Id { get; set; }


        public int SeatNumber { get; set; }

        public String StatusSeat { get; set; }

        [NotMapped]
        public bool Select { get; set; }


    }
}