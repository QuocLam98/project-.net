using HomeDoctorSolution.Controllers.Core;
using HomeDoctorSolution.Helper;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeDoctorSolution.Controllers.Admin
{
    [Route("admin")]
    [ApiController]
    public class AdminController : BaseController
    {
        IAccountService accountService;
        IBookingService bookingService;
        public AdminController(IAccountService _accountService,
            IBookingService _bookingService,
            ICacheHelper cacheHelper) : base(cacheHelper)
        {
            accountService = _accountService;
            bookingService = _bookingService;
        }
        [Route("action/sign-in")]
        public IActionResult Login()
        {
            ViewBag.admin = SystemConstant.ROLE_SUPER_ADMIN;
            ViewBag.doctor = SystemConstant.ROLE_DOCTOR;
            ViewBag.censor = SystemConstant.ROLE_CENSOR;
            return View();
        }

        [Route("action/forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet("action/doi-mat-khau/{code}/{hash}")]
        public async Task<IActionResult> ResetPasswordAdmin(string code, string hash)
        {
            try
            {
                //var tokenValid = await accountService.CheckKeyValid(code, hash);
                //ViewBag.tokenValid = tokenValid;
                //ViewBag.Value = code;
                //ViewBag.Hash = hash;
                //if (tokenValid)
                //{
                //    return View("~/Views/Admin/ResetPasswordAdmin.cshtml");
                //}
                //else
                //{
                //    return View();
                //}
                  return View();
          }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        [Route("dashboard")]
        public async Task<IActionResult> DashBoard()
        {
            //var reportBooking = await bookingService.BookingQuantityReport();
            //ViewBag.ReportBooking = reportBooking.Count > 0 ? reportBooking[0] : null;
            return View();
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var roleId = this.GetLoggedInRoleId();
            if(roleId == SystemConstant.ROLE_SYSTEM_ADMIN)
            {
                //return RedirectToAction("DashBoard");
                return Redirect("booking/admin/list");
            }
            else if(roleId == SystemConstant.ROLE_DOCTOR)
            {
                return Redirect("booking/lich-tu-van");
            }
            else if (roleId == SystemConstant.ROLE_TEENAGER_MOD)
            {
                return Redirect("forumPost/admin/list");
            }
            else if (roleId == SystemConstant.ROLE_ADMIN_SCHOOL)
            {
                return Redirect("account/admin/list");
            }else if (roleId == SystemConstant.ROLE_CENSOR)
            {
                return Redirect("booking/admin/list");
            }
            else
            {
                return Redirect("admin/action/sign-in");
            }
        }
        [HttpGet]
        [Route("user-profile")]
        public async Task<IActionResult> UserProfile()
        {
            ////Load data userprofile
            //var account = await accountService.GetProfile(this.GetLoggedInUserId());
            //ViewBag.Account = account != null ? account : null;
            //ViewBag.AccountJson = account != null ? JsonConvert.SerializeObject(account) : null;

            return View();
        }
    }
}
