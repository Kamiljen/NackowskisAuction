using Microsoft.AspNetCore.Mvc;
using NackowskisAuctionHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewComponents
{
    public class MessageViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new List<MessageVM> {
                new MessageVM {Title = "Du blev överbjuden på auktion 3432", Message = "Du blev överbjuden på auktion 3432 Rolex Datejust 1787. Aktuellt bud är 23000", TimeStamp = DateTime.Now },
                new MessageVM {Title = "Du blev överbjuden på auktion 3216", Message = "Du blev överbjuden på auktion 3216 Omega Seamaster. Aktuellt bud är 12300", TimeStamp = DateTime.Now}
            };
            return View(model);
        }
    }
}
