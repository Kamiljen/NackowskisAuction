using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class SearchVM
    {
        public string OrderBy { get; set; }
        public string SearchParam { get; set; }
        public string SearchString { get; set; }
        public List<SelectListItem> SearchParams { get; set; }
        public List<SelectListItem> OrderBys { get; set; }
    }
}
