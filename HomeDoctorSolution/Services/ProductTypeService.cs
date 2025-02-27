
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
            public class ProductTypeService : IProductTypeService
            {
                IProductTypeRepository productTypeRepository;
                public ProductTypeService(
                    IProductTypeRepository _productTypeRepository
                    )
                {
                    productTypeRepository = _productTypeRepository;
                }
                public async Task Add(ProductType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await productTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = productTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(ProductType obj)
                {
                    obj.Active = 0;
                    await productTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await productTypeRepository.DeletePermanently(id);
                }
        
                public async Task<ProductType> Detail(int? id)
                {
                    return await productTypeRepository.Detail(id);
                }
        
                public async Task<List<ProductType>> List()
                {
                    return await productTypeRepository.List();
                }
        
                public async Task<List<ProductType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await productTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<ProductType>> ListServerSide(ProductTypeDTParameters parameters)
                {
                    return await productTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<ProductType>> Search(string keyword)
                {
                    return await productTypeRepository.Search(keyword);
                }
        
                public async Task Update(ProductType obj)
                {
                    await productTypeRepository.Update(obj);
                }
        public async Task<bool> IsNameExist(int id, string name)
        {
            return await productTypeRepository.IsNameExist(id, name);
        }
    }
        }
    
    