
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
        public interface IPostPublishStatusRepository
        {
            Task <List< PostPublishStatus>> List();

            Task <List< PostPublishStatus>> Search(string keyword);

            Task <List< PostPublishStatus>> ListPaging(int pageIndex, int pageSize);

            Task <PostPublishStatus> Detail(int ? postId);

            Task <PostPublishStatus> Add(PostPublishStatus PostPublishStatus);

            Task Update(PostPublishStatus PostPublishStatus);

            Task Delete(PostPublishStatus PostPublishStatus);

            Task <int> DeletePermanently(int ? PostPublishStatusId);

            int Count();

            Task <DTResult<PostPublishStatus>> ListServerSide(PostPublishStatusDTParameters parameters);
        }
    }
