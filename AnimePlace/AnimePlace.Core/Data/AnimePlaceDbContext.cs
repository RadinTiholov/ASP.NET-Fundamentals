using AnimePlace.Core.Models;
using AnimePlace.Core.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AnimePlace.Data
{
    public class AnimePlaceDbContext : IdentityDbContext<ApplicationUser>
    {
        public AnimePlaceDbContext(DbContextOptions<AnimePlaceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Anime> Animes { get; set; }
    }
}