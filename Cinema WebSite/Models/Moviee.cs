using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Models
{
    public class Moviee
    {
        //all the informations about a new movie (without date and hour!)

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 30 characters")]
        public string Title { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Realisator must be between 2 and 30 characters")]
        public string Realisator { get; set; }


       [Required]
        public string Category { get; set; }

        [Required]
        public string LimitAge { get; set; }

        [Required]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime RunningTime { get; set; }

        [Required]
        public int Price { get; set; }



        [Required]
        [DisplayName("Upload File")]
        public String Poster { get; set; }

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

}