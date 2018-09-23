using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NackowskisAuctionHouse.BusinessLayer;
using NackowskisAuctionHouse.DAL.Models;
using NackowskisAuctionHouse.IdentityService;
using NackowskisAuctionHouse.ViewModels;
using NackowskisAuctionHouse.ChartViewModels;
using System.Security.Claims;

namespace NackowskisAuctionHouse.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IBusinessService _businessService;
        private IIdentityService _userService;

        public AdminController(IBusinessService businessService, IIdentityService userService)
        {
            _businessService = businessService;
            _userService = userService;
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult BarChart()
        {
            return View();
        }

        public async Task<JsonResult> GetDataSet()
        {
            var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var date = int.Parse(DateTime.Now.ToString("MM-dd-yyyy").Substring(0, 2));
            var data = await _businessService.ActivityLineChart(date, HttpContext.User.FindFirstValue(ClaimTypes.Name));
            return Json (new { data });
        }

        //public async Task<IActionResult> GetDataSet()
        //{
            
        //    var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
        //    var date = int.Parse(DateTime.Now.ToString("MM-dd-yyyy").Substring(0, 2));
        //    var data = await _businessService.ActivityLineChart(date, HttpContext.User.FindFirstValue(ClaimTypes.Name));
        //    return View(data);

        //}
        //[HttpPost]
        //public async Task<JsonResult> GetDataSet(DashboardVM input)
        //{

        //    var date = (int.Parse(input.monthToShow) == 0) ? int.Parse(DateTime.Now.ToString("MM-dd-yyyy").Substring(0, 2)) : int.Parse(input.monthToShow);

        //    var data = await _businessService.ActivityLineChart(date, input.userToShow);
        //    return Json (new { data});
        //}
        //public async Task<IActionResult> LineChart()
        //{
        //    var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
        //    var date = int.Parse(DateTime.Now.ToString("MM-dd-yyyy").Substring(0, 2));
        //    var data = await _businessService.ActivityLineChart(date, HttpContext.User.FindFirstValue(ClaimTypes.Name));
        //    return View(data);
        //}
        [HttpPost]
        public async Task<IActionResult> LineChart(DashboardVM input)
        {

            var date = (int.Parse(input.monthToShow) == 0) ? int.Parse(DateTime.Now.ToString("MM-dd-yyyy").Substring(0, 2)) : int.Parse(input.monthToShow);

            var data = await _businessService.ActivityLineChart(date, input.userToShow);
            return PartialView("_LineChartPartial", data);
        }

        //public IActionResult CreateAuction(Auction inputModel, string rule = "")
        //{
        //    var model = new Auction();
        //    if (inputModel != null)
        //    {
        //        model = inputModel;
        //    }

        //    return View(model);
        //}
        public async Task<IActionResult> Users()
        {
            return View(await _userService.GetUsersInRoleAsync());
        }

        
        public async Task<IActionResult> CreateAuction(Auction model)
        {
            if (ModelState.IsValid)
            {
                model.SlutDatumString = model.SlutDatum.Add(model.StartTid).ToString("yyyy-MM-dd HH:mm:ss");
                model.StartDatumString = model.StartDatum.Add(model.SlutTid).ToString("yyyy-MM-dd HH:mm:ss");
                var result = await _businessService.CreateAuction(model);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("FindAuctions", new RouteValueDictionary(new { Controller = "Home", Action = "FindAuctions", searchString = model.Titel , searchParam = "Titel" }));
                }
             
            }
            return View(model);
            //return RedirectToAction("CreateAuction", new RouteValueDictionary(new { Controller = "Admin", Action = "CreateAuction", inputModel = (Auction)model }));

        }
        [HttpGet]
        public async Task<IActionResult> EditAuction(int auctionId)
        {
            var model = await _businessService.GetAuction(auctionId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PushEditAuction(Auction model)
        {
            if (ModelState.IsValid)
            {
                model.SlutDatumString = model.SlutDatum.ToString();
                model.StartDatumString = model.StartDatum.ToString();
                var result = await _businessService.EditAuction(model);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAuction", new RouteValueDictionary(new { Controller = "Home", Action = "GetAuction", auctionId = model.AuktionID }));
                }
            }
            return RedirectToAction("EditAuction", new RouteValueDictionary(new { Controller = "Admin", Action = "EditAuction", auctionId = model.AuktionID }));
        }

        
        public async Task<IActionResult> DeleteAuction(int auctionId)
        {
             
            if (!await _businessService.HasBids(auctionId))
            {
                var result = await _businessService.DeleteAuction(auctionId);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("FindAuctions", new RouteValueDictionary(new { Controller = "Home", Action = "FindAuctions", searchInput = "t" }));
                }
            }
            return RedirectToAction("GetAuction" , new RouteValueDictionary( new { Controller = "Home", Action = "GetAuction", auctionId = auctionId }));
        }
  

        public IActionResult EditAuction()
        {

            return View();
        }

        
    }
}