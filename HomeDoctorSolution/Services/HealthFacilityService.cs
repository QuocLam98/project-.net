
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Services
{
    public class HealthFacilityService : IHealthFacilityService
    {
        IHealthFacilityRepository healthFacilityRepository;
        public HealthFacilityService(
            IHealthFacilityRepository _healthFacilityRepository
            )
        {
            healthFacilityRepository = _healthFacilityRepository;
        }
        public async Task Add(HealthFacility obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await healthFacilityRepository.Add(obj);
        }

        public int Count()
        {
            var result = healthFacilityRepository.Count();
            return result;
        }

        public async Task Delete(HealthFacility obj)
        {
            obj.Active = 0;
            await healthFacilityRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await healthFacilityRepository.DeletePermanently(id);
        }

        public async Task<HealthFacility> Detail(int? id)
        {
            return await healthFacilityRepository.Detail(id);
        }

        public async Task<List<HealthFacility>> List()
        {
            return await healthFacilityRepository.List();
        }

        public async Task<List<HealthFacility>> ListPaging(int pageIndex, int pageSize)
        {
            return await healthFacilityRepository.ListPaging(pageIndex, pageSize);
        }
        public async Task<List<HealthFacility>> ListPagingView(int pageIndex, int pageSize, int provinceId, string keyword)
        {
            return await healthFacilityRepository.ListPagingView(pageIndex, pageSize, provinceId, keyword);
        }

        public async Task<DTResult<HealthFacilityViewModel>> ListServerSide(HealthFacilityDTParameters parameters)
        {
            return await healthFacilityRepository.ListServerSide(parameters);
        }

        public async Task<List<HealthFacility>> Search(string keyword)
        {
            return await healthFacilityRepository.Search(keyword);
        }

        public async Task Update(HealthFacility obj)
        {
            await healthFacilityRepository.Update(obj);
        }
    }
}

