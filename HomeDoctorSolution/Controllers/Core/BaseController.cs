using HomeDoctorSolution.Helper;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Services;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;

namespace HomeDoctorSolution.Controllers.Core
{
    public class BaseController : Controller
    {

        private readonly ICacheHelper _cacheHelper;
        public static string SystemURL = "";
        public BaseController(ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ServerURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/";
            SystemURL = ServerURL;
            ViewBag.SystemURL = ServerURL;
            string RequestedURL = filterContext.HttpContext.Request.Path.ToString().ToLower();
            //VALIDATE REQUEST
            int AccountId = this.GetLoggedInUserId();
            int RoleId = this.GetLoggedInRoleId();
            #region Valid request
            bool IsValidRequest = true;
            if (RequestedURL.Contains("api"))
            {
                var listRights = _cacheHelper.GetRights();
                var listRoleRights = _cacheHelper.GetRoleRights();
                if(RoleId == SystemConstant.ROLE_TEENAGER_MOD)
                {
                    listRoleRights = _cacheHelper.GetRoleRightsTeenagerMod();
                }
                else if(RoleId == SystemConstant.ROLE_ADMIN_SCHOOL)
                {
                    listRoleRights = _cacheHelper.GetRoleRightsAdminSchool();
                }
                if (!(listRoleRights != null))
                {
                    listRoleRights = _cacheHelper.GetRoleRightsNotLogin();
                }
                IsValidRequest = SecurityUtil.AuthorizeRequestByToken(RequestedURL, listRoleRights, listRights, RoleId);
                #region Log lại truy cập API
                //user agent
                ActivityLog al = new ActivityLog();
                al.EntityCode = "API";
                al.AccountId = AccountId;
                al.Description = IsValidRequest.ToString();
                al.UserAgent = Request.Headers["User-Agent"].ToString();
                //ip address
                var remoteIpAddress = IPUtil.IPAddress(HttpContext.Request);
                al.IpAddress = remoteIpAddress.ToString();
                //url
                //model.Url = Request.GetDisplayUrl();
                al.Url = RequestedURL;
                al.UrlSource = Request.Headers["Referer"].ToString();
                al.CreatedTime = DateTime.Now;
                //repositoryActivityLog = new ActivityLogRepository();
                //repositoryActivityLog.Add(al); 
                try
                {
                    string _filePath = "log/api/api.txt";
                    using (StreamWriter writer = System.IO.File.AppendText(_filePath))
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(al));
                    }
                    //File.WriteAllText(_filePath, JsonConvert.SerializeObject(al));
                }
                catch (Exception)
                {

                }
                #endregion
            }
            else if (RequestedURL.Contains("admin"))
            {
                var access_token = HttpContext.Request.Cookies["Authorization"];
                // Decode the token.
                if (access_token != null && access_token != "")
                {
                    access_token = access_token.Replace("Bearer", "").Trim();
                    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(access_token);
                    IEnumerable<Claim> claims = jwtSecurityToken.Claims;
                    filterContext.HttpContext.User.Identities.FirstOrDefault().AddClaims(claims);
                }
                RoleId = this.GetLoggedInRoleId();
                AccountId = this.GetLoggedInUserId();
                ViewBag.AccountId = AccountId;
                var listMenu = _cacheHelper.GetMenu();
                var listRoleMenus = _cacheHelper.GetRoleMenu();
                List<RoleMenu> listRoleMenu = new List<RoleMenu>();
                if (listRoleMenus != null)
                {
                    listRoleMenu = listRoleMenus;
                }
                var listRoleMenuByRoleId = listRoleMenu.Where(x => x.RoleId == RoleId).ToList();
                List<Menu> listMenuByRole = new List<Menu>();
                for (global::System.Int32 i = 0; i < listRoleMenuByRoleId.Count; i++)
                {
                    listMenuByRole.AddRange(_cacheHelper.GetMenu().Where(menu => menu.Id == listRoleMenuByRoleId[i].MenuId).ToList());
                }
                _cacheHelper.SetMenuByRole(listMenuByRole);
                IsValidRequest = SecurityUtil.AuthorizeAdminPageByToken(RequestedURL, listRoleMenuByRoleId, listMenu, RoleId);
                ViewBag.IsValidAdminPage = IsValidRequest;
                var breadcrumbObj = listMenu.Find(x => x.Url.ToLower().Replace("/","") == RequestedURL.ToLower().Replace("/", ""));
                if(breadcrumbObj != null) ViewBag.BreadcrumbName = breadcrumbObj.Name;
                else ViewBag.BreadcrumbName = "";
            }
            else
            {
                var access_token = HttpContext.Request.Cookies["Authorization"];
                // Decode the token.
                if (access_token != null && access_token != "")
                {
                    access_token = access_token.Replace("Bearer", "").Trim();
                    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(access_token);
                    IEnumerable<Claim> claims = jwtSecurityToken.Claims;
                    filterContext.HttpContext.User.Identities.FirstOrDefault().AddClaims(claims);
                    //Check time token
                    if(jwtSecurityToken.ValidTo.AddHours(7) < DateTime.Now)
                    {
                        HttpContext.Response.Cookies.Delete("Authorization");
                        HttpContext.Response.Redirect("/sign-in");
                    }
                }
                RoleId = this.GetLoggedInRoleId();
                AccountId = this.GetLoggedInUserId();
                //ViewBag.IsActivedAccount = 1;
                if ((RequestedURL.Contains("/dang-nhap") || RequestedURL.Contains("/sign-in") || RequestedURL.Contains("/dang-ky") || RequestedURL.Contains("/sign-up")) && AccountId != 0)
                {
                    HttpContext.Response.Redirect("/");
                }
                ViewBag.AccountId = AccountId;
                ViewBag.RoleId = RoleId;
            }
            //Điều hướng sang 401 nếu không valid
            if (IsValidRequest == false)
            {
                if (ServerURL.Contains("localhost"))
                {
                    //DEV Mode
                    if (!(RequestedURL.Contains("action")))
                    {
                        filterContext.Result = Unauthorized();
                        //IsValidRequest = true;
                        return;
                    }
                }
                else
                {
                    if (!(RequestedURL.Contains("action")))
                    {
                        filterContext.Result = Unauthorized();
                        //IsValidRequest = true;
                        return;
                    }
                }
            }
            #endregion
            #region Setting cache
            var systemConfigs = _cacheHelper.GetSystemConfig();
            ViewBag.SystemConfigs = systemConfigs;
            //Lấy server URL động
            //ViewBag.SystemConfigs["HOMEPAGE_URL"].Description = ServerURL;
            SystemConstant.DEFAULT_URL = ServerURL;
            ViewBag.ServerURL = ServerURL;

            //Do something with LanguageConfig
            var languageConfigs = _cacheHelper.GetLanguageConfig();
            ViewBag.LanguageConfigs = languageConfigs;

            //Menu SystemAdmin
            var MenuSystemAdmin = _cacheHelper.GetMenuSystemAdmin();
            ViewBag.MenuSystemAdmin = MenuSystemAdmin;

            //Menu ALL
            var MenuAll = _cacheHelper.GetMenu();
            ViewBag.MenuAll = MenuAll;

            //Menu ByRole
            var MenuByRole = _cacheHelper.GetMenuByRole();
            MenuByRole = MenuByRole != null ? MenuByRole.OrderBy(x => x.Priority).ToList() : MenuByRole;
            ViewBag.MenuByRole = MenuByRole;

            CultureInfo ci = new CultureInfo("en-Us");

            ViewBag.DateTimeNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt", ci);
            #endregion
        }
    }
}
