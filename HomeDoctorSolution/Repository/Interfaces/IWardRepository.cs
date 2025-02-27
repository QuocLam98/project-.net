
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
        public interface IWardRepository
        {
            Task <List< Ward>> List();

            Task <List< Ward>> Search(string keyword);

            Task <List< Ward>> ListPaging(int pageIndex, int pageSize);

            Task <Ward> Detail(int ? postId);

            Task <Ward> Add(Ward Ward);

            Task Update(Ward Ward);

            Task Delete(Ward Ward);

            Task <int> DeletePermanently(int ? WardId);

            int Count();

            Task <DTResult<WardViewModel>> ListServerSide(WardDTParameters parameters);

            Task<List<Ward>> ListByDistrictId(int id);
        }
    }
