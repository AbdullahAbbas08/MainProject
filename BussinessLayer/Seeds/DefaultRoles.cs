using BussinessLayer.Constants;
using Microsoft.AspNetCore.Identity;

namespace BussinessLayer.Seeds 
{
    public static class DefaultRoles
    {
        public static async Task SeedRole(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin));
                await roleManager.CreateAsync(new IdentityRole(Roles.Manager));
                await roleManager.CreateAsync(new IdentityRole(Roles.Employee));
            }
        }
    }
}
