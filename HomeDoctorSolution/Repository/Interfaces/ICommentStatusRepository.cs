
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
        public interface ICommentStatusRepository
        {
            Task <List< CommentStatus>> List();

            Task <List< CommentStatus>> Search(string keyword);

            Task <List< CommentStatus>> ListPaging(int pageIndex, int pageSize);

            Task <CommentStatus> Detail(int ? postId);

            Task <CommentStatus> Add(CommentStatus CommentStatus);

            Task Update(CommentStatus CommentStatus);

            Task Delete(CommentStatus CommentStatus);

            Task <int> DeletePermanently(int ? CommentStatusId);

            int Count();

            Task <DTResult<CommentStatus>> ListServerSide(CommentStatusDTParameters parameters);
        }
    }
