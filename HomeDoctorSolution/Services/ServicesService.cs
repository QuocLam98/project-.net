
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
    public class ServicesService : IServicesService
    {
        IServicesRepository servicesRepository;
        public ServicesService(
            IServicesRepository _servicesRepository
            )
        {
            servicesRepository = _servicesRepository;
        }
        public async Task Add(Service obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await servicesRepository.Add(obj);
        }

        public int Count()
        {
            var result = servicesRepository.Count();
            return result;
        }

        public async Task Delete(Service obj)
        {
            obj.Active = 0;
            await servicesRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await servicesRepository.DeletePermanently(id);
        }

        public async Task<Service> Detail(int? id)
        {
            return await servicesRepository.Detail(id);
        }

        public async Task<List<Service>> List()
        {
            return await servicesRepository.List();
        }

        public async Task<List<ServicesViewModel>> ListTest()
        {
            return await servicesRepository.ListTest();
        }
        public async Task<List<ServicesViewModel>> ListFourService()
        {
            return await servicesRepository.ListFourService();
        }
        public async Task<List<Service>> ListPaging(int pageIndex, int pageSize)
        {
            return await servicesRepository.ListPaging(pageIndex, pageSize);
        }
        public async Task<List<Service>> ListPaging(int pageIndex, int pageSize, int clinicId)
        {
            return await servicesRepository.ListPaging(pageIndex, pageSize, clinicId);
        }
        public async Task<List<ServicesViewModel>> ListPaging(int pageIndex, int pageSize, int clinicId,string keyword)
        {
            return await servicesRepository.ListPaging(pageIndex, pageSize, clinicId, keyword);
        }
        public async Task<List<ServicesViewModel>> ListPagingViewModel(int pageIndex, int pageSize)
        {
            return await servicesRepository.ListPagingViewModel(pageIndex, pageSize);
        }
        public async Task<List<ServicesViewModel>> ListPagingViewModel(int pageIndex, int pageSize, int clinicId)
        {
            return await servicesRepository.ListPagingViewModel(pageIndex, pageSize, clinicId);
        }
        public async Task<DTResult<ServicesViewModel>> ListServerSide(ServicesDTParameters parameters)
        {
            return await servicesRepository.ListServerSide(parameters);
        }

        public async Task<List<Service>> Search(string keyword)
        {
            return await servicesRepository.Search(keyword);
        }

        public async Task<List<ServicesViewModel>> Top4Service(int id)
        {
            return await servicesRepository.Top4Service(id);
        }

        public async Task Update(Service obj)
        {
            await servicesRepository.Update(obj);
        }
    }
}

