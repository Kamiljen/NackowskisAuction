using NackowskisAuctionHouse.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class BidVM
    {
        public int calculatedSum { get; set; }
        public int auctionId { get; set; }

        [ValidBid]
        [Display(Name="Ditt bud")]
        public int bidSum { get; set; }
        
    }
}
