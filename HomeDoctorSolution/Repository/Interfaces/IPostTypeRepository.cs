
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
        public interface IPostTypeRepository
        {
            Task <List< PostType>> List();

            Task <List< PostType>> Search(string keyword);

            Task <List< PostType>> ListPaging(int pageIndex, int pageSize);

            Task <PostType> Detail(int ? postId);

            Task <PostType> Add(PostType PostType);

            Task Update(PostType PostType);

            Task Delete(PostType PostType);

            Task <int> DeletePermanently(int ? PostTypeId);

            int Count();

            Task <DTResult<PostType>> ListServerSide(PostTypeDTParameters parameters);
        }
    }
