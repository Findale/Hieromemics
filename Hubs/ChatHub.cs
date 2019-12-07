using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Hieromemics.Hubs {

    public class ChatHub : Hub {
        
        
        public async Task SendMessage(string user, string message) {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToCaller(string user, string message) {
            await Clients.Caller.SendAsync("ReceiveMessage",user, message);
        }
        
        public async Task SendMessageToFriend(string connectionId, string message) {
            await Clients.Client(connectionId).SendAsync("ReceiveFrMessage", connectionId, message);
        }
        public override async Task OnConnectedAsync() {
            await Clients.All.SendAsync("FriendConnected", Context.ConnectionId);
            //await Groups.AddToGroupAsync(Context.ConnectionId, "UserFriendChat");
            //await Clients.Group("UserFriendChat").SendAsync("Send", $"{Context.ConnectionId} has joined the group.");
            await base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception exception) {
            await Clients.All.SendAsync("FriendDisconnected", Context.ConnectionId);
            //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "UserFriendChat");
            //await Clients.Group("UserFriendChat").SendAsync("Send", $"{Context.ConnectionId} has left the chat.");
            await base.OnDisconnectedAsync(exception);
        }


    }
}

