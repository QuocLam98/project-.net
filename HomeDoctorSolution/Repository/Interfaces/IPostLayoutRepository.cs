
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
        public interface IPostLayoutRepository
        {
            Task <List< PostLayout>> List();

            Task <List< PostLayout>> Search(string keyword);

            Task <List< PostLayout>> ListPaging(int pageIndex, int pageSize);

            Task <PostLayout> Detail(int ? postId);

            Task <PostLayout> Add(PostLayout PostLayout);

            Task Update(PostLayout PostLayout);

            Task Delete(PostLayout PostLayout);

            Task <int> DeletePermanently(int ? PostLayoutId);

            int Count();

            Task <DTResult<PostLayout>> ListServerSide(PostLayoutDTParameters parameters);
        }
    }
