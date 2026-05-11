using AYMDatingCore.BLL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace AYMDatingCore.Helpers
{
    [Authorize]
    public class ChatHub : Hub
    {
        // Store online users (Username -> ConnectionId)
        private static readonly ConcurrentDictionary<string, string> _onlineUsers = new();

        public override async Task OnConnectedAsync()
        {
            // Get username from the logged-in user
            var userName = Context.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                // Store connection ID for this user
                _onlineUsers[userName] = Context.ConnectionId;
                Console.WriteLine($"✅ User connected: {userName} - ConnectionId: {Context.ConnectionId}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                _onlineUsers.TryRemove(userName, out _);
                Console.WriteLine($"❌ User disconnected: {userName}");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string message, string SenderUserName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", message, SenderUserName);
        }

        public async Task SendAudioToGroup(string groupName, string senderUserName, string base64Audio)
        {
            await Clients.Group(groupName).SendAsync("ReceiveAudioFromGroup", senderUserName, base64Audio);
        }

        public async Task SendFileToGroup(string groupName, string senderUserName, string fileName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveFileFromGroup", senderUserName, fileName);
        }

        // CALL METHODS - Now using ConnectionId instead of User()
        public async Task CallUser(string caller, string receiver, bool isVideo)
        {
            await Clients.User(receiver).SendAsync("IncomingCall", caller, isVideo);
        }

        public async Task AcceptCall(string caller, string receiver)
        {
            if (_onlineUsers.TryGetValue(caller, out string callerConnectionId))
            {
                await Clients.Client(callerConnectionId).SendAsync("CallAccepted", receiver);
            }
        }

        public async Task RejectCall(string caller, string receiver)
        {
            if (_onlineUsers.TryGetValue(caller, out string callerConnectionId))
            {
                await Clients.Client(callerConnectionId).SendAsync("CallRejected", receiver);
            }
        }

        public async Task EndCall(string caller, string receiver)
        {
            if (_onlineUsers.TryGetValue(caller, out string callerConnectionId))
            {
                await Clients.Client(callerConnectionId).SendAsync("CallEnded", caller);
            }

            if (_onlineUsers.TryGetValue(receiver, out string receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("CallEnded", caller);
            }
        }

        public async Task SendOffer(string receiver, string offer, bool isVideo)
        {
            await Clients.User(receiver).SendAsync("ReceiveOffer", offer, isVideo);
        }

        public async Task SendAnswer(string receiver, string answer)
        {
            if (_onlineUsers.TryGetValue(receiver, out string receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveAnswer", answer);
            }
        }

        public async Task SendIceCandidate(string receiver, string candidate)
        {
            if (_onlineUsers.TryGetValue(receiver, out string receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveIceCandidate", candidate);
            }
        }
    }
}

