using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisAuctionHouse.Hubs
{
        public class NotificationHub : Hub
        {
            public async Task SendMessage(string user, string message)
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }

            public async Task AddToGroup(string groupName)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

                await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
            }

            public async Task RemoveFromGroup(string groupName)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

                await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
            }
    }
    }
}
