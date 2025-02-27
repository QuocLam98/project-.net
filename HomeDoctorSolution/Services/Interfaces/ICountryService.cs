
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface ICountryService : IBaseService<Country>
            {
                Task<DTResult<Country>> ListServerSide(CountryDTParameters parameters);
            }
        }
    