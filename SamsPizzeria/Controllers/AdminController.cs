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

        private IUserRolesService userRolesService;

        public AdminController(IUserRolesService userRolesServ)
        {
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

            return RedirectToAction(nameof(Index));

        }

        public async Task<ViewResult> Index()
        {
            var usersVM = await userRolesService.GetUsers();

            return View(usersVM);
        }
    }
}