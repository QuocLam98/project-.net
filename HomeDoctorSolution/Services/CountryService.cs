
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
            public class CountryService : ICountryService
            {
                ICountryRepository countryRepository;
                public CountryService(
                    ICountryRepository _countryRepository
                    )
                {
                    countryRepository = _countryRepository;
                }
                public async Task Add(Country obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await countryRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = countryRepository.Count();
                    return result;
                }
        
                public async Task Delete(Country obj)
                {
                    obj.Active = 0;
                    await countryRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await countryRepository.DeletePermanently(id);
                }
        
                public async Task<Country> Detail(int? id)
                {
                    return await countryRepository.Detail(id);
                }
        
                public async Task<List<Country>> List()
                {
                    return await countryRepository.List();
                }
        
                public async Task<List<Country>> ListPaging(int pageIndex, int pageSize)
                {
                    return await countryRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<Country>> ListServerSide(CountryDTParameters parameters)
                {
                    return await countryRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Country>> Search(string keyword)
                {
                    return await countryRepository.Search(keyword);
                }
        
                public async Task Update(Country obj)
                {
                    await countryRepository.Update(obj);
                }
            }
        }
    
    