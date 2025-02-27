using HomeDoctor.Models;
using HomeDoctor.Models.ViewModels;
using HomeDoctor.Repository.Interfaces;
using HomeDoctor.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using NPOI.SS.Util;

namespace HomeDoctor.Services
{
    public class ProductMetaService : IProductMetaService
    {
        IProductMetaRepository productMetaRepository;
        public ProductMetaService(IProductMetaRepository _productMetaRepository)
        {
           productMetaRepository = _productMetaRepository;
        }

        public async Task Add(ProductMeta obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await productMetaRepository.Add(obj);
        }

        public int Count()
        {
            var result = productMetaRepository.Count();
            return result;
        }

        public async Task Delete(ProductMeta obj)
        {
            obj.Active = 0;
            await productMetaRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await productMetaRepository.DeletePermanently(id);
        }

        public async Task<ProductMeta> Detail(int? id)
        {
            return await productMetaRepository.Detail(id);  
        }

        public async Task<List<ProductMeta>> List()
        {
            return await productMetaRepository.List(); 
        }

        public async Task<List<ProductMeta>> ListPaging(int pageIndex, int pageSize)
        {
            return await productMetaRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<ProductMetaViewModel>> ListServerSide(ProductMetaDTParameters parameters)
        {
            return await productMetaRepository.ListServerSide(parameters);
        }

        public async Task<List<ProductMeta>> Search(string keyword)
        {
           return await productMetaRepository.Search(keyword);  
        }

        public async Task Update(ProductMeta obj)
        {
            await productMetaRepository.Update(obj);
        }
    }
}
