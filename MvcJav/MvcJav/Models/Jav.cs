using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MvcJav.Models
{
    public class Jav
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9""'\s-]*$"), Required, StringLength(100)]
        public string? Description { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"),Required,StringLength(30)]
        public string? Actor { get; set; }

        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name= "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"),Required, StringLength(5)]
        public string? Rating { get; set; }

    }
}
