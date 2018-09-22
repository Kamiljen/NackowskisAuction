﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NackowskisAuctionHouse.DAL.IdentityModels;
using NackowskisAuctionHouse.IdentityService;
using NackowskisAuctionHouse.ViewModels;

namespace NackowskisAuctionHouse.Controllers
{
    public class AccountController : Controller
    {
       private IIdentityService _userService;

        public AccountController(IIdentityService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> SignIn(SignInVM input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _userService.SignInAsync(input);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register(RegisterVM input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userService.RegisterUser(input);
                if (user != null)
                {
                    var done = await _userService.SignInAsync(user, true);
                    return LocalRedirect(returnUrl);
                }
            }
            return LocalRedirect(returnUrl);
        }


       
        [ValidateAntiForgeryToken]
        public IActionResult SignOut()
        {
             _userService.SignOut();

            return Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=http://gustavcleveman.azurewebsites.net/");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UserRoleVM model)
        {
            var result = await _userService.UpdateUserRole(model.UserId, model.OldRole, model.NewRole);
            return RedirectToAction("Users", "Admin");
        }

        //public IActionResult ExternalLogin(string provider, string returnUrl = null)
        //{
        //    var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { ReturnUrl = returnUrl });
        //    var properties = _userService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return Challenge(properties, provider);
        //}

        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        //{
        //    var info = await _userService.GetExternalLoginInfoAsync();

        //    var result = await _userService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey);
        //    if (result.Succeeded)
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then ask the user to create an account.
        //        ViewData["ReturnUrl"] = returnUrl;
        //        ViewData["LoginProvider"] = info.LoginProvider;
        //        var email = info.Principal.FindFirst(ClaimTypes.Email).Value;
        //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationVM { Email = email });
        //    }
        //}


    }
}