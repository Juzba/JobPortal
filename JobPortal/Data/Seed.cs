using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Data
{
    public class Seed(UserManager<IdentityUser> userManager, ApplicationDbContext db, ILogger<Seed> logger)
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly ApplicationDbContext _db = db;
        private readonly ILogger<Seed> _logger = logger;


        public async Task InitializeUsersAsync()
        {
            try
            {
                if (await _db.Users.AnyAsync()) return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Chyba při kontrole uživatelů v databázi.");
                return;
            }

            var user1 = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Juzba88@gmail.com",
                NormalizedUserName = "JUZBA88@GMAIL.COM",
                Email = "Juzba88@gmail.com",
                NormalizedEmail = "JUZBA88@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),   
            };

            user1.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user1, "123456");

            await _userManager.CreateAsync(user1);


        }
    }
}
