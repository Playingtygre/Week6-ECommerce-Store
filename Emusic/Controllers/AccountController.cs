﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Emusic.Data;
using Emusic.Models;
using Emusic.Models.ViewModels;

namespace Emusic.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public List<Claim> GetDefaultClaimsListForUser(ApplicationUser user) => new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.LastName}, {user.FirstName}", ClaimValueTypes.String),
            new Claim(ClaimTypes.Email, user.NormalizedEmail, ClaimValueTypes.Email),
            new Claim("MusicType", ((int)user.MusicType).ToString(), ClaimValueTypes.Integer32),
            new Claim("MusicVenue", ((int)user.MusicVenue).ToString(), ClaimValueTypes.Integer32)
        };

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email,
                    vm.Password, vm.KeepSignedIn, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(vm.Email);

                    // If the user is an administrator, take them to the product administration
                    // dashboard; otherwise, take the user to the products landing page
                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Products");
                    }

                    return RedirectToAction("Index", "Shop");
                }
                if (result.RequiresTwoFactor)
                {
                    throw new NotImplementedException();
                }
                if (result.IsLockedOut)
                {
                    throw new NotImplementedException();
                }
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
          
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterCommit(
            [Bind("Email", "Password", "ConfirmPassword", "FirstName", "LastName", "MusicType", "MusicVenue")] RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = vm.Email,
                    NormalizedEmail = vm.Email.ToLower(),
                    UserName = vm.Email,
                    NormalizedUserName = vm.Email.ToLower(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),

                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    MusicType = vm.MusicType,
                    MusicVenue = vm.MusicVenue
                };

                IdentityResult result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    // Add default claims and member role to the new user
                    await _userManager.AddClaimsAsync(user, GetDefaultClaimsListForUser(user));
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);

                    // Sign the user in and redirect them back to whence they came
                    await _signInManager.SignInAsync(user, isPersistent: true);

                    // If the user is an administrator, take them to the product administration
                    // dashboard; otherwise, take the user to the products landing page
                    if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Products");
                    }

                    return RedirectToAction("Index", "Shop");
                }

                // Something went wrong. Accumulate all errors into the model state.
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(vm);
        }

        public async Task<IActionResult> Profile()
        {
            return View(await _userManager.GetUserAsync(HttpContext.User));
        }
    }
}

