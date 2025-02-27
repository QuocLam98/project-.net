
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
            public class BookingTypeService : IBookingTypeService
            {
                IBookingTypeRepository bookingTypeRepository;
                public BookingTypeService(
                    IBookingTypeRepository _bookingTypeRepository
                    )
                {
                    bookingTypeRepository = _bookingTypeRepository;
                }
                public async Task Add(BookingType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await bookingTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = bookingTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(BookingType obj)
                {
                    obj.Active = 0;
                    await bookingTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await bookingTypeRepository.DeletePermanently(id);
                }
        
                public async Task<BookingType> Detail(int? id)
                {
                    return await bookingTypeRepository.Detail(id);
                }
        
                public async Task<List<BookingType>> List()
                {
                    return await bookingTypeRepository.List();
                }
        
                public async Task<List<BookingType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await bookingTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<BookingType>> ListServerSide(BookingTypeDTParameters parameters)
                {
                    return await bookingTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<BookingType>> Search(string keyword)
                {
                    return await bookingTypeRepository.Search(keyword);
                }
        
                public async Task Update(BookingType obj)
                {
                    await bookingTypeRepository.Update(obj);
                }
            }
        }
    
    