
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
            public class VoucherService : IVoucherService
            {
                IVoucherRepository voucherRepository;
                public VoucherService(
                    IVoucherRepository _voucherRepository
                    )
                {
                    voucherRepository = _voucherRepository;
                }
                public async Task Add(Voucher obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await voucherRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = voucherRepository.Count();
                    return result;
                }
        
                public async Task Delete(Voucher obj)
                {
                    obj.Active = 0;
                    await voucherRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await voucherRepository.DeletePermanently(id);
                }
        
                public async Task<Voucher> Detail(int? id)
                {
                    return await voucherRepository.Detail(id);
                }
        
                public async Task<List<Voucher>> List()
                {
                    return await voucherRepository.List();
                }
        
                public async Task<List<Voucher>> ListPaging(int pageIndex, int pageSize)
                {
                    return await voucherRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<VoucherViewModel>> ListServerSide(VoucherDTParameters parameters)
                {
                    return await voucherRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Voucher>> Search(string keyword)
                {
                    return await voucherRepository.Search(keyword);
                }
        
                public async Task Update(Voucher obj)
                {
                    await voucherRepository.Update(obj);
                }
            }
        }
    
    