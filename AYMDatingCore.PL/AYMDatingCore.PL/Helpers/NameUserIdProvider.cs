using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace AYMDatingCore.PL.Helpers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}
