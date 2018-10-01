using Microsoft.AspNetCore.Identity;
using NackowskisAuctionHouse.DAL.IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NackowskisAuctionHouse.DAL.DbContext
{
    public static class ApplicationDbInitializer
    {

        
        public static void SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync("alex@test.com").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "alex@test.com",
                    Email = "alex@test.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "!Nackademin002").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            if (userManager.FindByEmailAsync("gustav@test.com").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "gustav@test.com",
                    Email = "gustav@test.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "!Nackademin002").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Regular").Wait();
                }
            }
        }
    }
}

