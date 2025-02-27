
    using HomeDoctorSolution.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HomeDoctorSolution.Util;
    using HomeDoctorSolution.Util.Parameters;
    using HomeDoctorSolution.Models.ViewModels;


    namespace HomeDoctorSolution.Repository
    {
        public interface IProductTypeRepository
        {
            Task <List< ProductType>> List();

            Task <List< ProductType>> Search(string keyword);

            Task <List< ProductType>> ListPaging(int pageIndex, int pageSize);

            Task <ProductType> Detail(int ? postId);

            Task <ProductType> Add(ProductType ProductType);

            Task Update(ProductType ProductType);

            Task Delete(ProductType ProductType);

            Task <int> DeletePermanently(int ? ProductTypeId);

            int Count();

            Task <DTResult<ProductType>> ListServerSide(ProductTypeDTParameters parameters);
            Task<bool> IsNameExist(int id, string name);
        }
    }
