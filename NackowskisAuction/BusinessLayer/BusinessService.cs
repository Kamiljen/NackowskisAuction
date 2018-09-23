using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NackowskisAuctionHouse.DAL;
using NackowskisAuctionHouse.DAL.Models;
using NackowskisAuctionHouse.ViewModels;
using NackowskisAuctionHouse.ExtensionMethods;
using NackowskisAuctionHouse.BusinessLayer;
using NackowskisAuctionHouse.ChartViewModels;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace NackowskisAuctionHouse.BusinessLayer
{
    public class BusinessService : IBusinessService
    {
        private  NackowskisApi _api;
        public BusinessService()
        {
            _api = new NackowskisApi();
        }

        public Task<HttpResponseMessage> CreateAuction(Auction model)
        {

            return _api.CreateAuction(model);
        }

        public Task<HttpResponseMessage> CreateBid(int auctionId, int sum, string user)
        {
            var model = new Bid { AuktionID = auctionId, Summa = sum, Budgivare = user };
            return _api.CreateBid(model);
        }

        public Task<HttpResponseMessage> DeleteAuction(int auctionId)
        {
            return _api.DeleteAuction(auctionId);
        }

        public Task<HttpResponseMessage> DeleteBid(int bidId)
        {
            return _api.DeleteBid(bidId);
        }

        public Task<HttpResponseMessage> EditAuction(Auction model)
        {
            return _api.EditAuction(model);
        }

        public async Task<SearchResultVM> FindAuctions(string searchParam = " ", string orderBy = " ", string searchString = " ")
        {
            var searchResults = new SearchResultVM();
            searchResults.Results = new List<AuctionWithBidsVM>();
            var allAuctions = await GetAuctions();

            var filteredAuctions = allAuctions
                .WhereIf(searchParam == "Titel", x => x.Titel.ToLower().Contains(searchString.ToLower()))
                .WhereIf(searchParam == "Beskrivning", x => x.Beskrivning.ToLower().Contains(searchString.ToLower()))
                .WhereIf(string.IsNullOrEmpty(searchParam), x => x.Titel.ToLower().Contains(searchString.ToLower()) || x.Beskrivning.ToLower().Contains(searchString.ToLower())).ToList();
                

            foreach (var auction in filteredAuctions)
            {
                auction.SlutDatum = DateTime.Parse(auction.SlutDatumString);
                auction.StartDatum = DateTime.Parse(auction.StartDatumString);
                var auctionBids = await _api.GetBids(auction.AuktionID);
                if (auctionBids.Count == 0)
                {
                    auctionBids.Add(new Bid());
                }
                searchResults.Results.Add(new AuctionWithBidsVM
                {
                    Auction = auction,
                    Bids = auctionBids.OrderByDescending(x => x.Summa).ToList()
                
                });
                

            }
            var orderedResults = (orderBy == "Utropspris") ? searchResults.Results.OrderBy(x => x.Auction.Utropspris) : searchResults.Results.OrderBy(x => x.Auction.SlutDatum);
            searchResults.Results = orderedResults.ToList();

            return searchResults;
        }

        public async Task<DashboardVM> ActivityLineChart(int month, string userName)
        {
            var controllerSet = new DashboardVM();
            var alAuctions = await _api.GetAuctions();
            var selectedDates = alAuctions.WhereIf(month != 0 ,x => DateTime.Parse(x.SlutDatumString).Month == month)
                .WhereIf( userName != "Alla", x =>x.SkapadAv == userName)
                .ToList();
            var bids = await GetBids(selectedDates);
            controllerSet.DataSets = await GetDataSet(selectedDates, bids);
            var availableMonths = getAvailableMonths(alAuctions);
            controllerSet.availableMonths = new List<SelectListItem>();
            foreach (var date in availableMonths)
            {
                
                controllerSet.availableMonths.Add(new SelectListItem {
                    Text = new DateTime(2018, int.Parse(date), 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("sv")),
                    Value = date
                    });
            }
            controllerSet.userOptions = new List<SelectListItem>();
            controllerSet.userOptions.Add(new SelectListItem { Text = "Alla användare", Value = "Alla" });
            controllerSet.userOptions.Add(new SelectListItem { Text = userName, Value = userName });
            return controllerSet;
        }

        public List<string> getAvailableMonths(List<Auction> auctions)
        {
            return auctions.Select(x =>x.SlutDatumString.Substring(5,2)).Distinct().ToList();
            
        }

        public async Task<List<ChartVM>> GetDataSet(List<Auction> auctions, List<List<Bid>> bids)
        {
            var data = new List<ChartVM>();

            int utropsPris = 0;
            int slutPris = 0;


            foreach (var item in auctions)
            {
                List<Bid> auctionBids = new List<Bid>();
                foreach (var list in bids)
                {
                    auctionBids = list.Where(x => x.AuktionID == item.AuktionID).ToList();
                }
                
                var highestBid = (auctionBids.Count != 0) ? auctionBids.OrderBy(x => x.Summa).First().Summa : item.Utropspris;
                utropsPris += item.Utropspris;
                slutPris += highestBid;
                

               
                //data.Add(new ChartVM { DimensionOne = "Utropspris", Quantity = (highestBid / item.Utropspris) });
            }
            
            data.Add(new ChartVM { DimensionOne = "Utropspris", Quantity = utropsPris });
            data.Add(new ChartVM { DimensionOne = "Differans", Quantity = slutPris - utropsPris });
            data.Add(new ChartVM { DimensionOne = "SlutPris", Quantity = slutPris });

            return data;
        }

        public async Task<Auction> GetAuction(int auctionId)
        {
            return await _api.GetAuction(auctionId);
        }

        public async Task<AuctionWithBidsVM> GetAuctionAndBids(int auctionId)
        {
            var model = new AuctionWithBidsVM();
            var bids = await _api.GetBids(auctionId);
            if (bids.Count != 0)
            {
                model.Bids = bids;
            }
            else
            {
                model.Bids.Add( new Bid());
            }
            
            model.Auction = await _api.GetAuction(auctionId);
            model.Auction.SlutDatum = Convert.ToDateTime(model.Auction.SlutDatumString);
            model.Auction.StartDatum = Convert.ToDateTime(model.Auction.StartDatumString);
            
            return model;
        }

        public Task<List<Auction>> GetAuctions()
        {
            return _api.GetAuctions();
        }

        public async Task<Bid> GetBid(int auctionId, int bidId)
        {
            return await _api.GetBid(auctionId, bidId);
        }

        public async Task<List<Bid>> GetBids(int auctionId)
        {
            return await _api.GetBids(auctionId);
        }

        public async Task<List<List<Bid>>> GetBids(List<Auction> auctions)
        {
            var bidResult = new List<List<Bid>>();
            foreach(var auction in auctions)
            {
                var temp = await _api.GetBids(auction.AuktionID);
                bidResult.Add(temp);
            }
            return bidResult;
        }

        public async Task<bool> HasBids(int auctionId)
        {
            var bids = await _api.GetBids(auctionId);
            return bids.Count > 0;
        }
    }
}
