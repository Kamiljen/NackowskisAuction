using NackowskisAuctionHouse.DAL.ModelsEf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.DAL
{
    public interface IRepositoryService
    {
        void AuctionBid(AuctionBid model);
    }
}
