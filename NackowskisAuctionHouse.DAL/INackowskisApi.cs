using NackowskisAuctionHouse.DAL.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.DAL
{
    public interface INackowskisApi
    {
        Task<HttpResponseMessage> CreateAuction(Auction model);
        Task<HttpResponseMessage> EditAuction(Auction model);

        Task<HttpResponseMessage> CreateBid(Bid bid);
        Task<HttpResponseMessage> DeleteBid(int bidId);
        Task<HttpResponseMessage> DeleteAuction(int auctionId);

        Task<List<Auction>> GetAuctions();
        Task<Auction> GetAuction(int auctionId);
        Task<List<Bid>> GetBids(int auctionId);
        Task<Bid> GetBid(int auctionId, int bidId);
        

    }
}
