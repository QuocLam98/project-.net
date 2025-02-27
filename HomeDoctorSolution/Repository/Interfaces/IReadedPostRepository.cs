
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
        public interface IReadedPostRepository
        {
            Task <List< ReadedPost>> List();

            Task <List< ReadedPost>> Search(string keyword);

            Task <List< ReadedPost>> ListPaging(int pageIndex, int pageSize);

            Task <ReadedPost> Detail(int ? postId);

            Task <ReadedPost> Add(ReadedPost ReadedPost);

            Task Update(ReadedPost ReadedPost);

            Task Delete(ReadedPost ReadedPost);

            Task <int> DeletePermanently(int ? ReadedPostId);

            int Count();

            Task <DTResult<ReadedPostViewModel>> ListServerSide(ReadedPostDTParameters parameters);
        }
    }
