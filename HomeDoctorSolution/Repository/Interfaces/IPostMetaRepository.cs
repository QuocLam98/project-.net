
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
        public interface IPostMetaRepository
        {
            Task <List< PostMeta>> List();

            Task <List< PostMeta>> Search(string keyword);

            Task <List< PostMeta>> ListPaging(int pageIndex, int pageSize);

            Task <PostMeta> Detail(int ? postId);

            Task <PostMeta> Add(PostMeta PostMeta);

            Task Update(PostMeta PostMeta);

            Task Delete(PostMeta PostMeta);

            Task <int> DeletePermanently(int ? PostMetaId);

            int Count();

            Task <DTResult<PostMetaViewModel>> ListServerSide(PostMetaDTParameters parameters);
        }
    }
