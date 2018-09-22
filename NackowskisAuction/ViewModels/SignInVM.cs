using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class SignInVM
    {
        [Required(ErrorMessage = "Användarnamn krävs")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Fälter kräver en mailadress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord krävs")]
        [DataType(DataType.Password)]
        [Display (Name = "Lösenord")]
        public string Password { get; set; }

        
        [Display(Name ="Kom ihåg mig")]
        public bool RememberMe { get; set; }
    }
}
    