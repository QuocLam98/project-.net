
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
        public interface IHealthFacilityTypeRepository
        {
            Task <List< HealthFacilityType>> List();

            Task <List< HealthFacilityType>> Search(string keyword);

            Task <List< HealthFacilityType>> ListPaging(int pageIndex, int pageSize);

            Task <HealthFacilityType> Detail(int ? postId);

            Task <HealthFacilityType> Add(HealthFacilityType HealthFacilityType);

            Task Update(HealthFacilityType HealthFacilityType);

            Task Delete(HealthFacilityType HealthFacilityType);

            Task <int> DeletePermanently(int ? HealthFacilityTypeId);

            int Count();

            Task <DTResult<HealthFacilityType>> ListServerSide(HealthFacilityTypeDTParameters parameters);
        }
    }
