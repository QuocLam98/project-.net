using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace HomeDoctorSolution.Util.Hubs
{
    public class NotificationHub : Hub
    {
        public static IHubCallerClients CacheClients { get; set; } = default!;
        public static HashSet<(long UserId, string ConnectionId)> _connecteds = new();
        
        //Execute when user connect to signalR
        public override async Task OnConnectedAsync()
        {
            CacheClients = this.Clients;
            var connectionId = Context.ConnectionId;
            long userId = this.GetLoggedInUserId();
            _connecteds.Add((userId, connectionId));
            await base.OnConnectedAsync();
        }
        //Execute when user disconnect from signalR
        public override async Task OnDisconnectedAsync(Exception e)
        {
            var connectionId = Context.ConnectionId;
            var connect = _connecteds.FirstOrDefault(c => c.ConnectionId == connectionId);
            //Remove that conncetionId from dictionary containing all connections
            _connecteds.Remove(connect);
            await base.OnDisconnectedAsync(e);
        }
    }
}
