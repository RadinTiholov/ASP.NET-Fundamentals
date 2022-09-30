using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimePlace.Core.Models
{
    public class Anime
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public int Description { get; set; }

        [Required]
        public string Trailer { get; set; }
    }
}
