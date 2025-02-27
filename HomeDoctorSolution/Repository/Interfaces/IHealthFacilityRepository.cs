
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
    public interface IHealthFacilityRepository
    {
        Task<List<HealthFacility>> List();

        Task<List<HealthFacility>> Search(string keyword);

        Task<List<HealthFacility>> ListPaging(int pageIndex, int pageSize);
        Task<List<HealthFacility>> ListPagingView(int pageIndex, int pageSize, int provinceId, string keyword);

        Task<HealthFacility> Detail(int? postId);

        Task<HealthFacility> Add(HealthFacility HealthFacility);

        Task Update(HealthFacility HealthFacility);

        Task Delete(HealthFacility HealthFacility);

        Task<int> DeletePermanently(int? HealthFacilityId);

        int Count();

        Task<DTResult<HealthFacilityViewModel>> ListServerSide(HealthFacilityDTParameters parameters);
    }
}
