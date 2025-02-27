
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
            public class MenuTypeService : IMenuTypeService
            {
                IMenuTypeRepository menuTypeRepository;
                public MenuTypeService(
                    IMenuTypeRepository _menuTypeRepository
                    )
                {
                    menuTypeRepository = _menuTypeRepository;
                }
                public async Task Add(MenuType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await menuTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = menuTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(MenuType obj)
                {
                    obj.Active = 0;
                    await menuTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await menuTypeRepository.DeletePermanently(id);
                }
        
                public async Task<MenuType> Detail(int? id)
                {
                    return await menuTypeRepository.Detail(id);
                }
        
                public async Task<List<MenuType>> List()
                {
                    return await menuTypeRepository.List();
                }
        
                public async Task<List<MenuType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await menuTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<MenuType>> ListServerSide(MenuTypeDTParameters parameters)
                {
                    return await menuTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<MenuType>> Search(string keyword)
                {
                    return await menuTypeRepository.Search(keyword);
                }
        
                public async Task Update(MenuType obj)
                {
                    await menuTypeRepository.Update(obj);
                }
            }
        }
    
    