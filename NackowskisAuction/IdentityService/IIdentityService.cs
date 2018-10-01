﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using NackowskisAuctionHouse.DAL.IdentityModels;
using NackowskisAuctionHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.IdentityService
{
    public interface IIdentityService
    {
        Task<bool> SignInAsync(AppUser user, bool isPersistent);
        Task<SignInResult> SignInAsync(SignInVM Input);
        Task<string> SignOut();
        Task<AppUser> RegisterUser(RegisterVM input);
        Task<IdentityResult> AssignRoleAsync(AppUser user);

        List<AppUser> GetAlUser();
        Task<List<UserVM>> GetUsersInRoleAsync();
        Task<IdentityResult> UpdateUserRole(string userId, string oldRole, string newRole);
        AppUser GetUserWithId(string userId);




    }
}
