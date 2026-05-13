using AYMDatingCore.BLL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace AYMDatingCore.Helpers
{
    [Authorize]
    public class ChatHub : Hub
    {
        // Username -> ConnectionId
        private static readonly ConcurrentDictionary<string, string> _onlineUsers = new();

        // ==============================
        // CONNECTION
        // ==============================

        public override async Task OnConnectedAsync()
        {
            var userName = Context.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                _onlineUsers[userName] = Context.ConnectionId;
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userName = Context.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                _onlineUsers.TryRemove(userName, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }

        // ==============================
        // GROUP CHAT
        // ==============================

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(
            string groupName,
            string message,
            string senderUserName)
        {
            await Clients.Group(groupName)
                .SendAsync(
                    "ReceiveGroupMessage",
                    message,
                    senderUserName);
        }

        public async Task SendAudioToGroup(
            string groupName,
            string senderUserName,
            string base64Audio)
        {
            await Clients.Group(groupName)
                .SendAsync(
                    "ReceiveAudioFromGroup",
                    senderUserName,
                    base64Audio);
        }

        public async Task SendFileToGroup(
            string groupName,
            string senderUserName,
            string fileName)
        {
            await Clients.Group(groupName)
                .SendAsync(
                    "ReceiveFileFromGroup",
                    senderUserName,
                    fileName);
        }

        // ==============================
        // CALLS
        // ==============================

        public async Task CallUser(
            string caller,
            string receiver,
            bool isVideo)
        {
            if (_onlineUsers.TryGetValue(receiver, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId)
                    .SendAsync(
                        "IncomingCall",
                        caller,
                        isVideo);
            }
        }

        public async Task AcceptCall(
            string caller,
            string receiver)
        {
            if (_onlineUsers.TryGetValue(caller, out var callerConnectionId))
            {
                await Clients.Client(callerConnectionId)
                    .SendAsync(
                        "CallAccepted",
                        receiver);
            }
        }

        public async Task RejectCall(
            string caller,
            string receiver)
        {
            if (_onlineUsers.TryGetValue(caller, out var callerConnectionId))
            {
                await Clients.Client(callerConnectionId)
                    .SendAsync(
                        "CallRejected",
                        receiver);
            }
        }

        public async Task EndCall(
            string caller,
            string receiver)
        {
            if (_onlineUsers.TryGetValue(caller, out var callerConnectionId))
            {
                await Clients.Client(callerConnectionId)
                    .SendAsync(
                        "CallEnded",
                        caller);
            }

            if (_onlineUsers.TryGetValue(receiver, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId)
                    .SendAsync(
                        "CallEnded",
                        caller);
            }
        }

        // ==============================
        // WEBRTC
        // ==============================

        public async Task SendOffer(
            string receiver,
            string offer,
            bool isVideo)
        {
            if (_onlineUsers.TryGetValue(receiver, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId)
                    .SendAsync(
                        "ReceiveOffer",
                        offer,
                        isVideo);
            }
        }

        public async Task SendAnswer(
            string receiver,
            string answer)
        {
            if (_onlineUsers.TryGetValue(receiver, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId)
                    .SendAsync(
                        "ReceiveAnswer",
                        answer);
            }
        }

        public async Task SendIceCandidate(
            string receiver,
            string candidate)
        {
            if (_onlineUsers.TryGetValue(receiver, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId)
                    .SendAsync(
                        "ReceiveIceCandidate",
                        candidate);
            }
        }

        // ==============================
        // ONLINE USERS
        // ==============================

        public Task<List<string>> GetOnlineUsers()
        {
            return Task.FromResult(_onlineUsers.Keys.ToList());
        }
    }
}

