using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.DependencyInjection;
using SamsPizzeria.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace SamsPizzeria.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }

        public static async Task CreateDefaultRoles(IServiceProvider serviceProvider,
                                                    IConfiguration configuration)
        {

            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = configuration.GetSection("Data:DefaultRoles").Get<string[]>();

            foreach (string role in roles)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateDefaultUsers(IServiceProvider serviceProvider, IConfiguration configuration)
        {

            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var usersVM = configuration.GetSection("Data:DefaultUsers").Get<CreateDefaultUserModel[]>();

            foreach (var userVM in usersVM)
            {
                if (await userManager.FindByNameAsync(userVM.Email) == null)
                {
                   
                    AppUser user = new AppUser
                    {
                        UserName = userVM.Email,
                        Email = userVM.Email,
                    };

                    IdentityResult result = await userManager.CreateAsync(user, userVM.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, userVM.Role);
                    }
                }
            }


        }

    }

}