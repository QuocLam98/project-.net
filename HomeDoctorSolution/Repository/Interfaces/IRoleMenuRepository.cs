
using HomeDoctorSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;


namespace HomeDoctorSolution.Repository
{
    public interface IRoleMenuRepository
    {
        Task<List<RoleMenu>> List();

        Task<List<RoleMenu>> Search(string keyword);

        Task<List<RoleMenu>> ListPaging(int pageIndex, int pageSize);

        Task<RoleMenu> Detail(int? postId);

        Task<RoleMenu> Add(RoleMenu RoleMenu);

        Task<List<RoleMenu>> AddMany(List<RoleMenu> obj);

        Task Update(RoleMenu RoleMenu);

        Task Delete(RoleMenu RoleMenu);

        Task<int> DeletePermanently(int? RoleMenuId);

        int Count();

        Task<DTResult<RoleMenuViewModel>> ListServerSide(RoleMenuDTParameters parameters);
        Task<List<RoleMenu>> ListByRoleId(int? roleId);
    }
}
