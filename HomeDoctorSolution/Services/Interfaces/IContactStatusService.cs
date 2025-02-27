
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface IContactStatusService : IBaseService<ContactStatus>
            {
                Task<DTResult<ContactStatus>> ListServerSide(ContactStatusDTParameters parameters);
            }
        }
    