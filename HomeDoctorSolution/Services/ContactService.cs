
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
            public class ContactService : IContactService
            {
                IContactRepository contactRepository;
                public ContactService(
                    IContactRepository _contactRepository
                    )
                {
                    contactRepository = _contactRepository;
                }
                public async Task Add(Contact obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await contactRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = contactRepository.Count();
                    return result;
                }
        
                public async Task Delete(Contact obj)
                {
                    obj.Active = 0;
                    await contactRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await contactRepository.DeletePermanently(id);
                }
        
                public async Task<Contact> Detail(int? id)
                {
                    return await contactRepository.Detail(id);
                }
        
                public async Task<List<Contact>> List()
                {
                    return await contactRepository.List();
                }
        
                public async Task<List<Contact>> ListPaging(int pageIndex, int pageSize)
                {
                    return await contactRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<ContactViewModel>> ListServerSide(ContactDTParameters parameters)
                {
                    return await contactRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Contact>> Search(string keyword)
                {
                    return await contactRepository.Search(keyword);
                }
        
                public async Task Update(Contact obj)
                {
                    await contactRepository.Update(obj);
                }
            }
        }
    
    