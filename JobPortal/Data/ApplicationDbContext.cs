﻿using JobPortal.Models;
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
        public DbSet<Message> Messages { get; set; }

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
                    Id = "employer-role-id",
                    Name = "Employer",
                    NormalizedName = "EMPLOYER"
                }
            );

            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "juzba1-id-2478652fdsss154",
                    UserName = "Juzba88@gmail.com",
                    NormalizedUserName = "JUZBA88@GMAIL.COM",
                    Email = "Juzba88@gmail.com",
                    NormalizedEmail = "JUZBA88@GMAIL.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "security-stamp-1-14fds-fsd14dsf-dsfsdf5",
                    PasswordHash = "AQAAAAIAAYagAAAAEEH+X9L8IqMjAnas5R0lqrQnPScyf9lFnoVLZWO8Z6oXKDK72CXgyAiKCjd3drW26Q==",
                    ConcurrencyStamp = "concurency-stamp-1-112dsd-fsdsffsf-1444",
                },
                 new AppUser
                 {
                     Id = "katka2-id-12112122fdsss178",
                     UserName = "Katka@gmail.com",
                     NormalizedUserName = "KATKA@GMAIL.COM",
                     Email = "Katka@gmail.com",
                     NormalizedEmail = "KATKA@GMAIL.COM",
                     EmailConfirmed = true,
                     SecurityStamp = "security-stamp-2-14fds-fsd14dsf-242424242",
                     PasswordHash = "AQAAAAIAAYagAAAAEMpUq/G6FCHFYqBZnihzdsiHRhqrKsi6XzZQrOuBPZTKKCRYtiSzTxKxMPQe/GFbAg==",
                     ConcurrencyStamp = "concurency-stamp-2-112dsd-fssfdnmjsf-5866",
                 },
                 new AppUser
                 {
                     Id = "karel3-id-4242422fdsss145",
                     UserName = "Karel@gmail.com",
                     NormalizedUserName = "KAREL@GMAIL.COM",
                     Email = "Karel@gmail.com",
                     NormalizedEmail = "KAREL@GMAIL.COM",
                     EmailConfirmed = true,
                     SecurityStamp = "security-stamp-3-14fds-kjkhkdsf-dsfsd545",
                     PasswordHash = "AQAAAAIAAYagAAAAEKEAkQQa9R8U6qeYMzJ+wta1OH8ucPabTfW1aJSWYnQo/b/nWsQlVLQDVESflgvbJw==",
                     ConcurrencyStamp = "concurency-stamp-3-11kjkj-fsdsffsf-17855",
                 });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = "juzba1-id-2478652fdsss154",
                RoleId = "admin-role-id"
            },
            new IdentityUserRole<string>
            {
                UserId = "katka2-id-12112122fdsss178",
                RoleId = "employer-role-id"
            });

            builder.Entity<Job>()
                .Property(j => j.Salary)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Message>()
                .HasOne(m => m.Job)
                .WithMany(j => j.Messages)
                .HasForeignKey(m => m.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
