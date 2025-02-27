
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
        public interface ICommentRepository
        {
            Task <List< Comment>> List();

            Task <List< Comment>> Search(string keyword);

            Task <List< Comment>> ListPaging(int pageIndex, int pageSize);

            Task <Comment> Detail(int ? postId);

            Task <Comment> Add(Comment Comment);

            Task Update(Comment Comment);

            Task Delete(Comment Comment);

            Task <int> DeletePermanently(int ? CommentId);

            int Count();

            Task <DTResult<CommentViewModel>> ListServerSide(CommentDTParameters parameters);
        }
    }
