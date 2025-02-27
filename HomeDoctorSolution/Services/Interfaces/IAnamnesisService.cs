
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface IAnamnesisService : IBaseService<Anamnesis>
    {
        Task<DTResult<AnamnesisViewModel>> ListServerSide(AnamnesisDTParameters parameters);
        Task<HomeDoctorResponse> AddOrUpdateAnamnessis(Anamnesis model);
    }
}
