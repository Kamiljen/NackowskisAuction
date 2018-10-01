using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace NackowskisAuctionHouse.DAL.Models
{
    [DataContract]
    public class Auction
    {
        [DataMember]
        public int AuktionID { get; set; }

        [DataMember]
        public string Titel { get; set; }

        [DataMember]
        public string Beskrivning { get; set; }


        [DataMember(Name = "StartDatum")]
        public string StartDatumString { get; set; }


        [DataMember(Name = "SlutDatum")]
        public string SlutDatumString { get; set; }

        [DataMember]
        public int Gruppkod { get; set; }

        [DataMember]
        public int Utropspris { get; set; }

        [DataMember]
        [Display(Name = "Skapad av")]
        public string SkapadAv { get; set; }

       
        public DateTime StartDatum { get; set; }
        public TimeSpan StartTid { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:MM-DD-yyyy HH:MM}", ApplyFormatInEditMode = true)]
        public DateTime SlutDatum { get; set; }
        public TimeSpan SlutTid { get; set; }


    }
}
