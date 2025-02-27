
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
    public interface IRoleRightsRepository
    {
        Task<List<RoleRight>> List();

        Task<List<RoleRight>> Search(string keyword);

        Task<List<RoleRight>> ListPaging(int pageIndex, int pageSize);

        Task<RoleRight> Detail(int? postId);

        Task<RoleRight> Add(RoleRight RoleRight);

        Task Update(RoleRight RoleRight);

        Task Delete(RoleRight RoleRight);

        Task<int> DeletePermanently(int? RoleRightsId);

        int Count();

        Task<DTResult<RoleRightsViewModel>> ListServerSide(RoleRightsDTParameters parameters);

        Task<List<RoleRight>> ListByRoleId(int? id);
    }
}
