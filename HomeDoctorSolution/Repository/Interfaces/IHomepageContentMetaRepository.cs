
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
        public interface IHomepageContentMetaRepository
        {
            Task <List< HomepageContentMeta>> List();

            Task <List< HomepageContentMeta>> Search(string keyword);

            Task <List< HomepageContentMeta>> ListPaging(int pageIndex, int pageSize);

            Task <HomepageContentMeta> Detail(int ? postId);

            Task <HomepageContentMeta> Add(HomepageContentMeta HomepageContentMeta);

            Task Update(HomepageContentMeta HomepageContentMeta);

            Task Delete(HomepageContentMeta HomepageContentMeta);

            Task <int> DeletePermanently(int ? HomepageContentMetaId);

            int Count();

            Task <DTResult<HomepageContentMetaViewModel>> ListServerSide(HomepageContentMetaDTParameters parameters);
        }
    }
