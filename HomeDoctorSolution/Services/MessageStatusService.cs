
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Repository;
        using HomeDoctorSolution.Services.Interfaces;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System;
        using System.Collections.Generic;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services
        {
            public class MessageStatusService : IMessageStatusService
            {
                IMessageStatusRepository messageStatusRepository;
                public MessageStatusService(
                    IMessageStatusRepository _messageStatusRepository
                    )
                {
                    messageStatusRepository = _messageStatusRepository;
                }
                public async Task Add(MessageStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await messageStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = messageStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(MessageStatus obj)
                {
                    obj.Active = 0;
                    await messageStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await messageStatusRepository.DeletePermanently(id);
                }
        
                public async Task<MessageStatus> Detail(int? id)
                {
                    return await messageStatusRepository.Detail(id);
                }
        
                public async Task<List<MessageStatus>> List()
                {
                    return await messageStatusRepository.List();
                }
        
                public async Task<List<MessageStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await messageStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<MessageStatus>> ListServerSide(MessageStatusDTParameters parameters)
                {
                    return await messageStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<MessageStatus>> Search(string keyword)
                {
                    return await messageStatusRepository.Search(keyword);
                }
        
                public async Task Update(MessageStatus obj)
                {
                    await messageStatusRepository.Update(obj);
                }
            }
        }
    
    