
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface ITransactionMetaService : IBaseService<TransactionMeta>
            {
                Task<DTResult<TransactionMeta>> ListServerSide(TransactionMetaDTParameters parameters);
            }
        }
    