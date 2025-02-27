
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
        public interface IDistrictRepository
        {
            Task <List< District>> List();

            Task <List< District>> Search(string keyword);

            Task <List< District>> ListPaging(int pageIndex, int pageSize);

            Task <District> Detail(int ? postId);

            Task <District> Add(District District);

            Task Update(District District);

            Task Delete(District District);

            Task <int> DeletePermanently(int ? DistrictId);

            int Count();

            Task <DTResult<DistrictViewModel>> ListServerSide(DistrictDTParameters parameters);

            Task<List<District>> ListByProvinceId(int id);
        }
    }
