
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
            public class VoucherTypeService : IVoucherTypeService
            {
                IVoucherTypeRepository voucherTypeRepository;
                public VoucherTypeService(
                    IVoucherTypeRepository _voucherTypeRepository
                    )
                {
                    voucherTypeRepository = _voucherTypeRepository;
                }
                public async Task Add(VoucherType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await voucherTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = voucherTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(VoucherType obj)
                {
                    obj.Active = 0;
                    await voucherTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await voucherTypeRepository.DeletePermanently(id);
                }
        
                public async Task<VoucherType> Detail(int? id)
                {
                    return await voucherTypeRepository.Detail(id);
                }
        
                public async Task<List<VoucherType>> List()
                {
                    return await voucherTypeRepository.List();
                }
        
                public async Task<List<VoucherType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await voucherTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<VoucherType>> ListServerSide(VoucherTypeDTParameters parameters)
                {
                    return await voucherTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<VoucherType>> Search(string keyword)
                {
                    return await voucherTypeRepository.Search(keyword);
                }
        
                public async Task Update(VoucherType obj)
                {
                    await voucherTypeRepository.Update(obj);
                }
            }
        }
    
    