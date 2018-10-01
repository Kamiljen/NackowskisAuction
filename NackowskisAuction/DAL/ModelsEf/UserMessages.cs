using System;
using System.Collections.Generic;
using System.Text;

namespace NackowskisAuctionHouse.DAL.ModelsEf
{
    public class UserMessage
    {
        public int UserMessageId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool MessageRead { get; set; }
    }
}
