using System;
using System.Collections.Generic;
using System.Text;

namespace NackowskisAuctionHouse.DAL.ModelsEf
{
    public class AuctionCategory
    {
        public int AuctionCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int AuctionId { get; set; }
    }
}
