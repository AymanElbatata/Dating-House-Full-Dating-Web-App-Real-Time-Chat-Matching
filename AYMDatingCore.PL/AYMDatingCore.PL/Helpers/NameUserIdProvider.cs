using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace AYMDatingCore.PL.Helpers
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
