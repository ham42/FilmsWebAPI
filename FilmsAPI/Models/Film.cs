using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace FilmsAPI.Models
{
    public class Film
    {
        [Key]
        public int FilmID { get; set; }


        [Required(ErrorMessage = "Name is Required")]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Description is Required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Release Date is Required")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


        [Required(ErrorMessage = "Rating is Required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 to 5")]
        public int Rating { get; set; }


        [Required(ErrorMessage = "Ticket Price is Required")]
        public double TicketPrice { get; set; }


        [Required(ErrorMessage = "Country is Required")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Genre is Required")]
        public GenreType Genre { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string? ImageURL { get; set; }


        [NotMapped]
        public IFormFile ImageFile { get; set; }


        [NotMapped]
        public string? ImageSrc { get; set; }


        public enum GenreType
        {
            Action = 0,
            Comedy = 1,
            Drama = 2,
            SciFi = 3
        }
    }

    
}