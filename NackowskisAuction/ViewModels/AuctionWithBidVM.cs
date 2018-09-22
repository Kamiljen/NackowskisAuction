using NackowskisAuctionHouse.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class AuctionWithBidsVM
    {
        public Auction Auction { get; set; }
        public List<Bid> Bids { get; set; }
    }
}
