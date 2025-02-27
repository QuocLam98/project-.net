
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
        public interface IHealthFacilityStatusRepository
        {
            Task <List< HealthFacilityStatus>> List();

            Task <List< HealthFacilityStatus>> Search(string keyword);

            Task <List< HealthFacilityStatus>> ListPaging(int pageIndex, int pageSize);

            Task <HealthFacilityStatus> Detail(int ? postId);

            Task <HealthFacilityStatus> Add(HealthFacilityStatus HealthFacilityStatus);

            Task Update(HealthFacilityStatus HealthFacilityStatus);

            Task Delete(HealthFacilityStatus HealthFacilityStatus);

            Task <int> DeletePermanently(int ? HealthFacilityStatusId);

            int Count();

            Task <DTResult<HealthFacilityStatus>> ListServerSide(HealthFacilityStatusDTParameters parameters);
        }
    }
