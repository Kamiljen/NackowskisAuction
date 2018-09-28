using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NackowskisAuctionHouse.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult ReturnNotification()
        {
            return View();
        }
    }
}