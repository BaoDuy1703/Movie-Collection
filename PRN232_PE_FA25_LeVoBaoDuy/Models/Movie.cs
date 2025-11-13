using System.ComponentModel.DataAnnotations;

namespace PRN232_PE_FA25_LeVoBaoDuy.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Genre { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        [Url]
        public string? PosterImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

