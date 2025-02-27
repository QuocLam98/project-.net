

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
using HomeDoctorSolution.Constants;
using HomeDoctorSolution.Util.Entities;
using HomeDoctorSolution.Models.ModelDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using HomeDoctorSolution.Helper;
using HomeDoctorSolution.Models.ViewModel;
using HomeDoctor.Models.ViewModels;
using System.Net;
using HomeDoctor.Models.ModelDTO;
using Microsoft.EntityFrameworkCore;

namespace HomeDoctorSolution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        IAccountService service;
        public AccountController(IAccountService _service, ICacheHelper cacheHelper) : base(cacheHelper)
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
        [Route("admin/info-profile")]
        public async Task<IActionResult> AdminProfile()
        {
            ViewBag.RoleId = this.GetLoggedInRoleId();
            if (this.GetLoggedInRoleId() == SystemConstant.ROLE_DOCTOR)
            {
                ViewBag.Url = "booking/lich-tu-van";
            }
            ViewBag.BreadcrumbName = "Thông tin tài khoản";
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
        [Route("api/ListContact")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListContact(int pageIndex, int pageSize)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                var dataList = await service.ListContact(accountId, pageIndex, pageSize);
                if (dataList == null)
                {
                    return NotFound();
                }
                var homedoctorsolutionResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(homedoctorsolutionResponse);
            }
            catch (Exception ex)
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
        [Route("api/Detail-Account/{Id}")]
        public async Task<IActionResult> DetailAccount(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            try
            {
                var dataList = await service.DetailAccount(Id);
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
        [Route("api/Search")]
        public async Task<IActionResult> Search(SearchVM search)
        {
            try
            {
                string keyword = "";
                if (search.searchString != null)
                {
                    keyword = search.searchString.ToLower().Trim();
                }
                var dataList = await service.Search(keyword);
                //if (dataList == null || dataList.Count == 0)
                //{
                //    return NotFound();
                //}
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
        public async Task<IActionResult> Add([FromBody] Account model)
        {
            if (ModelState.IsValid)
            {
                //1. business logic
                var checkUserName = await service.CheckUserNameExist(0, model.Username);
                if (checkUserName)
                {
                    var existUserNameObj = HomeDoctorResponse.UsernameExist(checkUserName);
                    return Ok(existUserNameObj);
                }
                var erors = await service.ValidAccount(model.Email, model.Phone, model.IdCardNumber, 0);
                if (erors.Count > 0)
                {
                    var exist = HomeDoctorResponse.BAD_REQUEST(erors);
                    return Ok(exist);
                }
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
        public async Task<IActionResult> Update([FromBody] Account model)
        {
            if (ModelState.IsValid)
            {
                var checkUserName = await service.CheckUserNameExist(model.Id, model.Username);
                if (checkUserName)
                {
                    var existUserNameObj = HomeDoctorResponse.UsernameExist(checkUserName);
                    return Ok(existUserNameObj);
                }
                var erors = await service.ValidAccount(model.Email, model.Phone, model.IdCardNumber, model.Id);
                if (erors.Count > 0)
                {
                    var exist = HomeDoctorResponse.BAD_REQUEST(erors);
                    return Ok(exist);
                }
                try
                {
                    //1. business logic 
                    //2. update object
                    await service.UpdateAccount(model);
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
        public async Task<IActionResult> Delete([FromBody] Account model)
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
        [Route("api/Delete-mobile")]
        public async Task<IActionResult> Delete()
        {
            var model = await service.Detail(this.GetLoggedInUserId());
            if (ModelState.IsValid)
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
            return BadRequest();
        }
        [HttpPost]
        [Route("api/DeletePermanently")]
        public async Task<IActionResult> DeletePermanently([FromBody] Account model)
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
        public int CountAccount()
        {
            int result = service.Count();
            return result;
        }
        [HttpPost]
        [Route("api/list-server-side")]
        public async Task<IActionResult> ListServerSide([FromBody] AccountDTParameters parameters)
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

        //created: JinDo
        //date: 15/12/2023
        //description: Dang Nhap
        [Route("api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAction([FromBody] Account model)
        {
            try
            {
                var response = await service.Login(model);
                if (response.status == ResponseConst.STATUS_200)
                {
                    HttpContext.Response.Cookies.Append("Authorization", ((SignInResponse)response.resources).AccessToken);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                string errror = e.Message;
                return BadRequest();
            }
        }
        [HttpPost("api/sign-in-google")]
        public async Task<IActionResult> SignInGoogle([FromBody] SignInWithSocialNetworkDTO obj)
        {
            try
            {
                var res = await service.SignInWithGoole(obj);
                var token = "";
                if (res.data != null)
                {
                    token = (res.data.First() as LoginAccountModel).Token;
                    if (res.status == ResponseConst.STATUS_200)
                    {
                        HttpContext.Response.Cookies.Append("Authorization", token);
                    }
                }
                else
                {
                    token = (res.resources as LoginAccountModel).Token;
                    if (res.status == ResponseConst.STATUS_200)
                    {
                        HttpContext.Response.Cookies.Append("Authorization", token);
                    }
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("api/sign-in-facebook")]
        public async Task<IActionResult> SignInFacebook([FromBody] SignInWithSocialNetworkDTO obj)
        {
            try
            {
                var res = await service.SignInWithFacebook(obj);
                var token = "";
                if (res.data != null)
                {
                    token = (res.data.First() as LoginAccountModel).Token;
                    if (res.status == ResponseConst.STATUS_200)
                    {
                        HttpContext.Response.Cookies.Append("Authorization", token);
                    }
                }
                else
                {
                    token = (res.resources as LoginAccountModel).Token;
                    if (res.status == ResponseConst.STATUS_200)
                    {
                        HttpContext.Response.Cookies.Append("Authorization", token);
                    }
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("api/sign-in-apple")]
        public async Task<IActionResult> SignInApple([FromBody] SignInWithSocialNetworkDTO obj)
        {
            try
            {
                var res = await service.SignInWithApple(obj);
                var token = "";
                if (res.data != null)
                {
                    token = (res.data.First() as LoginAccountModel).Token;
                    if (res.status == ResponseConst.STATUS_200)
                    {
                        HttpContext.Response.Cookies.Append("Authorization", token);
                    }
                }
                else
                {
                    token = (res.resources as LoginAccountModel).Token;
                    if (res.status == ResponseConst.STATUS_200)
                    {
                        HttpContext.Response.Cookies.Append("Authorization", token);
                    }
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("api/logout")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                HttpContext.Response.Cookies.Append("Authorization", "");
                var happySmileResponse = HomeDoctorResponse.SUCCESS();
                return Ok(happySmileResponse);
            }
            catch (Exception e)
            {
                string errror = e.Message;
                return BadRequest();
            }
        }
        //end Dang Nhap


        /// <summary>
        /// Author: TrungHieuTr
        /// Description: Đăng ký tài khoản 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] InsertAccountDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var happysmilesolutionResponse = await service.Register(model);
                    return Created("", happysmilesolutionResponse);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        //end Đăng ký

        [Route("api/forgot-password")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string value)
        {
            try
            {
                //Xử lý logic
                int checkTypeValue;
                bool isNumber = int.TryParse(value, out checkTypeValue);
                if (isNumber)
                {

                }
                else
                {
                    if (!(HappySUtil.IsValidEmail(value)))
                    {
                        var happySmileResponse = HomeDoctorResponse.Faild();
                        happySmileResponse.message = "EMAIL_INCORRECT";
                        return Ok(happySmileResponse);
                    }
                    else
                    {
                        var happySmileResponse = await service.ForgotPassword(value);
                        return Ok(happySmileResponse);
                    }
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                string errror = e.Message;
                return BadRequest();
            }
        }
        [Route("api/forgot-password-by-user")]
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordByUser(string value)
        {
            try
            {
                //Xử lý logic
                int checkTypeValue;
                bool isNumber = int.TryParse(value, out checkTypeValue);
                if (isNumber)
                {

                }
                else
                {
                    if (!(HappySUtil.IsValidEmail(value)))
                    {
                        var happySmileResponse = HomeDoctorResponse.Faild();
                        happySmileResponse.message = "EMAIL_INCORRECT";
                        return Ok(happySmileResponse);
                    }
                    else
                    {
                        var homeDoctorResponse = await service.ForgotPasswordEndUser(value);
                        return Ok(homeDoctorResponse);
                    }
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                string errror = e.Message;
                return BadRequest();
            }
        }
        [HttpPost("api/reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] KeyResetForgotPasswordEmailViewModel keymodel)
        {
            if (ModelState.IsValid)
            {
                var checkValidKey = await service.CheckKeyValid(keymodel.Key, keymodel.Hash);
                if (!checkValidKey)
                {
                    keymodel.KeyUpToDate = false;
                    return BadRequest();
                }
                var hashed_password = SecurityUtil.ComputeSha256Hash(keymodel.Password);
                await service.ResetNewPassword(keymodel.Key, keymodel.Hash, hashed_password);
                return Ok(HomeDoctorResponse.SUCCESS());
            }
            else
            {
                return BadRequest(keymodel);
            }
        }
        [Route("api/change-password")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO obj)
        {
            try
            {
                //Xử lý loigc
                var accountId = this.GetLoggedInUserId();
                var response = await service.ChangePassword(obj, accountId);
                return Ok(response);
            }
            catch (Exception e)
            {
                string errror = e.Message;
                return BadRequest();
            }
        }
        [Route("api/change-userDetail")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] UpdateAdminAccountDTO obj)
        {
            try
            {
                //Xử lý loigc
                var accountId = this.GetLoggedInUserId();
                var response = await service.UpdateProfile(obj, accountId);
                return Ok(response);
            }
            catch (Exception e)
            {
                string errror = e.Message;
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/change-password-by-forgot")]
        public async Task<IActionResult> ChangePasswordByForgot([FromBody] ForgotPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var happysmilesolutionResponse = await service.ChangePasswordByForgot(model);
                    return Created("", happysmilesolutionResponse);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        /// <summary> 
        /// Author: HuyDQ
        /// Description: list danh sách theo roleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ListByRoleId/{roleId}")]
        public async Task<IActionResult> ListByRoleId(int roleId)
        {
            try
            {
                var dataList = await service.ListByRoleId(roleId);
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
        [Route("api/ChangeAvatar")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangeAvatar([FromForm(Name = "file")] IFormFile files)
        {
            try
            {
                int AccountId = this.GetLoggedInUserId();
                var obj = new Account();
                obj.Id = AccountId;
                obj.Photo = files.FileName;
                var homedoctorResponse = await service.ChangeAvatar(obj, files);
                return Ok(homedoctorResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route("api/profile")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> GetProfileById()
        {
            try
            {
                int AccountId = this.GetLoggedInUserId();
                if (AccountId == 0)
                {
                    return BadRequest();
                }
                var dataList = await service.GetProfile(AccountId);
                if (dataList == null)
                {
                    return NotFound();
                }
                var homedoctorResponse = HomeDoctorResponse.SUCCESS(dataList);
                return Ok(homedoctorResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}


