using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Models
{
    public class TestsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<Icon> Icons { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // theme - test many to many
            modelBuilder.Entity<TestTheme>()
                .HasKey(x => new { x.TestId, x.ThemeId });

            modelBuilder.Entity<TestTheme>()
                .HasOne(x => x.Test)
                .WithMany(m => m.Themes)
                .HasForeignKey(x => x.TestId);

            modelBuilder.Entity<TestTheme>()
                .HasOne(x => x.Theme)
                .WithMany(m => m.Tests)
                .HasForeignKey(x => x.ThemeId);

            // test - user many to many
            modelBuilder.Entity<UserTest>()
                .HasKey(x => new { x.UserId, x.TestId });

            modelBuilder.Entity<UserTest>()
                .HasOne(x => x.Test)
                .WithMany(m => m.Users)
                .HasForeignKey(x => x.TestId);

            modelBuilder.Entity<UserTest>()
                .HasOne(x => x.User)
                .WithMany(m => m.Tests)
                .HasForeignKey(x => x.UserId);

            // test - question many to many
            modelBuilder.Entity<TestQuestion>()
                .HasKey(x => new { x.TestId, x.QuestionId });

            modelBuilder.Entity<TestQuestion>()
                .HasOne(x => x.Question)
                .WithMany(m => m.Tests)
                .HasForeignKey(x => x.QuestionId);

            modelBuilder.Entity<TestQuestion>()
                .HasOne(x => x.Test)
                .WithMany(m => m.Questions)
                .HasForeignKey(x => x.TestId);

            // question - answer many to many
            modelBuilder.Entity<QuestionAnswer>()
                .HasKey(x => new { x.QuestionId, x.AnswerId });

            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(x => x.Answer)
                .WithMany(m => m.Questions)
                .HasForeignKey(x => x.AnswerId);

            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(x => x.Question)
                .WithMany(m => m.Answers)
                .HasForeignKey(x => x.QuestionId);

            // user - achievement many to many
            modelBuilder.Entity<UserAchievement>()
                .HasKey(x => new { x.UserId, x.AchievementId });

            modelBuilder.Entity<UserAchievement>()
                .HasOne(x => x.User)
                .WithMany(m => m.Achievements)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserAchievement>()
                .HasOne(x => x.Achievement)
                .WithMany(m => m.Users)
                .HasForeignKey(x => x.AchievementId);

            modelBuilder.Entity<Role>()
                .HasMany(x => x.Users)
                .WithOne(u => u.Role);
        }

        public TestsDbContext(DbContextOptions<TestsDbContext> options) : base(options)
        {
            
        }
    }
}
