
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface ISubscribeService : IBaseService<Subscribe>
            {
                Task<DTResult<Subscribe>> ListServerSide(SubscribeDTParameters parameters);
            }
        }
    