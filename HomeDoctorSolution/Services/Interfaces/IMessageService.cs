
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IMessageService : IBaseService<Message>
    {
        Task<DTResult<MessageViewModel>> ListServerSide(MessageDTParameters parameters);
        Task SendMessage(Message obj);
        Task<List<MessageViewModel>> ListContact(int accountId, int pageIndex, int pageSize);
        Task<List<MessageViewModel>> LoadUnread(int accountId, int pageIndex, int pageSize);
        Task<List<MessageViewModel>> LoadMessage(int accountId, int pageIndex, int pageSize, string roomName);
        Task ReadedMessage(int accountId, string roomName);
    }
}
