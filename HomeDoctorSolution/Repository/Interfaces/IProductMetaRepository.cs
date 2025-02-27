using HomeDoctor.Models;
using HomeDoctor.Models.ViewModels;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;

namespace HomeDoctor.Repository.Interfaces
{
    public interface IProductMetaRepository
    {
        Task<List<ProductMeta>> List();

        Task<List<ProductMeta>> Search(string keyword);

        Task<List<ProductMeta>> ListPaging(int pageIndex, int pageSize);

        Task<ProductMeta> Detail(int? id);

        Task<ProductMeta> Add(ProductMeta ProductMeta);

        Task Update(ProductMeta obj);

        Task Delete(ProductMeta ProductMeta);

        Task<int> DeletePermanently(int? objId);

        int Count();

        Task<DTResult<ProductMetaViewModel>> ListServerSide(ProductMetaDTParameters parameters);
    }
}
