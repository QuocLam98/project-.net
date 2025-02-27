
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
            public class OrganizationTypeService : IOrganizationTypeService
            {
                IOrganizationTypeRepository organizationTypeRepository;
                public OrganizationTypeService(
                    IOrganizationTypeRepository _organizationTypeRepository
                    )
                {
                    organizationTypeRepository = _organizationTypeRepository;
                }
                public async Task Add(OrganizationType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await organizationTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = organizationTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(OrganizationType obj)
                {
                    obj.Active = 0;
                    await organizationTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await organizationTypeRepository.DeletePermanently(id);
                }
        
                public async Task<OrganizationType> Detail(int? id)
                {
                    return await organizationTypeRepository.Detail(id);
                }
        
                public async Task<List<OrganizationType>> List()
                {
                    return await organizationTypeRepository.List();
                }
        
                public async Task<List<OrganizationType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await organizationTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<OrganizationType>> ListServerSide(OrganizationTypeDTParameters parameters)
                {
                    return await organizationTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<OrganizationType>> Search(string keyword)
                {
                    return await organizationTypeRepository.Search(keyword);
                }
        
                public async Task Update(OrganizationType obj)
                {
                    await organizationTypeRepository.Update(obj);
                }
            }
        }
    
    