using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Hieromemics.Hubs {

    public class ChatHub : Hub {
        
        public override async Task OnConnectedAsync() {
            await Groups.AddToGroupAsync(Context.ConnectionId, "UserFriendChat");
            await Clients.Group("UserFriendChat").SendAsync("Send", $"{Context.ConnectionId} has joined the group.");
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message) {
            await Clients.Group("UserFriendChat").SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnDisconnectedAsync(Exception exception) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "UserFriendChat");
            await Clients.Group("UserFriendChat").SendAsync("Send", $"{Context.ConnectionId} has left the chat.");
            await base.OnDisconnectedAsync(exception);
        }


    }
}

