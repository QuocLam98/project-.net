
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
        public interface IFeaturedPostTypeRepository
        {
            Task <List< FeaturedPostType>> List();

            Task <List< FeaturedPostType>> Search(string keyword);

            Task <List< FeaturedPostType>> ListPaging(int pageIndex, int pageSize);

            Task <FeaturedPostType> Detail(int ? postId);

            Task <FeaturedPostType> Add(FeaturedPostType FeaturedPostType);

            Task Update(FeaturedPostType FeaturedPostType);

            Task Delete(FeaturedPostType FeaturedPostType);

            Task <int> DeletePermanently(int ? FeaturedPostTypeId);

            int Count();

            Task <DTResult<FeaturedPostType>> ListServerSide(FeaturedPostTypeDTParameters parameters);
        }
    }
