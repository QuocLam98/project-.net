
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
            public class EmailService : IEmailService
            {
                IEmailRepository emailRepository;
                public EmailService(
                    IEmailRepository _emailRepository
                    )
                {
                    emailRepository = _emailRepository;
                }
                public async Task Add(Email obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await emailRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = emailRepository.Count();
                    return result;
                }
        
                public async Task Delete(Email obj)
                {
                    obj.Active = 0;
                    await emailRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await emailRepository.DeletePermanently(id);
                }
        
                public async Task<Email> Detail(int? id)
                {
                    return await emailRepository.Detail(id);
                }
        
                public async Task<List<Email>> List()
                {
                    return await emailRepository.List();
                }
        
                public async Task<List<Email>> ListPaging(int pageIndex, int pageSize)
                {
                    return await emailRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<Email>> ListServerSide(EmailDTParameters parameters)
                {
                    return await emailRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Email>> Search(string keyword)
                {
                    return await emailRepository.Search(keyword);
                }
        
                public async Task Update(Email obj)
                {
                    await emailRepository.Update(obj);
                }
            }
        }
    
    