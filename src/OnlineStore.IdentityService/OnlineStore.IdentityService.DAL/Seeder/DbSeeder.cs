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
                var roles = DataForSeeding.GetRoles();

                foreach (var role in roles)
                {
                    // Can't use 'await' there since it will be used in non async code (Program.cs)
                    _ = Task.Run(() => _roleManager.CreateAsync(role)).Result;
                }
            }

            if (!_db.Users.Any())
            {
                var users = DataForSeeding.GetUsers();

                foreach (var user in users)
                {
                    _ = Task.Run(() => _userManager.CreateAsync(user, user.Password)).Result;
                    _ = Task.Run(() => _userManager.AddToRoleAsync(user, user.Role)).Result;
                }
            }
        }
    }
}
