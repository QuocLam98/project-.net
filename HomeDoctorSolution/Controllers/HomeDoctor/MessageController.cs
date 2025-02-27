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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using HomeDoctorSolution.Helper;

namespace HomeDoctorSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : BaseController
    {
        IMessageService service;

        public MessageController(IMessageService _service, ICacheHelper cacheHelper) : base(cacheHelper)
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
        public async Task<IActionResult> Add([FromBody] Message model)
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
        public async Task<IActionResult> Update([FromBody] Message model)
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
        public async Task<IActionResult> Delete([FromBody] Message model)
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
        public async Task<IActionResult> DeletePermanently([FromBody] Message model)
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
        public int CountMessage()
        {
            int result = service.Count();
            return result;
        }

        [HttpPost]
        [Route("api/list-server-side")]
        public async Task<IActionResult> ListServerSide([FromBody] MessageDTParameters parameters)
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
        [Route("api/SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] Message model)
        {
            try
            {
                await service.SendMessage(model);
                var homedoctorsolutionResponse = HomeDoctorResponse.CREATED(model);
                return Created("", homedoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        [HttpGet]
        [Route("api/LoadMessage")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LoadMessage(int accountId, int pageIndex, int pageSize, string roomName)
        {
            try
            {
                if (roomName == "undefined")
                {
                    string defaultRoomName = $"{this.GetLoggedInUserId()}-{accountId}";
                    var data = await service.LoadMessage(accountId, pageIndex, pageSize, defaultRoomName);
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(data.Cast<object>().ToList());

                    if (data.Any())
                    {
                        return Ok(homedoctorsolutionResponse);
                    }

                    string alternativeRoomName = $"{accountId}-{this.GetLoggedInUserId()}";
                    var alternativeData =
                        await service.LoadMessage(accountId, pageIndex, pageSize, alternativeRoomName);
                    var alternativeResponse = HomeDoctorResponse.SUCCESS(alternativeData.Cast<object>().ToList());

                    if (alternativeData.Any())
                    {
                        return Ok(alternativeResponse);
                    }
                }
                else
                {
                    var data = await service.LoadMessage(accountId, pageIndex, pageSize, roomName);
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(data.Cast<object>().ToList());

                    if (data.Any())
                    {
                        return Ok(homedoctorsolutionResponse);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }

            return NotFound();
        }


        [HttpGet]
        [Route("api/ReadedMessage")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ReadedMessage(int accountId, string roomName)
        {
            try
            {
                if (roomName == "undefined")
                {
                    string defaultRoomName = $"{this.GetLoggedInUserId()}-{accountId}";
                    await service.ReadedMessage(accountId, defaultRoomName);
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS();
                    if (homedoctorsolutionResponse != null)
                    {
                        return Ok(homedoctorsolutionResponse);
                    }

                    string alternativeRoomName = $"{accountId}-{this.GetLoggedInUserId()}";
                    await service.ReadedMessage(accountId, alternativeRoomName);
                    var alternativeResponse = HomeDoctorResponse.SUCCESS();
                    if (alternativeResponse != null)
                    {
                        return Ok(alternativeResponse);
                    }
                }
                else
                {
                    await service.ReadedMessage(accountId, roomName);
                    var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS();
                    if (homedoctorsolutionResponse != null)
                    {
                        return Ok(homedoctorsolutionResponse);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }

            return NotFound();
        }


        [HttpGet]
        [Route("api/ListContact")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListContact(int pageIndex, int pageSize)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                var data = await service.ListContact(accountId, pageIndex, pageSize);
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(data.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        [HttpGet]
        [Route("api/LoadUnread")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LoadUnread(int pageIndex, int pageSize)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                var data = await service.LoadUnread(accountId, pageIndex, pageSize);
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(data.Cast<object>().ToList());
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }
    }
}