using NackowskisAuctionHouse.DAL.ModelsEf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.MessageService
{
    public interface IMessageService
    {
        void PersistUserMessage(UserMessage message);
        UserMessage CreateBidOverMessage(int auctionId, int bidId, string auctionTitle, string userName);
        UserMessage CreateAuctionFinishesMessage(int auctionId, string auctionTitle, string userName);
        Task<string> FindUserToMessage(int auctionId);
    }
}
