
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
            public class ContactStatusService : IContactStatusService
            {
                IContactStatusRepository contactStatusRepository;
                public ContactStatusService(
                    IContactStatusRepository _contactStatusRepository
                    )
                {
                    contactStatusRepository = _contactStatusRepository;
                }
                public async Task Add(ContactStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await contactStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = contactStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(ContactStatus obj)
                {
                    obj.Active = 0;
                    await contactStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await contactStatusRepository.DeletePermanently(id);
                }
        
                public async Task<ContactStatus> Detail(int? id)
                {
                    return await contactStatusRepository.Detail(id);
                }
        
                public async Task<List<ContactStatus>> List()
                {
                    return await contactStatusRepository.List();
                }
        
                public async Task<List<ContactStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await contactStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<ContactStatus>> ListServerSide(ContactStatusDTParameters parameters)
                {
                    return await contactStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<ContactStatus>> Search(string keyword)
                {
                    return await contactStatusRepository.Search(keyword);
                }
        
                public async Task Update(ContactStatus obj)
                {
                    await contactStatusRepository.Update(obj);
                }
            }
        }
    
    