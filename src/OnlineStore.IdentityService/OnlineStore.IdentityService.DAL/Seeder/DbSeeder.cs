using Microsoft.AspNetCore.Identity;
using OnlineStore.IdentityService.DAL.Data;
using OnlineStore.IdentityService.DAL.Seeder.Contracts;

namespace OnlineStore.IdentityService.DAL.Seeder
{
    public class DbSeeder : IDbSeeder
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeeder(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            if (!_db.Roles.Any())
            {
                // Can't use 'await' there since it will be used in non async code (Program.cs)
                _ = Task.Run(() => _roleManager.CreateAsync(new IdentityRole(UserRoles.User))).Result;
                _ = Task.Run(() => _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin))).Result;
            }

            if (!_db.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+375295071292",
                    PhoneNumberConfirmed = true
                };

                var user = new ApplicationUser
                {
                    UserName = "user",
                    Email = "user@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+375292643852",
                    PhoneNumberConfirmed = true
                };

                _ = Task.Run(() => _userManager.CreateAsync(admin, "Admin123!")).Result;
                _ = Task.Run(() => _userManager.CreateAsync(user, "User987!")).Result;
                _ = Task.Run(() => _userManager.AddToRoleAsync(admin, UserRoles.Admin)).Result;
                _ = Task.Run(() => _userManager.AddToRoleAsync(user, UserRoles.User)).Result;
            }
        }
    }
}
