
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security;
using HomeDoctorSolution.Models.ModelDTO;
using AutoMapper;

namespace HomeDoctorSolution.Services
{
    public class RoleMenuService : IRoleMenuService
    {
        IRoleMenuRepository roleMenuRepository;
        private readonly IMapper _mapper;

        public RoleMenuService(
            IRoleMenuRepository _roleMenuRepository, IMapper mapper
            )
        {
            roleMenuRepository = _roleMenuRepository;
            _mapper = mapper;
        }
        public async Task Add(RoleMenu obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await roleMenuRepository.Add(obj);
        }
        public async Task AddMany(RoleMenuDTO obj)
        {
            var models = MappingInsertDTO(obj);
            await roleMenuRepository.AddMany(models);
        }
        public int Count()
        {
            var result = roleMenuRepository.Count();
            return result;
        }

        public async Task Delete(RoleMenu obj)
        {
            obj.Active = 0;
            await roleMenuRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await roleMenuRepository.DeletePermanently(id);
        }

        public async Task<RoleMenu> Detail(int? id)
        {
            return await roleMenuRepository.Detail(id);
        }

        public async Task<List<RoleMenu>> List()
        {
            return await roleMenuRepository.List();
        }

        public async Task<List<RoleMenu>> ListPaging(int pageIndex, int pageSize)
        {
            return await roleMenuRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<RoleMenuViewModel>> ListServerSide(RoleMenuDTParameters parameters)
        {
            return await roleMenuRepository.ListServerSide(parameters);
        }

        public async Task<List<RoleMenu>> Search(string keyword)
        {
            return await roleMenuRepository.Search(keyword);
        }

        public async Task Update(RoleMenu obj)
        {
            await roleMenuRepository.Update(obj);
        }

        public List<RoleMenu> MappingInsertDTO(RoleMenuDTO obj)
        {
            var listRoleMenuAdd = new List<RoleMenu>();
            obj.Menus.ForEach(m =>
            {
                RoleMenu RoleMenu = new RoleMenu();
                RoleMenu.Id = 0;
                RoleMenu.Active = 1;
                RoleMenu.MenuId = m;
                RoleMenu.Name = obj.Name;
                RoleMenu.Description = obj.Description;
                RoleMenu.RoleId = obj.RoleId;
                RoleMenu.CreatedTime = DateTime.Now;
                listRoleMenuAdd.Add(RoleMenu);
            });
            return listRoleMenuAdd;
        }
    }
}

