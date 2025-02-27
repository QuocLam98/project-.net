
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
        public interface IProvinceRepository
        {
            Task <List< Province>> List();

            Task <List< Province>> Search(string keyword);

            Task <List< Province>> ListPaging(int pageIndex, int pageSize);

            Task <Province> Detail(int ? postId);

            Task <Province> Add(Province Province);

            Task Update(Province Province);

            Task Delete(Province Province);

            Task <int> DeletePermanently(int ? ProvinceId);

            int Count();

            Task <DTResult<ProvinceViewModel>> ListServerSide(ProvinceDTParameters parameters);
        }
    }
