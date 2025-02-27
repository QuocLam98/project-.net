using Microsoft.AspNetCore.SignalR;

namespace HomeDoctorSolution.Util.Hubs
{
    public class ListConversationHub : Hub
    {
        public async Task ListConversation(int accountId)
        {
            await Clients.All.SendAsync("ListConversation",accountId);
        }
    }
}
