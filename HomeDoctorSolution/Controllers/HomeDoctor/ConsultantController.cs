
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
    public class ConsultantController : BaseController
    {
        IConsultantService service;

        public ConsultantController(ICacheHelper cacheHelper, IConsultantService _service) : base(cacheHelper)
        {
            service = _service;
        }
        [HttpGet]
        [Route("api/Consultant/{Id}")]
        [Route("api/DetailConsultant/{Id}")]
        public async Task<IActionResult> DetailConsultantId(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var dataList = await service.DetailConsultant(id);
                //if (dataList == null)
                //{
                //    return NotFound();
                //}
                var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(HomeDoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //[Route("admin/DetailConsultant/{Id}")]
        //[Route("DetailConsultant/{Id}")]
        [Route("Share/DetailConsultant/{Id}")]
        [Route("phieu-danh-gia-sau-tu-van/{Id}")]
        public async Task<IActionResult> DetailConsultant(int id)
        {
            //var accountId = this.GetLoggedInUserId();
            //ViewBag.accountId = accountId;
            //var checkPermission = await service.CheckAccountViewConsultant(id, accountId);
            //if (checkPermission ==  true)
            //{
            //    return View();
            //}
            //else
            //{
            //    return View("~/Views/HappyS/Error404.cshtml");
            //}
            return View();
        }

        [HttpPost]
        [Route("api/Add")]
        public async Task<IActionResult> Add([FromBody] Consultant model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await service.Add(model);
                    var HomeDoctorsolutionResponse = HomeDoctorResponse.CREATED(model);
                    return Created("", HomeDoctorsolutionResponse);
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
        public async Task<IActionResult> Update([FromBody] Consultant model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //1. business logic 
                    //2. update object
                    await service.Update(model);
                    var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS(model);
                    return Ok(HomeDoctorsolutionResponse);
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

        [HttpGet]
        [Route("api/Detail/{Id}")]
        public async Task<IActionResult> Detail(int Id)
        {
            try
            {
                var dataList = await service.Detail(Id);
                if (dataList == null)
                {
                    return Ok(HomeDoctorResponse.BAD_REQUEST());
                }
                var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(HomeDoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/delete/{Id}")]
        public async Task<IActionResult> DeletePermanently(int Id)
        {
            try
            {
                await service.DeletePermanently(Id);
                var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS();
                return Ok(HomeDoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/DetailConsultantByBookingId/{Id}")]
        public async Task<IActionResult> DetailConsultantByBookingId(int Id)
        {
            try
            {
                var dataList = await service.DetailConsultantByBookingId(Id);
                if (dataList == null)
                {
                    return Ok(HomeDoctorResponse.BAD_REQUEST());
                }
                var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(HomeDoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/list-by-bookingId/{bookingId}")]
        public async Task<IActionResult> ListByBookingId(int bookingId)
        {
            try
            {
                var dataList = await service.ListByBookingId(bookingId);
                if (dataList == null)
                {
                    return Ok(HomeDoctorResponse.BAD_REQUEST());
                }
                var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(HomeDoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/ListReviewCounselor")]
        public async Task<IActionResult> ListReviewCounselor(int counselorId, int pageIndex, int pageSize)
        {
            try
            {
                var dataList = await service.ListReviewConsultant(counselorId, pageIndex, pageSize);
                if (dataList == null)
                {
                    return Ok(HomeDoctorResponse.BAD_REQUEST());
                }
                var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(HomeDoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/send-email-consultant-to-account")]
        public async Task<IActionResult> SendMailConsultantToAccount(int accountId, int counselorId, int bookingId)
        {
            try
            {
                var dataList = await service.SendEmailConsultantToAccount(accountId, counselorId, bookingId);
                if (dataList == null)
                {
                    return Ok(HomeDoctorResponse.BAD_REQUEST());
                }
                var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(HomeDoctorsolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/ListConsultantByAccountId/{accountId}")]
        public async Task<IActionResult> ListConsultantByAccountId(int accountId)
        {
            try
            {
                var dataList = await service.ListConsultantByAccountId(accountId);
                var HomeDoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(HomeDoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
