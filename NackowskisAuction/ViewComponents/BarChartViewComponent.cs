using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NackowskisAuctionHouse.ChartViewModels;

namespace NackowskisAuctionHouse.ViewComponents
{
   
    public class BarChartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ChartVM();
            return View(model);
        }
    }
}
