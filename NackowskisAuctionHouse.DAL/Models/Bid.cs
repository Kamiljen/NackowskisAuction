using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NackowskisAuctionHouse.DAL.Models
{
    [DataContract]
    public class Bid
    {
        [DataMember]
        public int BudID { get; set; }
        [DataMember]
        public int Summa { get; set; }
        [DataMember]
        public int AuktionID { get; set; }
        [DataMember]
        public string Budgivare { get; set; }
    }
}
