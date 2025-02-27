
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
    public class DistrictService : IDistrictService
    {
        IDistrictRepository districtRepository;
        public DistrictService(
            IDistrictRepository _districtRepository
            )
        {
            districtRepository = _districtRepository;
        }
        public async Task Add(District obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await districtRepository.Add(obj);
        }

        public int Count()
        {
            var result = districtRepository.Count();
            return result;
        }

        public async Task Delete(District obj)
        {
            obj.Active = 0;
            await districtRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await districtRepository.DeletePermanently(id);
        }

        public async Task<District> Detail(int? id)
        {
            return await districtRepository.Detail(id);
        }

        public async Task<List<District>> List()
        {
            return await districtRepository.List();
        }

        public async Task<List<District>> ListPaging(int pageIndex, int pageSize)
        {
            return await districtRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<DistrictViewModel>> ListServerSide(DistrictDTParameters parameters)
        {
            return await districtRepository.ListServerSide(parameters);
        }

        public async Task<List<District>> Search(string keyword)
        {
            return await districtRepository.Search(keyword);
        }

        public async Task Update(District obj)
        {
            await districtRepository.Update(obj);
        }

        public async Task<List<District>> ListByProvinceId(int id)
        {
            return await districtRepository.ListByProvinceId(id);
        }
    }
}

