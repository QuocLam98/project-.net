
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
            public class VoucherStatusService : IVoucherStatusService
            {
                IVoucherStatusRepository voucherStatusRepository;
                public VoucherStatusService(
                    IVoucherStatusRepository _voucherStatusRepository
                    )
                {
                    voucherStatusRepository = _voucherStatusRepository;
                }
                public async Task Add(VoucherStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await voucherStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = voucherStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(VoucherStatus obj)
                {
                    obj.Active = 0;
                    await voucherStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await voucherStatusRepository.DeletePermanently(id);
                }
        
                public async Task<VoucherStatus> Detail(int? id)
                {
                    return await voucherStatusRepository.Detail(id);
                }
        
                public async Task<List<VoucherStatus>> List()
                {
                    return await voucherStatusRepository.List();
                }
        
                public async Task<List<VoucherStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await voucherStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<VoucherStatus>> ListServerSide(VoucherStatusDTParameters parameters)
                {
                    return await voucherStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<VoucherStatus>> Search(string keyword)
                {
                    return await voucherStatusRepository.Search(keyword);
                }
        
                public async Task Update(VoucherStatus obj)
                {
                    await voucherStatusRepository.Update(obj);
                }
            }
        }
    
    