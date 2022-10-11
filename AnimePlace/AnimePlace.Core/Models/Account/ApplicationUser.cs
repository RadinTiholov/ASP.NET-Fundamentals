using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AnimePlace.Core.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }
    }
}
