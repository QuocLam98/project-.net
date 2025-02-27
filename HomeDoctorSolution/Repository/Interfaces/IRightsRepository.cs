
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
        public interface IRightsRepository
        {
            Task <List< Right>> List();

            Task <List< Right>> Search(string keyword);

            Task <List< Right>> ListPaging(int pageIndex, int pageSize);

            Task < Right> Detail(int ? postId);

            Task <Right> Add(Right Right);

            Task Update(Right Right);

            Task Delete(Right Right);

            Task <int> DeletePermanently(int ? RightsId);

            int Count();

            Task <DTResult<Right>> ListServerSide(RightsDTParameters parameters);
        }
    }
