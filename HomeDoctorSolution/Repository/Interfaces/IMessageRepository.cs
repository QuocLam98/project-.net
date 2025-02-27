using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace HomeDoctorSolution.Repository
{
    public interface IMessageRepository
    {
        Task<List<Message>> List();

        Task<List<Message>> Search(string keyword);

        Task<List<Message>> ListPaging(int pageIndex, int pageSize);

        Task<Message> Detail(int? postId);

        Task<Message> Add(Message Message);

        Task Update(Message Message);

        Task Delete(Message Message);

        Task<int> DeletePermanently(int? MessageId);

        int Count();

        Task<DTResult<MessageViewModel>> ListServerSide(MessageDTParameters parameters);
        DatabaseFacade GetDatabase();

        Task<Message> CheckExist(int? senderId, int? receiveId);

        Task<List<MessageViewModel>> ListContact(int accountId, int pageIndex, int pageSize);
        Task<List<MessageViewModel>> LoadUnread(int accountId, int pageIndex, int pageSize);
        Task<List<MessageViewModel>> ListMessage(int accountId, int pageIndex, int pageSize, string roomName);
        Task UpdateMany(List<Message> messages);
    }
}