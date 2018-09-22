using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NackowskisAuctionHouse.DAL.IdentityModels;

namespace NackowskisAuctionHouse.ViewModels
{
    public class UserVM
    {
        public List<AppUser> Users { get; set; }
        public string Role { get; set; }    
    }
}
