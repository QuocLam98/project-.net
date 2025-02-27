
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
            public class OrganizationStatusService : IOrganizationStatusService
            {
                IOrganizationStatusRepository organizationStatusRepository;
                public OrganizationStatusService(
                    IOrganizationStatusRepository _organizationStatusRepository
                    )
                {
                    organizationStatusRepository = _organizationStatusRepository;
                }
                public async Task Add(OrganizationStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await organizationStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = organizationStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(OrganizationStatus obj)
                {
                    obj.Active = 0;
                    await organizationStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await organizationStatusRepository.DeletePermanently(id);
                }
        
                public async Task<OrganizationStatus> Detail(int? id)
                {
                    return await organizationStatusRepository.Detail(id);
                }
        
                public async Task<List<OrganizationStatus>> List()
                {
                    return await organizationStatusRepository.List();
                }
        
                public async Task<List<OrganizationStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await organizationStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<OrganizationStatus>> ListServerSide(OrganizationStatusDTParameters parameters)
                {
                    return await organizationStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<OrganizationStatus>> Search(string keyword)
                {
                    return await organizationStatusRepository.Search(keyword);
                }
        
                public async Task Update(OrganizationStatus obj)
                {
                    await organizationStatusRepository.Update(obj);
                }
            }
        }
    
    