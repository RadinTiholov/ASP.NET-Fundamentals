using ForumAppExercise.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumAppExercise.Data
{
    public class ForumAppDbContext : DbContext
    {
        private Post FirstPost { get; set; }
        private Post SecondPost { get; set; }
        private Post ThirdPost { get; set; }
        public DbSet<Post> Posts { get; set; }
        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedPosts();
            modelBuilder
                .Entity<Post>()
                .HasData(this.FirstPost, this.SecondPost, this.ThirdPost);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedPosts()
        {
            this.FirstPost = new Post()
            {
                Id = 1,
                Title = "My first post",
                Content = "Some content here....Hehehehehehhehehehehhe"
            };

            this.SecondPost = new Post()
            {
                Id = 2,
                Title = "My second post",
                Content = "Some content here....Hehehehehehhehehehehhe2222kue22r"
            };

            this.ThirdPost = new Post()
            {
                Id = 3,
                Title = "My third post",
                Content = "Some content here....Heheheheheaaaahhehehehehhe"
            };
        }

    }
}
