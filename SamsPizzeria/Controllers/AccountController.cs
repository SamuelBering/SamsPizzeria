using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SamsPizzeria.Models;
using SamsPizzeria.Models.ViewModels;

namespace Users.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> usrMgr, SignInManager<AppUser> signinMgr)
        {
            userManager = usrMgr;
            signInManager = signinMgr;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(
                    user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email),
                "Invalid user or password");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StreetAddress = model.StreetAddress,
                    ZipCode = model.ZipCode,
                    PostTown = model.PostTown,
                    Email = model.Email,
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Product");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditModel editModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(User);

                var isValidPassword = await userManager.CheckPasswordAsync(user, editModel.Password);
                if (!isValidPassword)
                {
                    ModelState.AddModelError(nameof(EditModel.Password), "Felaktigt lösenord");
                    return View(editModel);
                }

                if (editModel.NewPassword != null)
                {
                    if (editModel.Password == null)
                    {
                        ModelState.AddModelError(nameof(EditModel.Password), "Ange ditt nuvarande lösenord");
                        return View(editModel);
                    }

                    var result = await userManager.ChangePasswordAsync(user, editModel.Password, editModel.NewPassword);

                    if (!result.Succeeded)
                    {
                        AddModelError("", result);
                        return View(editModel);
                    }
                }
                
                user.FirstName = editModel.FirstName;
                user.LastName = editModel.LastName;
                user.Email = editModel.Email;
                user.StreetAddress = editModel.StreetAddress;
                user.PostTown = editModel.PostTown;
                user.ZipCode = editModel.ZipCode;

                var updateResult = await userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                {
                    AddModelError("", updateResult);
                    return View(editModel);
                }

                return RedirectToAction(nameof(ConfirmAccountUpdate));
            }

            return View(editModel);
        }

        public ViewResult ConfirmAccountUpdate()
        {
            return View();
        }

        private void AddModelError(string key, IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
                ModelState.AddModelError(key, error.Description);
        }

        public async Task<ViewResult> Edit()
        {
            AppUser user = await userManager.GetUserAsync(User);

            EditModel userVM = new EditModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PostTown = user.PostTown,
                StreetAddress = user.StreetAddress,
                ZipCode = user.ZipCode
            };

            return View(userVM);
        }
    }
}