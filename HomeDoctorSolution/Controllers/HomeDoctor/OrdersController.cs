using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeDoctor.Models.ViewModels;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Controllers.Core;
using HomeDoctorSolution.Helper;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace HomeDoctorSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        IOrdersService service;

        public OrdersController(IOrdersService _service, ICacheHelper cacheHelper) : base(cacheHelper)
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
        public async Task<IActionResult> Add([FromBody] Order model)
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
        public async Task<IActionResult> Update([FromBody] Order model)
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

        [HttpPost]
        [Route("api/Delete")]
        public async Task<IActionResult> Delete([FromBody] Order model)
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
        public async Task<IActionResult> DeletePermanently([FromBody] Order model)
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
        public int CountOrders()
        {
            int result = service.Count();
            return result;
        }

        [HttpPost]
        [Route("api/list-server-side")]
        public async Task<IActionResult> ListServerSide([FromBody] OrdersDTParameters parameters)
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
        [Route("api/DetailById/{Id}")]
        public async Task<IActionResult> DetailById(int? Id)
        {
            try
            {
                var dataList = await service.DetailById(Id);
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

        [HttpPost]
        [Route("api/AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] OrdersViewModel model)
        {
            try
            {
                await service.AddOrder(model);
                var homedoctorsolutionResponse = HomeDoctorResponse.CREATED(model);
                return Created("", homedoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrdersViewModel model)
        {
            try
            {
                await service.UpdateOrder(model);
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(model);
                return Created("", homedoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/ListOrdersById/{id}&&pageIndex={pageIndex}&&pageSize={pageSize}")]
        public async Task<IActionResult> ListById(int? id, int pageIndex, int pageSize)
        {
            try
            {
                var ListOrders = await service.ListById(id, pageIndex, pageSize);
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(ListOrders);
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // [HttpPost]
        // [Route("api/Delete/{id}")]
        // public async Task<IActionResult> DeleteById(int id)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             //1. business logic
        //             await service.DeleteById(id);
        //             var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS();
        //             return Ok(homedoctorsolutionResponse);
        //         }
        //         catch (Exception ex)
        //         {
        //             if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
        //             {
        //                 return NotFound();
        //             }
        //             return BadRequest();
        //         }
        //     }
        //     return BadRequest();
        // }
        [HttpGet]
        [Route("api/list-order-by-orderStatusId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListBookingByBookingStatusId(int orderStatusId, int pageIndex, int pageSize)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                var dataList = await service.ListOrderByOrderStatusId(accountId, orderStatusId, pageIndex, pageSize);
                var homeDoctorSolutionResponse = HomeDoctorResponse.SUCCESS(dataList.Cast<object>().ToList());
                return Ok(homeDoctorSolutionResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/count-order-by-accountId")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<int> CountListOrderByAccountId(int orderstatusId)
        {
            var accountId = this.GetLoggedInUserId();
            int result = await service.CountListOrderByAccountId(accountId, orderstatusId);
            return result;
        }
        
        [HttpGet]
        [Route("api/count-orders")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CountListOrders()
        {
            var accountId = this.GetLoggedInUserId();
            List<OrderCountViewModel> result = await service.CountListOrders(accountId);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/CancelOrderById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CancelById([FromBody] OrdersViewModel model)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                await service.DeleteById(model.Id, accountId);
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS();
                return Ok(homedoctorsolutionResponse);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}