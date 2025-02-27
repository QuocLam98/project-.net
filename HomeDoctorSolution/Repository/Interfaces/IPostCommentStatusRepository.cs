
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
        public interface IPostCommentStatusRepository
        {
            Task <List< PostCommentStatus>> List();

            Task <List< PostCommentStatus>> Search(string keyword);

            Task <List< PostCommentStatus>> ListPaging(int pageIndex, int pageSize);

            Task <PostCommentStatus> Detail(int ? postId);

            Task <PostCommentStatus> Add(PostCommentStatus PostCommentStatus);

            Task Update(PostCommentStatus PostCommentStatus);

            Task Delete(PostCommentStatus PostCommentStatus);

            Task <int> DeletePermanently(int ? PostCommentStatusId);

            int Count();

            Task <DTResult<PostCommentStatus>> ListServerSide(PostCommentStatusDTParameters parameters);
        }
    }
