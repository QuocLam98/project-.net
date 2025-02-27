
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
        public interface IHomepageContentRepository
        {
            Task <List< HomepageContent>> List();

            Task <List< HomepageContent>> Search(string keyword);

            Task <List< HomepageContent>> ListPaging(int pageIndex, int pageSize);

            Task <HomepageContent> Detail(int ? postId);

            Task <HomepageContent> Add(HomepageContent HomepageContent);

            Task Update(HomepageContent HomepageContent);

            Task Delete(HomepageContent HomepageContent);

            Task <int> DeletePermanently(int ? HomepageContentId);

            int Count();

            Task <DTResult<HomepageContentViewModel>> ListServerSide(HomepageContentDTParameters parameters);
        }
    }
