//using AYMDatingCore.BLL.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;

//namespace AYMDatingCore.Helpers
//{
//    [Authorize]
//    public class ChatManagement
//    {
//        private readonly IHubContext<ChatHub> _hubContext;
//        private readonly IUnitOfWork unitOfWork ;

//        public ChatManagement(IHubContext<ChatHub> hubContext, IUnitOfWork unitOfWork)
//        {
//            _hubContext = hubContext;
//            this.unitOfWork = unitOfWork;
//        }

//        public async Task AddToGroup(string connectionId, string groupName)
//        {
//            await _hubContext.Groups.AddToGroupAsync(connectionId, groupName);
//        }

//        public async Task SendMessage(string groupName, string user, string user2, DateTime dateOfMaking, string message)
//        {
//            unitOfWork.UserMessageRepository.Add(new DAL.Entities.UserMessageTBL() { SenderAppUserId = user, ReceiverAppUserId = user2, Message = message });
//            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", user, user2, dateOfMaking, message);
//        }


//    }
//}
