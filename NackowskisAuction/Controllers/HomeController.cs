using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NackowskisAuction.Models;
using NackowskisAuctionHouse.BusinessLayer;
using NackowskisAuctionHouse.DAL.Models;
using NackowskisAuctionHouse.ViewModels;

namespace NackowskisAuctionHouse.Controllers
{
    public class HomeController : Controller
    {
        private IBusinessService _businessService;

        public HomeController(IBusinessService businessService)
        {
            _businessService = businessService;
        }



        public IActionResult OrderSerachResults(List<AuctionWithBidsVM> list, string orderBy)
        {
            switch (orderBy)
            {
                case "Slutdatum":
                    list.OrderBy(c => c.Auction.SlutDatum);
                    break;
                case "Utropspris":
                    list.OrderBy(c => c.Auction.Utropspris);
                    break;
            }
            return PartialView(list);
        }

        public async Task<IActionResult> Index()
        {

            var auctions = await _businessService.GetActiveAuctionsAndBids();

            return View(auctions);
        }
        //[HttpPost]
        //public async Task<IActionResult> GetAuction(Auction auction)
        //{

        //    return View("AuctionDetails", await _businessService.GetAuction(auction.AuktionID));
        //}
        [Authorize]
        [HttpGet("GetAuction/{auctionid}")]
        public async Task<IActionResult> GetAuction(int auctionId)
        {
           
            return View("AuctionDetails", await _businessService.GetAuctionAndBids(auctionId));
        }

        
        [HttpPost]
        public async Task<IActionResult> PlaceBid(BidVM bid)
        {
            //returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (bid.bidSum < bid.oldBid)
                {
                    var result = await _businessService.CreateBid(sum: bid.bidSum, user: User.Identity.Name, auctionId: bid.auctionId);
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "budet måste vara högre än det högsta budet");
                }
               
            }
            return View("GetAuction", bid.auctionId);



        }
        [HttpPost]
        public async Task<IActionResult> FindAcutionsWithParams(SearchVM model)
        {
            var searchString = (model.SearchString != null) ? model.SearchString : "";
            var searchParam = (model.SearchParam != null) ? model.SearchParam : "Alla";
            var result = await _businessService.FindAuctions(model.SearchParam, model.OrderBy, searchString);
            return PartialView("_SearchListPartial", result);
        }



        public async Task<IActionResult> FindAuctions(string searchString = " ", string searchParam = " ")
        {
            var result = await  _businessService.FindAuctions(searchParam, "", searchString);
            result.SearchString = searchString;
            return View("SearchResults", result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
