
using BussinessLayer.Constants;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace BussinessLayer.Seeds 
{
    public static class DefaultUsers  
    {
        public static async Task SeedAdminUser(UserManager<AppUser> UserManager, RoleManager<IdentityRole> roleManager)
        {
            #region  Create SuperAdmin
            var SuperAdmin = new Employee
            {
                UserName = "SuperAdmin250",
                Email = "SuperAdmin250@gmail.com",
                FirstName ="Abdullah",
                LastName ="Saeed",
                Salay=1000,
                ImagePath ="123",              
            };

            var user = await UserManager.FindByNameAsync(SuperAdmin.UserName);
            if (user == null)
            {
                await UserManager.CreateAsync(SuperAdmin, "Consoleapp#123");
                await UserManager.AddToRolesAsync(SuperAdmin, new List<string>
                {
                    Roles.SuperAdmin
                });
            }
            #endregion

            #region Create Manager
            var Manager = new Employee
            {
                UserName = "Manager250",
                Email = "Manager250@gmail.com",
                FirstName ="Ahmed",
                LastName ="Abdullah",
                Salay=2000,
                ImagePath ="456",
                //ManagerId = "c64cc7ed-dee0-490f-8f3c-01bd0928b458"
            };

            var _Manager = await UserManager.FindByNameAsync(Manager.UserName);
            if (_Manager == null) 
            {
                await UserManager.CreateAsync(Manager, "Consoleapp#123");
                await UserManager.AddToRolesAsync(Manager, new List<string>
                {
                    Roles.Manager
                });
            }
            #endregion

            #region Create Employee
            var Employee = new Employee
            {
                UserName = "Employee250",
                Email = "Employee250@gmail.com",
                FirstName ="Khaled",
                LastName ="Abdullah",
                Salay=3000,
                ImagePath ="789",
                //ManagerId = "0b94684d-618e-42f5-9416-93b8a83dd7ee"
            };

            var _Employee = await UserManager.FindByNameAsync(Employee.UserName);
            if (_Employee == null) 
            {
                await UserManager.CreateAsync(Employee, "Consoleapp#123");
                await UserManager.AddToRolesAsync(Employee, new List<string>
                {
                    Roles.Employee
                });
            }
            #endregion

            //await roleManager.SeedClaimsForAdmin();
        }

        //private static async Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        //{
        //    //var AdminRole = await roleManager.FindByNameAsync(Roles.Manager);
        //    //await roleManager.AddClaimPermissions(AdminRole, "call");
        //}

        //private static async Task AddClaimPermissions(this RoleManager<IdentityRole> roleManager,IdentityRole role , string Module)
        //{
        //    var AllClaims = await roleManager.GetClaimsAsync(role);
        //    var AllPermissions = Permissions.GeneratePermissionsList(Module);
        //    foreach (var item in AllPermissions)
        //    {
        //        if(!AllClaims.Any(x=>x.Type =="Permission" && x.Value == item))
        //        {
        //            await roleManager.AddClaimAsync(role, new Claim("Permission", item));
        //            //List<string> x = Enum.GetValues(typeof(ClientPermissions)).Cast<ClientPermissions>().Cast<string>().ToList();
        //        }
        //    }
        //}
    }
}
