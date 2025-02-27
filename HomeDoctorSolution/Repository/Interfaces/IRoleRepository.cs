
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
    public interface IRoleRepository
    {
        Task<List<Role>> List();

        Task<List<Role>> Search(string keyword);

        Task<List<Role>> ListPaging(int pageIndex, int pageSize);

        Task<Role> Detail(int? postId);

        Task<Role> Add(Role Role);

        Task Update(Role Role);

        Task Delete(Role Role);

        Task<int> DeletePermanently(int? RoleId);

        int Count();

        Task<DTResult<Role>> ListServerSide(RoleDTParameters parameters);

        Task<int> GetIdByCode(string codeName);
    }
}
