﻿using NackowskisAuctionHouse.ChartViewModels;
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
        Task<HttpResponseMessage> CreateAuction(Auction model);
        Task<HttpResponseMessage> EditAuction(Auction model);
        Task<HttpResponseMessage> DeleteBid(int bidId);
        Task<HttpResponseMessage> DeleteAuction(int auctionId);

        Task<HttpResponseMessage> CreateBid(int auctionId, int sum, string user);

        Task<SearchResultVM> FindAuctions(string searchParam, string orderBy, string searchString = "");
        Task<List<Auction>> GetAuctions();
        Task<Auction> GetAuction(int auctionId);
        Task<List<Bid>> GetBids(int auctionId);
        Task<Bid> GetBid(int auctionId, int bidId);
        Task<bool> HasBids(int auctionId);
        Task<AuctionWithBidsVM> GetAuctionAndBids(int auctionId);

        //line chart
        Task<List<ChartVM>> GetDataSet(List<Auction> auctions, List<List<Bid>> bids);
        Task<DashboardVM> ActivityLineChart(int month, string userName);
        List<string> getAvailableMonths(List<Auction> auctions);

    }
}