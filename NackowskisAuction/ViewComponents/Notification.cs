using Microsoft.AspNetCore.Mvc;
using NackowskisAuctionHouse.DAL.IdentityModels;
using NackowskisAuctionHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewComponents
{
   
    public class NotificationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
           


            return View();
        }
    }
}
