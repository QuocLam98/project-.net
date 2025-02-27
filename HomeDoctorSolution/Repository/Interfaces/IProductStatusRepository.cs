
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
        public interface IProductStatusRepository
        {
            Task <List< ProductStatus>> List();

            Task <List< ProductStatus>> Search(string keyword);

            Task <List< ProductStatus>> ListPaging(int pageIndex, int pageSize);

            Task <ProductStatus> Detail(int ? postId);

            Task <ProductStatus> Add(ProductStatus ProductStatus);

            Task Update(ProductStatus ProductStatus);

            Task Delete(ProductStatus ProductStatus);

            Task <int> DeletePermanently(int ? ProductStatusId);

            int Count();

            Task <DTResult<ProductStatus>> ListServerSide(ProductStatusDTParameters parameters);

            Task<bool> IsNameExist(int id, string name);
        }
    }
