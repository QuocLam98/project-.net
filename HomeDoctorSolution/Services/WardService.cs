
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
    public class WardService : IWardService
    {
        IWardRepository wardRepository;
        public WardService(
            IWardRepository _wardRepository
            )
        {
            wardRepository = _wardRepository;
        }
        public async Task Add(Ward obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await wardRepository.Add(obj);
        }

        public int Count()
        {
            var result = wardRepository.Count();
            return result;
        }

        public async Task Delete(Ward obj)
        {
            obj.Active = 0;
            await wardRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await wardRepository.DeletePermanently(id);
        }

        public async Task<Ward> Detail(int? id)
        {
            return await wardRepository.Detail(id);
        }

        public async Task<List<Ward>> List()
        {
            return await wardRepository.List();
        }

        public async Task<List<Ward>> ListPaging(int pageIndex, int pageSize)
        {
            return await wardRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<WardViewModel>> ListServerSide(WardDTParameters parameters)
        {
            return await wardRepository.ListServerSide(parameters);
        }

        public async Task<List<Ward>> Search(string keyword)
        {
            return await wardRepository.Search(keyword);
        }

        public async Task Update(Ward obj)
        {
            await wardRepository.Update(obj);
        }

        public async Task<List<Ward>> ListByDistrictId(int id)
        {
            return await wardRepository.ListByDistrictId(id);
        }

    }
}

