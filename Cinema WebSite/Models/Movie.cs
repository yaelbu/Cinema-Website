using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class Movie
    {
        [Key]
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 30 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Realisator must be between 2 and 30 characters")]
        public string Realisator { get; set; }

        [Required]
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

       
        [Required]
        public string Category { get; set; }

       [Required]
        public int Price { get; set; }

    }
    /*
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
    }*/
}