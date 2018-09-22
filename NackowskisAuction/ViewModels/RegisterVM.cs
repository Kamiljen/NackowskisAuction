using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Användarnamn krävs")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Fälter kräver en mailadress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord krävs")]
        [DataType(DataType.Password)]
        [Display(Name="Lösenord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bekräfta lösenord krävs")]
        [Compare("Password", ErrorMessage = "Lösenorden stämmer inte överäns")]
        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        public string PasswordConfirmed { get; set; }

        
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

    }
}
