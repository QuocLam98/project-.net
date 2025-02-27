using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ViewModels;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace HomeDoctorSolution.Util.Hubs
{
    public class AccountSendMessageHub : Hub
    {
        public static IHubCallerClients CacheClients { get; set; } = default!;
        public static HashSet<(int RoomId, string ConnectionId)> _connecteds = new();
        IMessageService service;
        public AccountSendMessageHub(IMessageService _service)
        {
            service = _service;
        }

        public static List<ConnectedUser> connectedUsers = new List<ConnectedUser>();

        public async Task SendAccountOnline()
        {
                await CacheClients.All.SendAsync("SendAccountOnline", connectedUsers);
        }
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}   
        //Execute when user connect to signalR
        public override async Task OnConnectedAsync()
        {    
            CacheClients = this.Clients;
            var roomId = Context.GetHttpContext().Request.Query["roomId"].ToString();
            var accountId = Context.GetHttpContext().Request.Query["accountId"].ToString();
            ((ClaimsIdentity)Context.User.Identity).AddClaim(new Claim("roomId", roomId));
            var connectionId = Context.ConnectionId;
            if((connectedUsers.FindAll(c=> c.accountId == accountId).Count == 0) && accountId != null && accountId != "")
            {
                var userOnline = new ConnectedUser();
                userOnline.accountId = accountId;
                userOnline.connectionId = connectionId;
                userOnline.status = "ONLINE";
                connectedUsers.Add(userOnline);
            }
            await Groups.AddToGroupAsync(connectionId, roomId);
            await Clients.All.SendAsync("SendAccountOnline", connectedUsers);
            await base.OnConnectedAsync();
        }

        //Execute when user disconnect from signalR
        public override async Task OnDisconnectedAsync(Exception e)
        {
            var connectionId = Context.ConnectionId;
            var connect = _connecteds.FirstOrDefault(c => c.ConnectionId == connectionId);
            var accountId = Context.GetHttpContext().Request.Query["accountId"].ToString();
            connectedUsers.RemoveAll(x => x.accountId == accountId);
            //Remove that conncetionId from dictionary containing all connections
            _connecteds.Remove(connect);
            await Clients.All.SendAsync("SendAccountOnline", connectedUsers);
            await base.OnDisconnectedAsync(e);
        }
    }

    public class ConnectedUser
    {
        public string connectionId { get; set; }
        public string accountId { get; set; }
        public string status { get; set; }

        public static ConnectedUser GetConnectedUserByAccountId(List<ConnectedUser> listUser, string accountId)
        {
            ConnectedUser result = new ConnectedUser();

            for (int i = 0; i < listUser.Count; i++)
            {
                if (listUser[i].accountId == accountId)
                {
                    result = listUser[i];
                    break;
                }
            }

            return result;
        }
    }
}
