
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;
using HomeDoctorSolution.Models.ModelDTO;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IRoleMenuService : IBaseService<RoleMenu>
    {
        Task<DTResult<RoleMenuViewModel>> ListServerSide(RoleMenuDTParameters parameters);

        Task AddMany(RoleMenuDTO obj);
    }
}
