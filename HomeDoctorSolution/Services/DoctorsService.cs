
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
    public class DoctorsService : IDoctorsService
    {
        IDoctorsRepository doctorsRepository;
        public DoctorsService(
            IDoctorsRepository _doctorsRepository
            )
        {
            doctorsRepository = _doctorsRepository;
        }
        public async Task Add(Doctor obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await doctorsRepository.Add(obj);
        }

        public int Count()
        {
            var result = doctorsRepository.Count();
            return result;
        }

        public async Task Delete(Doctor obj)
        {
            obj.Active = 0;
            await doctorsRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await doctorsRepository.DeletePermanently(id);
        }

        public async Task<Doctor> Detail(int? id)
        {
            return await doctorsRepository.Detail(id);
        }

        public async Task<List<Doctor>> List()
        {
            return await doctorsRepository.List();
        }

        public async Task<List<Doctor>> ListPaging(int pageIndex, int pageSize)
        {
            return await doctorsRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<DoctorsViewModel>> ListServerSide(DoctorsDTParameters parameters)
        {
            return await doctorsRepository.ListServerSide(parameters);
        }

        public async Task<List<Doctor>> Search(string keyword)
        {
            return await doctorsRepository.Search(keyword);
        }

        public async Task Update(Doctor obj)
        {
            await doctorsRepository.Update(obj);
        }

        public async Task<List<Doctors>> ListDoctor()
        {
            return await doctorsRepository.ListDoctor();
        }
        public async Task<List<DoctorsViewModel>> ListThreeDoctorByTimeDesc()
        {
            return await doctorsRepository.ListThreeDoctorByTimeDesc();
        }
        public async Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize)
        {
            return await doctorsRepository.listPagingViewModel(pageIndex, pageSize);
        }
        public async Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize, int serviceId, int healthFacilityId)
        {
            return await doctorsRepository.listPagingViewModel(pageIndex, pageSize,serviceId,healthFacilityId);
        }
        public async Task<List<DoctorsViewModel>> listPagingViewModel(int pageIndex, int pageSize, int serviceId, int healthFacilityId,string keyword, string district)
        {
            return await doctorsRepository.listPagingViewModel(pageIndex, pageSize, serviceId, healthFacilityId,keyword, district);
        }
        public async Task<int> listPagingViewModelCount(int serviceId, int healthFacilityId,string keyword, string district)
        {
            return await doctorsRepository.listPagingViewModelCount( serviceId, healthFacilityId,keyword, district);
        }
        public async Task<Doctor> DetailViewModel(int? id)
        {
            return await doctorsRepository.DetailViewModel(id);
        }

        public async Task<List<Doctor>> get6DoctorOutstanding()
        {
            return await doctorsRepository.get6DoctorOutstanding();
        }
        public async Task<List<DoctorsViewModel>> ListDoctorBooking(int AccountId)
        {
            return await doctorsRepository.ListDoctorBooking(AccountId);
        }
    }
}

