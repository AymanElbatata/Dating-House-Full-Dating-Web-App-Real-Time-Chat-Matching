using AYMDatingCore.BLL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AYMDatingCore.Helpers
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendMessageToGroup(string groupName, string message, string SenderUserName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", message , SenderUserName );
        }
        public async Task SendAudioToGroup(string groupName, string senderUserName, string base64Audio)
        {
            // Send to all clients in the group
            await Clients.Group(groupName).SendAsync("ReceiveAudioFromGroup", senderUserName, base64Audio);
        }
        public async Task SendFileToGroup(string groupName, string senderUserName, string fileName)
        {
            // Send to all clients in the group
            await Clients.Group(groupName).SendAsync("ReceiveFileFromGroup", senderUserName, fileName);
        }
        public async Task CallUser(string caller, string receiver)
        {
            await Clients.User(receiver).SendAsync("IncomingCall", caller);
        }

        public async Task AcceptCall(string caller, string receiver)
        {
            await Clients.User(caller).SendAsync("CallAccepted", receiver);
        }

        public async Task RejectCall(string caller, string receiver)
        {
            await Clients.User(caller).SendAsync("CallRejected", receiver);
        }
        public async Task EndCall(string caller, string receiver)
        {
            // Notify both sides that the call has ended
            await Clients.Users(caller, receiver).SendAsync("CallEnded", caller);
        }
        public async Task SendOffer(string receiver, string offer)
        {
            await Clients.User(receiver).SendAsync("ReceiveOffer", offer);
        }

        public async Task SendAnswer(string receiver, string answer)
        {
            await Clients.User(receiver).SendAsync("ReceiveAnswer", answer);
        }

        public async Task SendIceCandidate(string receiver, string candidate)
        {
            await Clients.User(receiver).SendAsync("ReceiveIceCandidate", candidate);
        }
        
    }
}
