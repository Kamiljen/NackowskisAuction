using NackowskisAuctionHouse.DAL.DbContext;
using NackowskisAuctionHouse.DAL.ModelsEf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.DAL.RepoService
{
    public class RepositoryService : IRepositoryService
    {
        private NackowskisDBContext _context;

        public RepositoryService(NackowskisDBContext context)
        {
            _context = context;
        }


        public void AuctionBid(AuctionBid model)
        {
            _context.AuctionBids.Add(model);
            _context.SaveChanges();
        }
    }
}
