using Microsoft.AspNetCore.Mvc;
using NackowskisAuctionHouse.DAL.IdentityModels;
using NackowskisAuctionHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewComponents
{
   
    public class RoleUpdaterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AppUser user, string oldRole, string newRole)
        {
            var model = new UserRoleVM { User = user, UserName = user.UserName, UserId = user.Id, OldRole= oldRole, NewRole = newRole};


            return View(model);
        }
    }
}
