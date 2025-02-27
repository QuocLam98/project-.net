
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services
{
    public class ProductStatusService : IProductStatusService
    {
        IProductStatusRepository productStatusRepository;
        public ProductStatusService(
            IProductStatusRepository _productStatusRepository
            )
        {
            productStatusRepository = _productStatusRepository;
        }
        public async Task Add(ProductStatus obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await productStatusRepository.Add(obj);
        }

        public int Count()
        {
            var result = productStatusRepository.Count();
            return result;
        }

        public async Task Delete(ProductStatus obj)
        {
            obj.Active = 0;
            await productStatusRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await productStatusRepository.DeletePermanently(id);
        }

        public async Task<ProductStatus> Detail(int? id)
        {
            return await productStatusRepository.Detail(id);
        }

        public async Task<List<ProductStatus>> List()
        {
            return await productStatusRepository.List();
        }

        public async Task<List<ProductStatus>> ListPaging(int pageIndex, int pageSize)
        {
            return await productStatusRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<ProductStatus>> ListServerSide(ProductStatusDTParameters parameters)
        {
            return await productStatusRepository.ListServerSide(parameters);
        }

        public async Task<List<ProductStatus>> Search(string keyword)
        {
            return await productStatusRepository.Search(keyword);
        }

        public async Task Update(ProductStatus obj)
        {
            await productStatusRepository.Update(obj);
        }
        public async Task<bool> IsNameExist(int id, string name)
        {
            return await productStatusRepository.IsNameExist(id, name);
        }
    }
}

