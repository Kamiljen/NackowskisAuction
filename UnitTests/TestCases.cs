using System;
using System.Collections.Generic;
using NackowskisAuctionHouse.BusinessLayer;
using NackowskisAuctionHouse.DAL.Models;
using Xunit;

namespace UnitTests
{
    //public class TestCases
    //{
        
    //    public TestCases()
    //    {

    //    }

    //    [Fact]
    //    public void CheckDifferenceSucceed()
    //    {

    //        int expected = 8771;


    //        var _businessService = new BusinessService();

    //        var bids = new List<Bid> { new Bid { Summa = 2222, AuktionID = 1 }, new Bid { Summa = 4599, AuktionID = 2 }, new Bid { Summa = 5600, AuktionID = 3} };
    //        var bidList = new List<List<Bid>>();
    //        bidList.Add(bids);
    //        var auctions = new List<Auction> { new Auction { Utropspris = 1000, AuktionID = 1 }, new Auction { Utropspris = 1450, AuktionID = 2 }, new Auction { Utropspris = 1200, AuktionID = 3 } };
    //        var temp = _businessService.GetDataSet(auctions, bidList).Result;

    //        int actual = 0;
    //        foreach (var item in temp)
    //        {
    //            actual = (item.DimensionOne == "Differans") ? item.Quantity : actual;
    //        }

    //        Assert.Equal(expected, actual);
    //    }

    //    [Fact]
    //    public void CheckDifferenceFail()
    //    {

    //        int expected = 87712;


    //        var _businessService = new BusinessService();

    //        var bids = new List<Bid> { new Bid { Summa = 2222, AuktionID = 1 }, new Bid { Summa = 4599, AuktionID = 2 }, new Bid { Summa = 5600, AuktionID = 3 } };
    //        var bidList = new List<List<Bid>>();
    //        bidList.Add(bids);
    //        var auctions = new List<Auction> { new Auction { Utropspris = 1000, AuktionID = 1 }, new Auction { Utropspris = 1450, AuktionID = 2 }, new Auction { Utropspris = 1200, AuktionID = 3 } };
    //        var temp = _businessService.GetDataSet(auctions, bidList).Result;

    //        int actual = 0;
    //        foreach (var item in temp)
    //        {
    //            actual = (item.DimensionOne == "Differans") ? item.Quantity : actual;
    //        }

    //        Assert.Equal(expected, actual);
    //    }

    //}
}
