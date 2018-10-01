using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class EditAuctionVM
    {
        public int AuctionId { get; set; }
        [Required(ErrorMessage = "Titel kan inte vara tom")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Beskrivning kan inte vara tom")]
        public string Beskrivning { get; set; }



        public string StartDatumString { get; set; }



        public string SlutDatumString { get; set; }

        [Required]
        public int Gruppkod { get; set; }

        [Required(ErrorMessage = "Utropspris kan inte vara tom")]
        public int Utropspris { get; set; }


        [Display(Name = "Skapad av")]
        public string SkapadAv { get; set; }

        [Required(ErrorMessage = "Startdatum kan inte vara tom")]
        public DateTime StartDatum { get; set; }
        public TimeSpan StartTid { get; set; }

        [Required(ErrorMessage = "Slutdatum kan inte vara tom")]
        public DateTime SlutDatum { get; set; }
        public TimeSpan SlutTid { get; set; }
    }
}
