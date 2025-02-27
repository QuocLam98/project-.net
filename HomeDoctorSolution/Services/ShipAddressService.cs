
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
            public class ShipAddressService : IShipAddressService
            {
                IShipAddressRepository shipAddressRepository;
                public ShipAddressService(
                    IShipAddressRepository _shipAddressRepository
                    )
                {
                    shipAddressRepository = _shipAddressRepository;
                }
                public async Task Add(ShipAddress obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await shipAddressRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = shipAddressRepository.Count();
                    return result;
                }
        
                public async Task Delete(ShipAddress obj)
                {
                    obj.Active = 0;
                    await shipAddressRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await shipAddressRepository.DeletePermanently(id);
                }
        
                public async Task<ShipAddress> Detail(int? id)
                {
                    return await shipAddressRepository.Detail(id);
                }
        
                public async Task<List<ShipAddress>> List()
                {
                    return await shipAddressRepository.List();
                }
        
                public async Task<List<ShipAddress>> ListPaging(int pageIndex, int pageSize)
                {
                    return await shipAddressRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<ShipAddressViewModel>> ListServerSide(ShipAddressDTParameters parameters)
                {
                    return await shipAddressRepository.ListServerSide(parameters);
                }
        
                public async Task<List<ShipAddress>> Search(string keyword)
                {
                    return await shipAddressRepository.Search(keyword);
                }
        
                public async Task Update(ShipAddress obj)
                {
                    await shipAddressRepository.Update(obj);
                }
            }
        }
    
    