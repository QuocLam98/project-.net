
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services.Interfaces
        {
            public interface IProductTypeService : IBaseService<ProductType>
            {
                Task<DTResult<ProductType>> ListServerSide(ProductTypeDTParameters parameters);
                Task<bool> IsNameExist(int id, string name);
            }
        }
    