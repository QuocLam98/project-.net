
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
            public class OrganizationService : IOrganizationService
            {
                IOrganizationRepository organizationRepository;
                public OrganizationService(
                    IOrganizationRepository _organizationRepository
                    )
                {
                    organizationRepository = _organizationRepository;
                }
                public async Task Add(Organization obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await organizationRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = organizationRepository.Count();
                    return result;
                }
        
                public async Task Delete(Organization obj)
                {
                    obj.Active = 0;
                    await organizationRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await organizationRepository.DeletePermanently(id);
                }
        
                public async Task<Organization> Detail(int? id)
                {
                    return await organizationRepository.Detail(id);
                }
        
                public async Task<List<Organization>> List()
                {
                    return await organizationRepository.List();
                }
        
                public async Task<List<Organization>> ListPaging(int pageIndex, int pageSize)
                {
                    return await organizationRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<OrganizationViewModel>> ListServerSide(OrganizationDTParameters parameters)
                {
                    return await organizationRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Organization>> Search(string keyword)
                {
                    return await organizationRepository.Search(keyword);
                }
        
                public async Task Update(Organization obj)
                {
                    await organizationRepository.Update(obj);
                }
            }
        }
    
    