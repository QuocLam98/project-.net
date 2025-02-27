
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
            public class BookingStatusService : IBookingStatusService
            {
                IBookingStatusRepository bookingStatusRepository;
                public BookingStatusService(
                    IBookingStatusRepository _bookingStatusRepository
                    )
                {
                    bookingStatusRepository = _bookingStatusRepository;
                }
                public async Task Add(BookingStatus obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await bookingStatusRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = bookingStatusRepository.Count();
                    return result;
                }
        
                public async Task Delete(BookingStatus obj)
                {
                    obj.Active = 0;
                    await bookingStatusRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await bookingStatusRepository.DeletePermanently(id);
                }
        
                public async Task<BookingStatus> Detail(int? id)
                {
                    return await bookingStatusRepository.Detail(id);
                }
        
                public async Task<List<BookingStatus>> List()
                {
                    return await bookingStatusRepository.List();
                }
        
                public async Task<List<BookingStatus>> ListPaging(int pageIndex, int pageSize)
                {
                    return await bookingStatusRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<BookingStatus>> ListServerSide(BookingStatusDTParameters parameters)
                {
                    return await bookingStatusRepository.ListServerSide(parameters);
                }
        
                public async Task<List<BookingStatus>> Search(string keyword)
                {
                    return await bookingStatusRepository.Search(keyword);
                }
        
                public async Task Update(BookingStatus obj)
                {
                    await bookingStatusRepository.Update(obj);
                }
            }
        }
    
    