using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ChartViewModels
{
    public class DashboardVM
    {
        public List<SelectListItem> availableMonths { get; set; }
        public List<SelectListItem> userOptions { get; set; }
        public List<ChartVM> DataSets { get; set; }
        public string userToShow { get; set; }
        public string monthToShow { get; set; } = "";
    }
}
