using Microsoft.AspNetCore.Mvc;
using NackowskisAuctionHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewComponents
{
    public class BidCalculatorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int bidSum, int auctionId)
        {
            var model = new BidVM();
            model.calculatedSum = Convert.ToInt32(bidSum * 1.05);
            model.auctionId = auctionId;

            return View(model);
        }
    }

}
