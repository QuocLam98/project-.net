
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IDistrictService : IBaseService<District>
    {
        Task<DTResult<DistrictViewModel>> ListServerSide(DistrictDTParameters parameters);

        Task<List<District>> ListByProvinceId(int id);
    }
}
