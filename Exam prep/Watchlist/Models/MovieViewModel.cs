using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Models;

namespace Watchlist.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

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
    }
}
