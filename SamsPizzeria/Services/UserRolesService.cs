﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SamsPizzeria.Models;
using SamsPizzeria.Models.ViewModels;


namespace SamsPizzeria.Services
{
    public class UserRolesService : IUserRolesService
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserRolesService(UserManager<AppUser> usrMgr, RoleManager<IdentityRole> roleMgr)
        {
            userManager = usrMgr;
            roleManager = roleMgr;
        }

        public async Task<ICollection<UserRolesModificationModel>> GetUsers()
        {
            var roles = roleManager.Roles.ToList();

            List<UserRolesModificationModel> usersVM = new List<UserRolesModificationModel>();

            foreach (var user in userManager.Users)
            {

                UserRolesModificationModel userVM = new UserRolesModificationModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = new List<SelectListItem>()
                };

                foreach (var role in roles)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                        userVM.Roles.Add(new SelectListItem
                        {
                            Value = role.Id,
                            Text = role.Name,
                            Selected = true
                        });
                    else
                        userVM.Roles.Add(new SelectListItem
                        {
                            Value = role.Id,
                            Text = role.Name,
                        });
                }

                usersVM.Add(userVM);
            }
            return usersVM;
        }

        public async Task UpdateUserRoles(UserRolesModificationModel userVM)
        {
            var appUser = await userManager.FindByIdAsync(userVM.UserId);

            var roles = await userManager.GetRolesAsync(appUser);

            if (roles != null && roles.Count > 0)
                await userManager.RemoveFromRolesAsync(appUser, roles);

            foreach (var roleId in userVM.SelectedRoleIds)
            {
                var roleName = (await roleManager.FindByIdAsync(roleId)).Name;
                await userManager.AddToRoleAsync(appUser, roleName);
            }
        }


    }
}
