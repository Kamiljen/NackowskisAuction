using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class SearchResultVM
    {
        public string SearchString { get; set; }
        public string SearchParam { get; set; }
        public string OrderBy { get; set; }
        public List<AuctionWithBidsVM> Results { get; set; }
    }
}
