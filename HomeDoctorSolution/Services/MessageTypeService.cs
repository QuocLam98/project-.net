
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
            public class MessageTypeService : IMessageTypeService
            {
                IMessageTypeRepository messageTypeRepository;
                public MessageTypeService(
                    IMessageTypeRepository _messageTypeRepository
                    )
                {
                    messageTypeRepository = _messageTypeRepository;
                }
                public async Task Add(MessageType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await messageTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = messageTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(MessageType obj)
                {
                    obj.Active = 0;
                    await messageTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await messageTypeRepository.DeletePermanently(id);
                }
        
                public async Task<MessageType> Detail(int? id)
                {
                    return await messageTypeRepository.Detail(id);
                }
        
                public async Task<List<MessageType>> List()
                {
                    return await messageTypeRepository.List();
                }
        
                public async Task<List<MessageType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await messageTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<MessageType>> ListServerSide(MessageTypeDTParameters parameters)
                {
                    return await messageTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<MessageType>> Search(string keyword)
                {
                    return await messageTypeRepository.Search(keyword);
                }
        
                public async Task Update(MessageType obj)
                {
                    await messageTypeRepository.Update(obj);
                }
            }
        }
    
    