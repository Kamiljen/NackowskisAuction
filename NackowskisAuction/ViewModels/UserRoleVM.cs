using NackowskisAuctionHouse.DAL.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.ViewModels
{
    public class UserRoleVM
    {
        public AppUser User { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string OldRole { get; set; }
        public string NewRole { get; set; }
    }
}
