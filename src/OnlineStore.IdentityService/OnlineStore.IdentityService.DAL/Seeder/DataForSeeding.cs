using Microsoft.AspNetCore.Identity;
using OnlineStore.IdentityService.DAL.Data;

namespace OnlineStore.IdentityService.DAL.Seeder
{
    public static class DataForSeeding
    {
        public static List<ApplicationUserExtended> GetUsers()
        {
            return new List<ApplicationUserExtended>
            {
                new ApplicationUserExtended
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+375295071292",
                    PhoneNumberConfirmed = true,
                    Password = "Admin123!",
                    Role = UserRoles.Admin
                },
                new ApplicationUserExtended
                {
                    UserName = "user",
                    Email = "user@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+375292643852",
                    PhoneNumberConfirmed = true,
                    Password = "User987!",
                    Role = UserRoles.User
                }
            };
        }

        public static List<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole(UserRoles.User),
                new IdentityRole(UserRoles.Admin)
            };
        }
    }
}
