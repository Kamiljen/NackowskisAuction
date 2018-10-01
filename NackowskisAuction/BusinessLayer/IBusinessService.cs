using Microsoft.AspNetCore.Mvc.Rendering;
using NackowskisAuctionHouse.ChartViewModels;
using NackowskisAuctionHouse.DAL.Models;
using NackowskisAuctionHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.BusinessLayer
{
    public interface IBusinessService
    {
        Task<HttpResponseMessage> CreateAuction(CreateAuctionVM model);
        Task<HttpResponseMessage> EditAuction(Auction model);
        Task<HttpResponseMessage> DeleteBid(int bidId);
        Task<HttpResponseMessage> DeleteAuction(int auctionId);

       

        Task<HttpResponseMessage> CreateBid(int auctionId, int sum, string user);
        void InititeUserBid(BidVM bid, string userName);
        void PersistUserBid(int auctionId, Bid bid, string userName);

        Task<AuctionsWithBidsVM> GetActiveAuctionsAndBids();
        

        Task<SearchResultVM> FindAuctions(string searchParam, string orderBy, string searchString = "");
        Task<List<Auction>> GetAuctions();
        Task<Auction> GetAuction(int auctionId);
        Task<List<Bid>> GetBids(int auctionId);
        Task<Bid> GetHighestBidByUser(int auctionId, string userName);
        Task<bool> HasBids(int auctionId);
        Task<AuctionWithBidsVM> GetAuctionAndBids(int auctionId);

        //line chart
        Task<List<ChartVM>> GetDataSet(List<Auction> auctions, List<List<Bid>> bids);
        Task<DashboardVM> ActivityBarChart(int month, string userName);
        List<SelectListItem> GetAvailableMonthsSelectList(List<Auction> auctions);
        Task<List<SelectListItem>> GetAvailableMonthsSelectList();
        List<SelectListItem> GetUserOptionsSelectList(string userName);

    }
}
