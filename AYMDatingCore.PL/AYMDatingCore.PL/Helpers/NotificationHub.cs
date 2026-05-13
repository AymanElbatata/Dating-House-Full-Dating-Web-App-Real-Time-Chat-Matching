using AYMDatingCore.BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

public class NotificationHub : Hub
{
    private readonly IUnitOfWork unitOfWork;

    // UserId -> ConnectionId
    private static readonly ConcurrentDictionary<string, string> onlineUsers = new();

    public NotificationHub(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    // ==============================
    // CONNECTION
    // ==============================

    public override async Task OnConnectedAsync()
    {
        var user = await unitOfWork.UserManager.GetUserAsync(Context.User);

        if (user != null)
        {
            onlineUsers[user.Id] = Context.ConnectionId;

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
            onlineUsers.TryRemove(user.Id, out _);

            user.IsOnline = false;
            user.LastSeen = DateTime.Now;

            await unitOfWork.UserManager.UpdateAsync(user);
        }

        await base.OnDisconnectedAsync(exception);
    }

    // ==============================
    // LIKE NOTIFICATION
    // ==============================

    public async Task GetLikeNotification(string userRecieverId)
    {
        if (onlineUsers.TryGetValue(userRecieverId, out var connectionId))
        {
            var count = unitOfWork.UserLikeRepository
                .GetAllCustomized(filter =>
                    filter.ReceiverAppUserId == userRecieverId &&
                    !filter.IsSeen &&
                    !filter.IsDeleted)
                .Count();

            await Clients.Client(connectionId)
                .SendAsync("ReceiveLikeNotification", count);
        }
    }

    // ==============================
    // VIEW NOTIFICATION
    // ==============================

    public async Task GetViewNotification(string userRecieverId)
    {
        if (onlineUsers.TryGetValue(userRecieverId, out var connectionId))
        {
            var count = unitOfWork.UserViewRepository
                .GetAllCustomized(filter =>
                    filter.ReceiverAppUserId == userRecieverId &&
                    !filter.IsSeen &&
                    !filter.IsDeleted)
                .Count();

            await Clients.Client(connectionId)
                .SendAsync("ReceiveViewNotification", count);
        }
    }

    // ==============================
    // MESSAGE NOTIFICATION
    // ==============================

    public async Task GetMessageNotification(string userRecieverId)
    {
        if (onlineUsers.TryGetValue(userRecieverId, out var connectionId))
        {
            var count = unitOfWork.UserMessageRepository
                .GetAllCustomized(filter =>
                    filter.ReceiverAppUserId == userRecieverId &&
                    !filter.IsSeen &&
                    !filter.IsDeleted)
                .Count();

            await Clients.Client(connectionId)
                .SendAsync("ReceiveMessageNotification", count);
        }
    }

    // ==============================
    // FAVORITE NOTIFICATION
    // ==============================

    public async Task GetFavoriteNotification(string userRecieverId)
    {
        if (onlineUsers.TryGetValue(userRecieverId, out var connectionId))
        {
            var count = unitOfWork.UserFavoriteRepository
                .GetAllCustomized(filter =>
                    filter.ReceiverAppUserId == userRecieverId &&
                    !filter.IsSeen &&
                    !filter.IsDeleted)
                .Count();

            await Clients.Client(connectionId)
                .SendAsync("ReceiveFavoriteNotification", count);
        }
    }

    // ==============================
    // BLOCK NOTIFICATION
    // ==============================

    public async Task GetBlockNotification(string userRecieverId)
    {
        if (onlineUsers.TryGetValue(userRecieverId, out var connectionId))
        {
            var count = unitOfWork.UserBlockRepository
                .GetAllCustomized(filter =>
                    filter.ReceiverAppUserId == userRecieverId &&
                    !filter.IsSeen &&
                    !filter.IsDeleted)
                .Count();

            await Clients.Client(connectionId)
                .SendAsync("ReceiveBlockNotification", count);
        }
    }

    // ==============================
    // ONLINE USERS
    // ==============================

    public Task<List<string>> GetOnlineUsers()
    {
        return Task.FromResult(onlineUsers.Keys.ToList());
    }
}