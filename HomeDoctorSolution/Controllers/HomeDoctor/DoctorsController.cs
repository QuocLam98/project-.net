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
using HomeDoctorSolution.Helper;

namespace HomeDoctorSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorsController : BaseController
    {
        IDoctorsService service;

        public DoctorsController(IDoctorsService _service, ICacheHelper cacheHelper) : base(cacheHelper)
        {
            service = _service;
        }

        [HttpGet]
        [Route("admin/List")]
        public async Task<IActionResult> AdminListServerSide()
        {
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
        [Route("api/DetailViewmodel/{Id}")]
        public async Task<IActionResult> DetailViewModel(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

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

        [HttpGet]
        [Route("api/ListPagingViewModel")]
        public async Task<IActionResult> ListPagingViewModel(int pageIndex, int pageSize)
        {
            if (pageIndex < 0 || pageSize < 0) return BadRequest();
            try
            {
                var dataList = await service.listPagingViewModel(pageIndex, pageSize);

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
        [Route("api/ListPagingViewModelServiceAndHealthFacility")]
        public async Task<IActionResult> ListPagingViewModelServiceAndHealthFacility(int pageIndex, int pageSize,
            int serviceId, int healthFacilityId)
        {
            if (pageIndex < 0 || pageSize < 0) return BadRequest();
            try
            {
                var dataList = await service.listPagingViewModel(pageIndex, pageSize, serviceId, healthFacilityId);

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
        [Route("api/ListPagingViewModelFilter")]
        public async Task<IActionResult> ListPagingViewModelFilter(int pageIndex, int pageSize, int serviceId,
            int healthFacilityId, string keyword, string district)
        {
            if (pageIndex < 0 || pageSize < 0) return BadRequest();
            try
            {
                var dataList = await service.listPagingViewModel(pageIndex, pageSize, serviceId, healthFacilityId,
                    keyword, district);

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
        public async Task<IActionResult> Add([FromBody] Doctor model)
        {
            if (ModelState.IsValid)
            {
                //1. business logic

                //data validation
                if (model.Name.Length == 0)
                {
                    return BadRequest();
                }

                //2. add new object
                try
                {
                    await service.Add(model);
                    var homedoctorsolutionResponse = HomeDoctorResponse.CREATED(model);
                    return Created("", homedoctorsolutionResponse);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("api/Update")]
        public async Task<IActionResult> Update([FromBody] Doctor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //1. business logic 
                    //2. update object
                    await service.Update(model);
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
        [Route("api/Delete")]
        public async Task<IActionResult> Delete([FromBody] Doctor model)
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
        public async Task<IActionResult> DeletePermanently([FromBody] Doctor model)
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
        public int CountDoctors()
        {
            int result = service.Count();
            return result;
        }

        [HttpGet]
        [Route("api/get6DoctorOutstanding")]
        public async Task<IActionResult> get6DoctorOutstanding()
        {
            try
            {
                var dataList = await service.get6DoctorOutstanding();
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/getBookingDoctor")]
        public async Task<IActionResult> getBookingDoctor()
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                var dataList = await service.ListDoctorBooking(accountId);
                if (dataList == null || dataList.Count == 0)
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

        [HttpPost]
        [Route("api/list-server-side")]
        public async Task<IActionResult> ListServerSide([FromBody] DoctorsDTParameters parameters)
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

        [HttpGet]
        [Route("api/list-doctor")]
        public async Task<IActionResult> ListDoctor()
        {
            try
            {
                var dataList = await service.ListDoctor();
                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound();
                }

                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/list-three-doctor")]
        public async Task<IActionResult> ListThreeDoctorByTimeDesc()
        {
            try
            {
                var listDoctor = await service.ListThreeDoctorByTimeDesc();
                if (listDoctor == null || listDoctor.Count == 0)
                {
                    return NotFound();
                }

                var homeDoctorSolutionResponse = HomeDoctorResponse.SUCCESS(listDoctor.Cast<object>().ToList());
                return Ok(homeDoctorSolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/ListPagingViewModelCount")]
        public async Task<int> listPagingViewModelCount(int serviceId,
            int healthFacilityId, string keyword, string district)
        {
            var dataList = await service.listPagingViewModelCount( serviceId, healthFacilityId, keyword, district);
            
            return dataList;
        }
    }
}