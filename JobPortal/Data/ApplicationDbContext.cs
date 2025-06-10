using JobPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Applications> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //IdentityUser user1 = new()
            //{
            //    Id = "user1-Id",
            //    UserName = "Juzba",
            //    NormalizedUserName= "JUZBA",
            //    Email = "Juzba88@gmail.com",
            //    NormalizedEmail = "JUZBA88@GMAIL.COM",
            //    PasswordHash = "123456",
            //    EmailConfirmed = true
            //};

            //IdentityUser user2 = new()
            //{
            //    Id = "user2-Id",
            //    UserName = "Katka",
            //    NormalizedUserName = "KATKA",
            //    Email = "Katka@gmail.com",
            //    NormalizedEmail = "KATKA@GMAIL.COM",
            //    PasswordHash = "123456",
            //    EmailConfirmed = true
            //};

            //IdentityUser user3 = new()
            //{
            //    Id = "user3-Id",
            //    UserName = "Karel",
            //    NormalizedUserName = "KAREL",
            //    Email = "Karel@gmail.com",
            //    NormalizedEmail = "KAREL@GMAIL.COM",
            //    PasswordHash = "123456",
            //    EmailConfirmed = true
            //};

            //builder.Entity<IdentityUser>().HasData(user1, user2, user3);

            builder.Entity<Job>()
                .Property(j => j.Salary)
                .HasColumnType("decimal(18, 2)");
        }

    }
}
