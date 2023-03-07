using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsAPI.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }


        [Required]
        public string Remarks { get; set; }


        [ForeignKey("User")]
        public string? UserId { get; set; }


        [ForeignKey("Film")]
        public int FilmId { get; set; }
    }
}
