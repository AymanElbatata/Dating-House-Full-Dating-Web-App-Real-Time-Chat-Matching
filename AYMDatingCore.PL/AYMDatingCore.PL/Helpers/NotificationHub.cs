using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.BLL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace AYMDatingCore.Helpers
{
    public class NotificationHub : Hub
    {
        private readonly IUnitOfWork unitOfWork;

        public NotificationHub(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task GetLikeNotification(string userRecieverId)
        {
            await Clients.User(userRecieverId).SendAsync("ReceiveLikeNotification");
        }
        public async Task GetViewNotification(string userRecieverId)
        {
            await Clients.User(userRecieverId).SendAsync("ReceiveViewNotification");
        }
        public async Task GetMessageNotification(string userRecieverId)
        {
            await Clients.User(userRecieverId).SendAsync("ReceiveMessageNotification");
        }
        public async Task GetFavoriteNotification(string userRecieverId)
        {
            await Clients.User(userRecieverId).SendAsync("ReceiveFavoriteNotification");
        }
        public async Task GetBlockNotification(string userRecieverId)
        {
            await Clients.User(userRecieverId).SendAsync("ReceiveBlockNotification");
        }
        public override async Task OnConnectedAsync()
        {
            var user = await unitOfWork.UserManager.GetUserAsync(Context.User);
            if (user != null)
            {
                user.IsOnline = true;
                user.LastSeen = DateTime.Now;
                await unitOfWork.UserManager.UpdateAsync(user);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(Context.User);
            if (user != null)
            {
                user.IsOnline = false;
                user.LastSeen = DateTime.Now;
                await unitOfWork.UserManager.UpdateAsync(user);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }


}
