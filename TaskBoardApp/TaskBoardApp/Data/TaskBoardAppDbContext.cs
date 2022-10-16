using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Entities;
using Task = TaskBoardApp.Data.Entities.Task;

namespace TaskBoardApp.Data
{
    public class TaskBoardAppDbContext : IdentityDbContext<User>
    {
        public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Task> Task { get; set; }
        private User GuestUser { get; set; }
        private Board OpenBoard { get; set; }
        private Board InProgressBoard { get; set; }
        private Board DoneBoard { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.
                Entity<Task>()
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();
            builder
                .Entity<User>()
                .HasData(this.GuestUser);

            SeedBoards();
            builder
                .Entity<Board>()
                .HasData(this.OpenBoard, this.InProgressBoard, this.DoneBoard);

            builder
                .Entity<TaskBoardApp.Data.Entities.Task>()
                .HasData(
                new TaskBoardApp.Data.Entities.Task()
                {
                    Id = 1,
                    Title = "Study for CS",
                    Description = "Study as hard as you can bitch!",
                    CreatedOn = DateTime.Now,
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.OpenBoard.Id
                },
                new TaskBoardApp.Data.Entities.Task()
                {
                    Id = 2,
                    Title = "Study for Math",
                    Description = "Study as hard as you can bitch!",
                    CreatedOn = DateTime.Now,
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.DoneBoard.Id
                },
                new TaskBoardApp.Data.Entities.Task()
                {
                    Id = 3,
                    Title = "Workout",
                    Description = "Workout as hard as you can bitch!",
                    CreatedOn = DateTime.Now,
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.OpenBoard.Id
                },
                new TaskBoardApp.Data.Entities.Task()
                {
                    Id = 4,
                    Title = "Read",
                    Description = "Read as fast as you can bitch!",
                    CreatedOn = DateTime.Now,
                    OwnerId = this.GuestUser.Id,
                    BoardId = this.InProgressBoard.Id
                });
            ;

            base.OnModelCreating(builder);
        }

        private void SeedBoards()
        {
            this.OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"
            };

            this.InProgressBoard = new Board()
            {
                Id = 2,
                Name = "InProgress"
            };

            this.DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            this.GuestUser = new User()
            {
                UserName = "Radin",
                NormalizedUserName = "RADIN",
                Email = "radin@mail.bg",
                NormalizedEmail = "RADIN@MAIL.BG",
                FirstName = "Radin",
                LastName = "Tiholov"
            };

            this.GuestUser.PasswordHash = hasher.HashPassword(this.GuestUser, "asdasd");
        }
    }
}