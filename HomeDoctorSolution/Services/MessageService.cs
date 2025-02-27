using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeDoctorSolution.Util.Hubs;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;

namespace HomeDoctorSolution.Services
{
    public class MessageService : IMessageService
    {
        IMessageRepository messageRepository;
        IRoomRepository roomRepository;
        IMapper mapper;
        private readonly IHubContext<AccountSendMessageHub> hubContext;
        private readonly IHubContext<ListConversationHub> listConversationContext;

        public MessageService(
            IMessageRepository _messageRepository,
            IRoomRepository _roomRepository,
            IHubContext<AccountSendMessageHub> _hubContext,
            IMapper _mapper
        )
        {
            messageRepository = _messageRepository;
            roomRepository = _roomRepository;
            hubContext = _hubContext;
            mapper = _mapper;
        }

        public async Task Add(Message obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await messageRepository.Add(obj);
        }

        public int Count()
        {
            var result = messageRepository.Count();
            return result;
        }

        public async Task Delete(Message obj)
        {
            obj.Active = 0;
            await messageRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await messageRepository.DeletePermanently(id);
        }

        public async Task<Message> Detail(int? id)
        {
            return await messageRepository.Detail(id);
        }

        public async Task<List<Message>> List()
        {
            return await messageRepository.List();
        }

        public async Task<List<Message>> ListPaging(int pageIndex, int pageSize)
        {
            return await messageRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<MessageViewModel>> ListServerSide(MessageDTParameters parameters)
        {
            return await messageRepository.ListServerSide(parameters);
        }

        public async Task<List<Message>> Search(string keyword)
        {
            return await messageRepository.Search(keyword);
        }

        public async Task Update(Message obj)
        {
            await messageRepository.Update(obj);
        }


        public async Task SendMessage(Message obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            obj.Name = obj.Text;
            //Tạo transaction thêm vào bảng room
            using (var dataBase = messageRepository.GetDatabase().BeginTransaction())
            {
                try
                {
                    //Kiểm tra tồn tại của 2 account đã có trong room chưa
                    var data = await messageRepository.CheckExist(obj.AccountId, obj.ReceiverId);
                    if (data != null)
                    {
                        //Thêm trực tiếp vào bảng message
                        obj.RoomId = data.RoomId;
                    }
                    else
                    {
                        //Thêm vào bảng room
                        var newRoom = new Room()
                        {
                            Id = 0,
                            Active = 1,
                            Name = obj.AccountId + "-" + obj.ReceiverId,
                            CreatedTime = DateTime.Now,
                        };
                        var room = await roomRepository.Add(newRoom);
                        //thêm vào bảng message
                        obj.RoomId = room.Id;
                    }

                    await messageRepository.Add(obj);
                    await hubContext.Clients.Groups(obj.ReceiverId.ToString()).SendAsync("ReceiveMessage", obj);
                    await dataBase.CommitAsync();
                }
                catch (Exception e)
                {
                    await dataBase.RollbackAsync();
                }
            }
        }

        public async Task<List<MessageViewModel>> ListContact(int accountId, int pageIndex, int pageSize)
        {
            return await messageRepository.ListContact(accountId, pageIndex, pageSize);
        }

        public async Task<List<MessageViewModel>> LoadUnread(int accountId, int pageIndex, int pageSize)
        {
            return await messageRepository.LoadUnread(accountId, pageIndex, pageSize);
        }

        public async Task<List<MessageViewModel>> LoadMessage(int accountId, int pageIndex, int pageSize, string roomName)
        {
            //var data = await messageRepository.ListMessage(accountId, pageIndex, -1);
            //data = data.Where(c => c.AccountId == accountId).ToList();
            //var mapperData = mapper.Map<List<Message>>(data);
            //await messageRepository.UpdateMany(mapperData);
            return await messageRepository.ListMessage(accountId, pageIndex, pageSize, roomName);
        }

        public async Task ReadedMessage(int accountId, string roomName)
        {
            var data = await messageRepository.ListMessage(accountId, 1, -1, roomName);
            var mapperData = mapper.Map<List<Message>>(data);
            await messageRepository.UpdateMany(mapperData);
        }
    }
}