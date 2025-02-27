
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IHealthFacilityService : IBaseService<HealthFacility>
    {
        Task<DTResult<HealthFacilityViewModel>> ListServerSide(HealthFacilityDTParameters parameters);
        Task<List<HealthFacility>> ListPagingView(int pageIndex, int pageSize, int provinceId, string keyword);
    }
}
