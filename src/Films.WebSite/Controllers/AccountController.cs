using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Films.Website.Domain;
using Films.WebSite.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Films.WebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignUpDetails signUpDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(signUpDetails);
            }

            var user = new User
            {
                UserName = signUpDetails.UserName,
                Email = signUpDetails.Email,
            };

            var result = await userManager.CreateAsync(user, signUpDetails.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }

                return View();
            }

            await signInManager.SignInAsync(user, true);

            return RedirectToActionPermanent(nameof(FilmsController.Index), "Films");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInCredentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }

            var user = await userManager.FindByNameAsync(credentials.UserName);

            if(user is null)
            {
                ModelState.AddModelError(String.Empty, "Invalid credentials.");
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(user, credentials.Password, credentials.RememberMe, false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Invalid credentials.");
                return View();
            }

            return RedirectToActionPermanent(nameof(FilmsController.Index), "Films");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToActionPermanent(nameof(FilmsController.Index), "Films");
        }
    }
}
