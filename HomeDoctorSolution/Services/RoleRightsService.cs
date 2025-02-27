
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
            public class RoleRightsService : IRoleRightsService
            {
                IRoleRightsRepository roleRightsRepository;
                public RoleRightsService(
                    IRoleRightsRepository _roleRightsRepository
                    )
                {
                    roleRightsRepository = _roleRightsRepository;
                }
                public async Task Add(RoleRight obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await roleRightsRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = roleRightsRepository.Count();
                    return result;
                }
        
                public async Task Delete(RoleRight obj)
                {
                    obj.Active = 0;
                    await roleRightsRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await roleRightsRepository.DeletePermanently(id);
                }
        
                public async Task<RoleRight> Detail(int? id)
                {
                    return await roleRightsRepository.Detail(id);
                }
        
                public async Task<List<RoleRight>> List()
                {
                    return await roleRightsRepository.List();
                }
        
                public async Task<List<RoleRight>> ListPaging(int pageIndex, int pageSize)
                {
                    return await roleRightsRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<RoleRightsViewModel>> ListServerSide(RoleRightsDTParameters parameters)
                {
                    return await roleRightsRepository.ListServerSide(parameters);
                }
        
                public async Task<List<RoleRight>> Search(string keyword)
                {
                    return await roleRightsRepository.Search(keyword);
                }
        
                public async Task Update(RoleRight obj)
                {
                    await roleRightsRepository.Update(obj);
                }
            }
        }
    
    