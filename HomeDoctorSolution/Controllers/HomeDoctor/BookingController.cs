

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Controllers.Core;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HomeDoctorSolution.Helper;

namespace HomeDoctorSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : BaseController
    {
        IBookingService service;
        public BookingController(IBookingService _service, ICacheHelper cacheHelper) : base(cacheHelper)
        {
            service = _service;
        }
        [HttpGet]
        [Route("admin/List")]
        public async Task<IActionResult> AdminListServerSide()
        {
            var accountIdGetLog = this.GetLoggedInUserId();
            var roleIdGetLog = this.GetLoggedInRoleId();
            ViewBag.accountIdGetLog = accountIdGetLog;
            ViewBag.roleIdGetLog = roleIdGetLog;
            return View();
        }

        [HttpGet]
        [Route("admin/list-test")]
        public async Task<IActionResult> AdminListTestServerSide()
        {
            var accountIdGetLog = this.GetLoggedInUserId();
            var roleIdGetLog = this.GetLoggedInRoleId();
            ViewBag.accountIdGetLog = accountIdGetLog;
            ViewBag.roleIdGetLog = roleIdGetLog;
            return View();
        }
        [HttpGet]
        [Route("api/List")]
        public async Task<IActionResult> List()
        {
            try
            {
                var dataList = await service.List();
                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/list-booking-test")]
        public async Task<IActionResult> ListBookingTests(string startTime, string endTime, int bookingStatusId, string filterId)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                var dataList = await service.ListTestByAccountId(startTime, endTime, bookingStatusId, filterId, accountId);
                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Detail/{Id}")]
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var dataList = await service.Detail(Id);
                if (dataList == null)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/DetailViewModel/{Id}")]
        public async Task<IActionResult> DetailViewModel(int Id)
        {
            try
            {
                var dataList = await service.DetailViewModel(Id);
                if (dataList == null)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Search")]
        public async Task<IActionResult> Search(string keyword)
        {
            try
            {
                var dataList = await service.Search(keyword);
                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/ListPaging")]
        public async Task<IActionResult> ListPaging(int pageIndex, int pageSize)
        {
            if (pageIndex < 0 || pageSize < 0) return BadRequest();
            try
            {
                var dataList = await service.ListPaging(pageIndex, pageSize);

                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }

                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/Add")]
        public async Task<IActionResult> Add([FromBody] Booking model)
        {

            //1. business logic

            //data validation

            //2. add new object
            try
            {
                //var roleId = this.GetLoggedInRoleId();
                var roleId = 1000002;
                await service.AddByRole(model, roleId);
                var homedoctorSolutionResponse = HomeDoctorResponse.CREATED(model);
                return Created("", homedoctorSolutionResponse);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpPost]
        [Route("api/Update")]
        public async Task<IActionResult> Update([FromBody] Booking model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //1. business logic 
                    //2. update object
                    var roleId = this.GetLoggedInRoleId();
                    await service.UpdateByRole(model, roleId);
                    //await service.Update(model);
                    var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(model);
                    return Ok(happysmilesolutionResponse);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("api/update-test")]
        public async Task<IActionResult> UpdateTest([FromBody] Booking model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //1. business logic 
                    //2. update object
                    var roleId = this.GetLoggedInRoleId();
                    await service.UpdateByRole(model, roleId);
                    //await service.Update(model);
                    var happysmilesolutionResponse = HomeDoctorResponse.SUCCESS(model);
                    return Ok(happysmilesolutionResponse);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("api/Delete")]
        public async Task<IActionResult> Delete([FromBody] Booking model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //1. business logic
                    await service.Delete(model);
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(model);
                    return Ok(homedoctorsolutionResponse);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("api/DeletePermanently")]
        public async Task<IActionResult> DeletePermanently([FromBody] Booking model)
        {
            var result = 0;
            if (!(model.Id > 0))
            {
                return BadRequest();
            }
            try
            {
                //physically delete object
                result = await service.DeletePermanently(model.Id);
                if (result == 0)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(model);
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/Count")]
        public int CountBooking()
        {
            int result = service.Count();
            return result;
        }

        [HttpGet]
        [Route("api/CountStatics")]
        public async Task<IActionResult> CountStatics()
        {
            try
            {
                var result = await service.CountStatics();
                if (result != null)
                {
                    return Ok(HomeDoctorResponse.SUCCESS(result));
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/count-static-test")]
        public async Task<IActionResult> CountStaticTest()
        {
            try
            {
                var result = await service.CountStaticsTest();
                if (result != null)
                {
                    return Ok(HomeDoctorResponse.SUCCESS(result));
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/list-server-side")]
        public async Task<IActionResult> ListServerSide([FromBody] BookingDTParameters parameters)
        {
            try
            {
                var data = await service.ListServerSide(parameters);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("api/list-test-server-side")]
        public async Task<IActionResult> ListTestServerSide([FromBody] BookingDTParameters parameters)
        {
            try
            {
                var data = await service.ListTestServerSide(parameters);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route("api/list-booking-by-bookingStatusId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListBookingByBookingStatusId(int bookingStatusId, int pageIndex, int pageSize)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                var dataList = await service.ListBookingByBookingStatusId(accountId, bookingStatusId, pageIndex, pageSize);
                var homeDoctorSolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homeDoctorSolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/count-list-booking-by-accountId")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public int CountListBookingByAccountId(int bookingStatusId)
        {
            var accountId = this.GetLoggedInUserId();
            int result = service.CountListBookingByAccountId(accountId, bookingStatusId);
            return result;
        }
        #region Lịch cá nhân sự kiện
        /// <summary>
        /// Author: HuyDQ
        /// Created: 
        /// Description: booking Schedule
        /// </summary>
        /// <returns></returns>
        [Route("lich-tu-van")]
        public IActionResult BookingSchedule()
        {
            var accountId = this.GetLoggedInUserId();
            var roleIdGetLog = this.GetLoggedInRoleId();
            ViewBag.AccountId = accountId;
            ViewBag.roleIdGetLog = roleIdGetLog;
            ViewBag.Url = "booking/lich-tu-van";
            return View();
        }

        /// <summary>
        /// Author: HuyDQ
        /// Created: 
        /// Description: booking of doctor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/lich-cua-tu-van-nien/{accountId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListBookingByCounselorId(int accountId)
        {
            try
            {
                var dataList = await service.ListBookingByAccountId(accountId);
                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }
                var homeDoctorSolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homeDoctorSolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
