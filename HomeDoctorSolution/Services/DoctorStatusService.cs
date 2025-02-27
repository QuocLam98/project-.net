
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
            public class DoctorStatusService : IDoctorStatusService
            {
                IDoctorStatusRepository doctorStatusRepository;
                public DoctorStatusService(
                    IDoctorStatusRepository _doctorStatusRepository
                    )
                {
                    doctorStatusRepository = _doctorStatusRepository;
                }
                public async Task Add(DoctorStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await doctorStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = doctorStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(DoctorStatus obj)
                {
                    obj.Active = 0;
                    await doctorStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await doctorStatusRepository.DeletePermanently(id);
                }
        
                public async Task<DoctorStatus> Detail(int? id)
                {
                    return await doctorStatusRepository.Detail(id);
                }
        
                public async Task<List<DoctorStatus>> List()
                {
                    return await doctorStatusRepository.List();
                }
        
                public async Task<List<DoctorStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await doctorStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<DoctorStatus>> ListServerSide(DoctorStatusDTParameters parameters)
                {
                    return await doctorStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<DoctorStatus>> Search(string keyword)
                {
                    return await doctorStatusRepository.Search(keyword);
                }
        
                public async Task Update(DoctorStatus obj)
                {
                    await doctorStatusRepository.Update(obj);
                }
            }
        }
    
    