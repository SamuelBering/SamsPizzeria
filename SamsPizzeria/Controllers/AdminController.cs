using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SamsPizzeria.Models;
using System.Collections.Generic;
using System.Linq;
using SamsPizzeria.Models.ViewModels;
using System.Threading.Tasks;
using SamsPizzeria.Services;

namespace Users.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //private UserManager<AppUser> userManager;
        //private RoleManager<IdentityRole> roleManager;
        private IUserRolesService userRolesService;

        public AdminController(/*UserManager<AppUser> usrMgr, RoleManager<IdentityRole> roleMgr,*/ IUserRolesService userRolesServ)
        {
            //userManager = usrMgr;
            //roleManager = roleMgr;
            userRolesService = userRolesServ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRole(UserRolesModificationModel userVM)
        {
            if (ModelState.IsValid)
            {
                await userRolesService.UpdateUserRoles(userVM);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index), await userRolesService.GetUsers());
        }

        public async Task<ViewResult> Index()
        {

            //var roles =  roleManager.Roles.ToList();

            //List<UserRolesModificationModel> usersVM = new List<UserRolesModificationModel>();

            //foreach (var user in userManager.Users)
            //{

            //    UserRolesModificationModel userVM = new UserRolesModificationModel
            //    {
            //        UserId = user.Id,
            //        FirstName=user.FirstName,
            //        LastName=user.LastName,
            //        Email=user.Email,
            //        Roles = new List<SelectListItem>()
            //    };

            //    foreach (var role in roles)
            //    {
            //        if (await userManager.IsInRoleAsync(user, role.Name))
            //            userVM.Roles.Add(new SelectListItem
            //            {
            //                Value=role.Id,
            //                Text=role.Name,
            //                Selected=true
            //            });
            //        else
            //            userVM.Roles.Add(new SelectListItem
            //            {
            //                Value=role.Id,
            //                Text=role.Name,
            //            });
            //    }


            //usersVM.Add(userVM);
            //}

            var usersVM = await userRolesService.GetUsers();

            return View(usersVM);
        }
    }
}