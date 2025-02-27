
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
        public interface IHomepageContentTypeRepository
        {
            Task <List< HomepageContentType>> List();

            Task <List< HomepageContentType>> Search(string keyword);

            Task <List< HomepageContentType>> ListPaging(int pageIndex, int pageSize);

            Task <HomepageContentType> Detail(int ? postId);

            Task <HomepageContentType> Add(HomepageContentType HomepageContentType);

            Task Update(HomepageContentType HomepageContentType);

            Task Delete(HomepageContentType HomepageContentType);

            Task <int> DeletePermanently(int ? HomepageContentTypeId);

            int Count();

            Task <DTResult<HomepageContentType>> ListServerSide(HomepageContentTypeDTParameters parameters);
        }
    }
