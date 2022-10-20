﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Redirect(nameof(Login));
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded) 
                {
                    return RedirectToAction("All", "Movies");
                }
            }
            ModelState.AddModelError("", "Invalid login!");

            return View(model);
        }

        public async Task<IActionResult> Logout(string id) 
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}