using Microsoft.EntityFrameworkCore;
using static ForumAppExercise.Data.DataConstants;

namespace ForumAppExercise.Data
{
    public class ForumAppDbContext : DbContext
    {
        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Post> Posts { get; set; }
    }
}
