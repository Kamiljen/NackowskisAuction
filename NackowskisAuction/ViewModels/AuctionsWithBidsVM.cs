using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class AuctionsWithBidsVM
    {
        public AuctionsWithBidsVM()
        {
            Auctions = new List<AuctionWithBidsVM>();
        }

        public List<AuctionWithBidsVM> Auctions { get; set; }
    }
}
