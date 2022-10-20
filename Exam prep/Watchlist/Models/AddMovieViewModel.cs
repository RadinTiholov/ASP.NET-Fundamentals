using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class AddMovieViewModel
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Director { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(typeof(decimal), "0.0", "10", ConvertValueInInvariantCulture = true)]
        [Required]
        public decimal Rating { get; set; }

        [Required]
        public string Genre { get; set; }

        public int GenreId { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
    }
}
