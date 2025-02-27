
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface IWardService : IBaseService<Ward>
            {
                Task<DTResult<WardViewModel>> ListServerSide(WardDTParameters parameters);

                Task<List<Ward>> ListByDistrictId(int id);
            }
        }
    