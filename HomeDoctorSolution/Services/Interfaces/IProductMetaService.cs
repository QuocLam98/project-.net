using HomeDoctor.Models;
using HomeDoctor.Models.ViewModels;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;

namespace HomeDoctor.Services.Interfaces
{
    public interface IProductMetaService : IBaseService<ProductMeta>
    {
        Task<DTResult<ProductMetaViewModel>> ListServerSide(ProductMetaDTParameters parameters);
    }
}
