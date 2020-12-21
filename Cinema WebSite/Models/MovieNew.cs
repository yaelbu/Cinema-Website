using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class MovieNew
    {
        //[ForeignKey]
        //public int index { get; set; }
        [Key]
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 30 characters")]
        public string Title { get; set; }


        [Required]
        [DisplayName("Upload File")]
        public String ImagePath { get; set; }


        [Required]
        public string Category { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Limitation_Age { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [Required]
        public int BeginHourMovie { get; set; }

        [Required]
        public int EndHourMovie { get; set; }




        [Required]
        public String Hall { get; set; }



    }


    public enum Category
    {
        Action,
        Adventure,
        Comedy,
        Crime,
        Drama,
        Fantasy,
        Horror,
        Romance,
        ScienceFiction,
        Thriller,
        Western
    }

    public enum Halls
    {
        A1,
        A2,
        A3,
        A4,
        A5,
        A6,
        A7,
        A8,
        A9,
        A10,
        A11,
        A12,
        A13,
        A14,
        A15,
        A16,
        A17,
        A18,
        A19,
        A20
    }

}