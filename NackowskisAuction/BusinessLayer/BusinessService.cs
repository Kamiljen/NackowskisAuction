﻿using System;
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

        public Task<HttpResponseMessage> CreateAuction(CreateAuctionVM model)
        {
            var apiModel = new Auction {
                Titel = model.Titel,
                Beskrivning = model.Beskrivning,
                StartDatumString = model.StartDatum.Add(model.SlutTid).ToString("yyyy-MM-dd HH:mm:ss"),
                SlutDatumString = model.SlutDatum.Add(model.StartTid).ToString("yyyy-MM-dd HH:mm:ss"),
                Utropspris = model.Utropspris,
                SkapadAv = model.SkapadAv,
                Gruppkod = model.Gruppkod
        };
            

            return _api.CreateAuction(apiModel);
        }

        public Task<HttpResponseMessage> CreateBid(int auctionId, int sum, string user)
        {
            var model = new Bid { AuktionID = auctionId, Summa = sum, Budgivare = user };
            return _api.CreateBid(model);
        }

        public async Task<AuctionsWithBidsVM> GetActiveAuctionsAndBids()
        {
            var model = new AuctionsWithBidsVM();
            var auctions = await GetAuctions();
            var activeAuctions = auctions.Where(x => DateTime.Parse(x.SlutDatumString) > DateTime.Today);
            foreach (var auction in activeAuctions)
            {
                var tempBids = await GetBids(auction.AuktionID);

                var highestBid = (tempBids.Count != 0) ? tempBids.OrderByDescending(x => x.Summa).First().Summa :  0;
                
                model.Auctions.Add(new AuctionWithBidsVM
                {
                    Auction = auction,
                    HighestBid = highestBid
                });

            }

            return model;
        }

        public async Task<EditAuctionVM> GetAuctionEditModel(int auctionId)
        {
            var model = await GetAuction(auctionId);
            var startTid = model.StartDatumString.Substring(11, 5);
            var slutTid = model.SlutDatumString.Substring(11, 5);
            var editModel = new EditAuctionVM
            {
                AuctionId = model.AuktionID,
                Titel = model.Titel,
                Beskrivning = model.Beskrivning,
                StartDatum = DateTime.Parse(model.StartDatumString.Substring(0,10)),
                StartTid = TimeSpan.Parse(startTid),
                SlutDatum = DateTime.Parse(model.SlutDatumString.Substring(0, 10)),
                SlutTid = TimeSpan.Parse(slutTid),
                Utropspris = model.Utropspris,
                SkapadAv = model.SkapadAv,
                Gruppkod = model.Gruppkod
            };
            return editModel;
        }
        public Task<HttpResponseMessage> DeleteAuction(int auctionId)
        {
            return _api.DeleteAuction(auctionId);
        }

        public Task<HttpResponseMessage> DeleteBid(int bidId)
        {
            return _api.DeleteBid(bidId);
        }

        public Task<HttpResponseMessage> EditAuction(EditAuctionVM model)
        {
            var apiModel = new Auction
            {
                AuktionID = model.AuctionId,
                Titel = model.Titel,
                Beskrivning = model.Beskrivning,
                StartDatumString = model.StartDatum.Add(model.SlutTid).ToString("yyyy-MM-dd HH:mm:ss"),
                SlutDatumString = model.SlutDatum.Add(model.StartTid).ToString("yyyy-MM-dd HH:mm:ss"),
                Utropspris = model.Utropspris,
                SkapadAv = model.SkapadAv,
                Gruppkod = model.Gruppkod
            };
            return _api.EditAuction(apiModel);
        }

        public async Task<SearchResultVM> FindAuctions(string searchParam = " ", string orderBy = " ", string searchString = " ")
        {
            var searchResults = new SearchResultVM();
            searchResults.Results = new List<AuctionWithBidsVM>();
            var allAuctions = await GetAuctions();

            var filteredAuctions = allAuctions
                .WhereIf(searchParam == "Titel", x => x.Titel.ToLower().Contains(searchString.ToLower()))
                .WhereIf(searchParam == "Beskrivning", x => x.Beskrivning.ToLower().Contains(searchString.ToLower()))
                .Where( x => x.Titel.ToLower().Contains(searchString.ToLower()) || x.Beskrivning.ToLower().Contains(searchString.ToLower())).ToList();
                

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

      

        public async Task<DashboardVM> ActivityBarChart(int month, string userName)
        {
            var controllerSet = new DashboardVM();
            var alAuctions = await _api.GetAuctions();
            var selectedDates = alAuctions.WhereIf(month != 0, x => DateTime.Parse(x.SlutDatumString).Month == month)
                .WhereIf(userName != "Alla", x => x.SkapadAv == userName)
                .ToList();
            var bids = await GetBids(selectedDates);
            controllerSet.DataSets = await GetDataSet(selectedDates, bids);
            controllerSet.availableMonths = GetAvailableMonthsSelectList(alAuctions);
            controllerSet.userOptions = GetUserOptionsSelectList(userName);
            controllerSet.userToShow = userName;
            controllerSet.monthToShow = month.ToString();

            return controllerSet;
        }

        public async Task<List<SelectListItem>> GetAvailableMonthsSelectList()
        {
            var auctions = await _api.GetAuctions();
            var temp = new List<SelectListItem>();
            var availableMonths = auctions.Select(x => x.SlutDatumString.Substring(5, 2)).Distinct().ToList();
            foreach (var date in availableMonths)
            {
                if (int.Parse(date) <= DateTime.Now.Month )
                {
                    temp.Add(new SelectListItem
                    {
                        Text = new DateTime(2018, int.Parse(date), 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("sv")),
                        Value = date
                    });
                }
                
            }

            return temp;
            
        }
        public  List<SelectListItem> GetAvailableMonthsSelectList(List<Auction> auctions)
        {
            
            var temp = new List<SelectListItem>();
            var availableMonths = auctions.Select(x => x.SlutDatumString.Substring(5, 2)).Distinct().ToList();
            foreach (var date in availableMonths)
            {

                if(int.Parse(date) <= DateTime.Now.Month)
                {
                    temp.Add(new SelectListItem
                    {
                        Text = new DateTime(2018, int.Parse(date), 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("sv")),
                        Value = date
                    });
                }
            }

            return temp;

        }
        public List<SelectListItem> GetUserOptionsSelectList(string userName)
        {
            var temp = new List<SelectListItem>();
            temp.Add(new SelectListItem { Text = "Alla användare", Value = "Alla" });
            temp.Add(new SelectListItem { Text = userName, Value = userName });
            return temp;
        }

        public async Task<List<ChartVM>> GetDataSet(List<Auction> auctions, List<List<Bid>> bids)
        {
            var data = new List<ChartVM>();

            int utropsPris = 0;
            int slutPris = 0;


            foreach (var item in auctions)
            {
                List<Bid> auctionBids = new List<Bid>();
                foreach (var lists in bids)
                {
                    foreach(var bid in lists)
                    {
                        if (bid.AuktionID == item.AuktionID)
                        {
                            auctionBids.Add(bid);
                        }
                         
                    }
                    
                }
                
                var highestBid = (auctionBids.Count != 0) ? auctionBids.OrderByDescending(x => x.Summa).First().Summa : 0;
                if (highestBid != 0)
                {
                    utropsPris += item.Utropspris;
                    slutPris += highestBid;
                }
               
                

               
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
                model.HighestBid = bids.OrderByDescending(x => x.Summa).First().Summa;
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
