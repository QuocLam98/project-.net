
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
        public interface IPostCategoryRepository
        {
            Task <List< PostCategory>> List();

            Task <List< PostCategory>> Search(string keyword);

            Task <List< PostCategory>> ListPaging(int pageIndex, int pageSize);

            Task <PostCategory> Detail(int ? postId);

            Task <PostCategory> Add(PostCategory PostCategory);

            Task Update(PostCategory PostCategory);

            Task Delete(PostCategory PostCategory);

            Task <int> DeletePermanently(int ? PostCategoryId);

            int Count();

            Task <DTResult<PostCategory>> ListServerSide(PostCategoryDTParameters parameters);
        }
    }
