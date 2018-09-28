using System;
using System.Collections.Generic;
using System.Text;

namespace NackowskisAuctionHouse.DAL.ModelsEf
{
    public class AuctionBid
    {
        public int AuctionId { get; set; }
        public int BidId { get; set; }
        public string User { get; set; }
    }
}
