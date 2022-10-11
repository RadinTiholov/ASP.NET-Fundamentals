using System.ComponentModel.DataAnnotations;

namespace AnimePlace.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null;

        [UIHint("hidden")]
        public string? RreturnUrl { get; set; }
    }
}
