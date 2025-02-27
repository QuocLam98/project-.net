
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
            public class RoomService : IRoomService
            {
                IRoomRepository roomRepository;
                public RoomService(
                    IRoomRepository _roomRepository
                    )
                {
                    roomRepository = _roomRepository;
                }
                public async Task Add(Room obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await roomRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = roomRepository.Count();
                    return result;
                }
        
                public async Task Delete(Room obj)
                {
                    obj.Active = 0;
                    await roomRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await roomRepository.DeletePermanently(id);
                }
        
                public async Task<Room> Detail(int? id)
                {
                    return await roomRepository.Detail(id);
                }
        
                public async Task<List<Room>> List()
                {
                    return await roomRepository.List();
                }
        
                public async Task<List<Room>> ListPaging(int pageIndex, int pageSize)
                {
                    return await roomRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<Room>> ListServerSide(RoomDTParameters parameters)
                {
                    return await roomRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Room>> Search(string keyword)
                {
                    return await roomRepository.Search(keyword);
                }
        
                public async Task Update(Room obj)
                {
                    await roomRepository.Update(obj);
                }
            }
        }
    
    