using NackowskisAuctionHouse.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class AuctionWithBidsVM
    {
        public AuctionWithBidsVM()
        {
            Bids = new List<Bid>();
            Auction = new Auction();
        }
        public Auction Auction { get; set; }
        public List<Bid> Bids { get; set; }
        public int HighestBid { get; set; }
        public string VCErrorMsg { get; set; }  

    }
}
