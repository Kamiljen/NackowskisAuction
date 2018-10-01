using NackowskisAuctionHouse.BusinessLayer;
using NackowskisAuctionHouse.DAL.DbContext;
using NackowskisAuctionHouse.DAL.ModelsEf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.MessageService
{
    public class MessageService : IMessageService
    {
        private NackowskisDBContext _context;
        private IBusinessService _businessService;
        public MessageService(NackowskisDBContext context, IBusinessService businessService)
        {
            _context = context;
            _businessService = businessService;
        }

        public void PersistUserMessage(UserMessage message)
        {
            _context.UserMessages.Add(message);
            _context.SaveChanges();
        }

        public UserMessage CreateBidOverMessage(int auctionId, int bidId, string auctionTitle, string userName)
        {
            string title = "Du blev överbjuden på auktion " + auctionId + " " + auctionTitle;
            string message = "Ditt bud " + bidId + "på auktion " + auctionId +" " + auctionTitle + " blev överbjudet";
            //string message = "Ditt bud {0} på auktion {1} {2} blev överbjudet", (bidId, auctionId, auctionTitle);
            var model = new UserMessage {
                UserName = userName,
                Title = title,
                Message = message,
                TimeStamp = DateTime.Now,
                MessageRead = false,

            };
            return model;
        }

        public UserMessage CreateAuctionFinishesMessage(int auctionId, string auctionTitle, string userName)
        {
            string title = "Auktion " + auctionId + " " + auctionTitle + " går snart ut.";
            string message = "Passa på att lägga ett bud på auktion " + auctionId + " " + auctionTitle + " innan den går ut.";
            var model = new UserMessage
            {
                UserName = userName,
                Title = title,
                Message = message,
                TimeStamp = DateTime.Now,
                MessageRead = false,
            };
            return model;
        }

        public async Task<string> FindUserToMessage(int auctionId)
        {
            var bids = await _businessService.GetBids(auctionId);
            if (bids.Count > 1)
            {
                var userName = bids.OrderByDescending(x => x.Summa).Take(2).Reverse().First().Budgivare;
                return userName;
            }
            return null;
            
        }



        //public async void InitiateUserMessages(string userName)
        //{
        //    List<UserMessage> userMessages = new List<UserMessage>();
        //    var userBids = _context.AuctionBids.Where(x => x.User == userName).ToList();
        //    foreach (var bid in userBids)
        //    {
        //        var auctionAndBids = await _businessService.GetAuctionAndBids(bid.AuctionId);
        //        var highestBid = auctionAndBids.Bids.OrderByDescending(x => x.Summa).First();
               
      
        //        if (highestBid.Budgivare != userName)
        //        {
        //            userMessages.Add(CreateBidOverMessage(bid.AuctionId, bid.BidId, auctionAndBids.Auction.Titel, userName));
        //        }

        //    }

        //    foreach (var message in userMessages)
        //    {
        //        _context.UserMessages.Add
        //    }
        //}
      
    }
}
