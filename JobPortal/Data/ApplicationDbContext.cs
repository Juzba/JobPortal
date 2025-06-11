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
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "admin-role-id",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "jobseeker-role-id",
                    Name = "JobSeeker",
                    NormalizedName = "JOBSEEKER"
                },
                new IdentityRole
                {
                    Id = "user-role-id",
                    Name = "Employer",
                    NormalizedName = "EMPLOYER"
                }
            );

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "user1-id-2478652fdsss154",
                UserName = "Juzba88@gmail.com",
                NormalizedUserName = "JUZBA88@GMAIL.COM",
                Email = "Juzba88@gmail.com",
                NormalizedEmail = "JUZBA88@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "security-stamp-1-14fds-fsd14dsf-dsfsdf5",
                PasswordHash = "AQAAAAIAAYagAAAAEEH+X9L8IqMjAnas5R0lqrQnPScyf9lFnoVLZWO8Z6oXKDK72CXgyAiKCjd3drW26Q==",
                ConcurrencyStamp = "concurency-stamp-1-112dsd-fsdsffsf-1444",
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = "user1-id-2478652fdsss154",
                RoleId = "admin-role-id"
            });

            builder.Entity<Job>()
                .Property(j => j.Salary)
                .HasColumnType("decimal(18, 2)");

            base.OnModelCreating(builder);

        }

    }
}
