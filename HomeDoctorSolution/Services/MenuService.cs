
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
            public class MenuService : IMenuService
            {
                IMenuRepository menuRepository;
                public MenuService(
                    IMenuRepository _menuRepository
                    )
                {
                    menuRepository = _menuRepository;
                }
                public async Task Add(Menu obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await menuRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = menuRepository.Count();
                    return result;
                }
        
                public async Task Delete(Menu obj)
                {
                    obj.Active = 0;
                    await menuRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await menuRepository.DeletePermanently(id);
                }
        
                public async Task<Menu> Detail(int? id)
                {
                    return await menuRepository.Detail(id);
                }
        
                public async Task<List<Menu>> List()
                {
                    return await menuRepository.List();
                }
        
                public async Task<List<Menu>> ListPaging(int pageIndex, int pageSize)
                {
                    return await menuRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<MenuViewModel>> ListServerSide(MenuDTParameters parameters)
                {
                    return await menuRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Menu>> Search(string keyword)
                {
                    return await menuRepository.Search(keyword);
                }
        
                public async Task Update(Menu obj)
                {
                    await menuRepository.Update(obj);
                }
            }
        }
    
    