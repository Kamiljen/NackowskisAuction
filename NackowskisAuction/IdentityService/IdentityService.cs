﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NackowskisAuctionHouse.DAL.IdentityModels;
using NackowskisAuctionHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<List<UserVM>> GetUsersInRoleAsync()
        {
            var model = new List<UserVM>();
            var roles = _roleManager.Roles.ToList();
            var users = _userManager.Users;

            var regularModel = new UserVM { Role = "Regular"};
            var adminModel = new UserVM { Role = "Admin" };
            var adminUsers = new List<AppUser>();
            var regualrUsers = new List<AppUser>();

            foreach (var user in users)
            {
                //_userManager.IsInRoleAsync("Regular")
                var result = await _userManager.GetRolesAsync(user);
                if (result.Count != 0)
                {
                    if (result.First() == "Admin")
                    {
                        adminUsers.Add(user);
                    }
                    else 
                    {
                        regualrUsers.Add(user);
                    }
                }
                else
                {

                    var result2 = await _userManager.AddToRoleAsync(user, "Regular");
                    regualrUsers.Add(user);
                }
               

            }
            adminModel.Users = adminUsers;
            regularModel.Users = regualrUsers;
            model.Add(adminModel);
            model.Add(regularModel);
            //foreach (var roleName in roles)
            //{
            //    var tempUsers = await _userManager.GetUsersInRoleAsync(roleName.Name);
            //    model.Add(new UserVM { Role = roleName.Name, Users = tempUsers.ToList()});
            //}
            return model;
        }

        public List<AppUser> GetAlUser()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityResult> AssignRoleAsync(AppUser user)
        {
            return await _userManager.AddToRoleAsync(user, "Regular");
        }

        public async Task<AppUser> RegisterUser(RegisterVM input)
        {
            var appUser = new AppUser
            {
                UserName = input.Email,
                NormalizedUserName = input.Email.ToUpper(),
                Email = input.Email,
                NormalizedEmail = input.Email
            };

            var result = await _userManager.CreateAsync(appUser, input.Password);
            if (result.Succeeded)
            {
                var roleResult = await AssignRoleAsync(appUser);
                if (roleResult.Succeeded)
                {
                    return appUser;
                }
                
            }
            return null;
          
        }

        public async Task<string> SignInAsync(AppUser user, bool isPersistent)
        {
            await _signInManager.SignInAsync(user, isPersistent);
            return "finnished!";

        }
        public async Task<SignInResult> SignInAsync(SignInVM Input)
        {
            return await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        }

        public async void SignOut()
        {
           await _signInManager.SignOutAsync();
          
        }

        public AppUser GetUserWithId(string userId)
        {
            return _userManager.Users.SingleOrDefault(x => x.Id == userId);
        }
       
        public async Task<IdentityResult> UpdateUserRole(string userId, string oldRole, string newRole)
        {
            var user = GetUserWithId(userId);
            await _userManager.RemoveFromRoleAsync(user, oldRole);
            return await _userManager.AddToRoleAsync(user, newRole);
        }
    }
}
