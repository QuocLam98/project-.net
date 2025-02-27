using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HomeDoctorSolution.Controllers.Core;
using HomeDoctorSolution.Helper;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeDoctorSolution.Controllers
{
    public class HomeDoctorController : BaseController
    {
        IAccountService accountService;
        public HomeDoctorController(ICacheHelper cacheHelper, IAccountService _accountService) : base(cacheHelper)
        {
            accountService = _accountService;
        }
        [Route("")]
        [Route("xac-thuc-tai-khoan/{code}/{hash}")]
        public async Task<IActionResult> Index(string code, string hash)
        {
            var accountId = this.GetLoggedInUserId();
            var accObj = await accountService.Detail(accountId);
            ViewBag.IsActivedAccount = 0;
            if (accObj != null)
            {
                ViewBag.Name = accObj.Name;
                ViewBag.Photo = accObj.Photo;
            }
            if (code != null && hash != null)
            {
                var tokenValid = await accountService.CheckKeyValidAndUpdateActiveAccount(code, hash);
                if (tokenValid)
                {
                    ViewBag.IsActivedAccount = 1;
                    return View();
                }
                else
                {
                    return View("~/Views/HomeDoctor/Error404.cshtml");
                }
            }

            return View();
        }
        #region Web
        //[Route("")]
        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {
        //        var accountId = this.GetLoggedInUserId();
        //        if(accountId > 0)
        //        {
        //            var accountDetail = await accountService.Detail(accountId);
        //            if(accountDetail != null)
        //            {
        //                ViewBag.Name = accountDetail.Name;
        //                ViewBag.Photo = accountDetail.Photo;
        //            }
                    
        //        }               
        //        return View();
        //    }
        //    catch(Exception e)
        //    {
        //        return BadRequest();
        //    }
            
        //}
        //Anh em code thì viết ở đây nhé theo đúng vị trí
        #region Hiếu
        [Route("sign-in")]
        [Route("dang-nhap")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("sign-up")]
        [Route("dang-ky")]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("forgot-password")]
        [Route("quen-mat-khau")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Route("change-password")]
        [Route("doi-mat-khau")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("medical-info")]
        [Route("thong-tin-y-te")]
        [Authorize]
        public IActionResult MedicalInfo()
        {
            return View();
        }

        #endregion

        #region Lâm
        [Route("about-us")]
        [Route("ve-chung-toi")]
        public IActionResult AboutUs()
        {
            return View();
        }
        [Route("doctor-detail/{id}")]
        [Route("chi-tiet-bac-si/{id}")]
        public IActionResult DoctorDetail(string id)
        {
            var rawUrl = id.Split('-');
            var accountId = this.GetLoggedInUserId();
            ViewBag.LinkFirst = "/danh-sach-bac-si";
            ViewBag.NameFirst = "Danh sách bác sĩ";
            ViewBag.UrlSecond = "Chi tiết bác sĩ";
            ViewBag.UrlFirst = "Phòng Khám";
            ViewBag.UrlSecond = "Bệnh Viện Mắt Trung Ương";
            ViewBag.IdDoctor = rawUrl[0];
            ViewBag.AccountId = accountId;
            return View();
        }
        [Route("history-booking/{id}")]
        [Route("chi-tiet-lich-su-tu-van/{id}")]
        public IActionResult BookingDetail(int id)
        {
            ViewBag.NameFirst = "Tài khoản";
            ViewBag.NameSecond = "Chi tiết lịch khám";
            ViewBag.BookingId = id;
            return View();
        }

        [Route("list-service")]
        [Route("danh-sach-dich-vu")]
        public IActionResult ListService()
        {
            @ViewBag.Title = "Dịch vụ";
            @ViewBag.Description = "Ở đây, bệnh nhân có thể nhận được các dịch vụ kiểm tra sức khỏe định kỳ, xét nghiệm máu, và chẩn đoán hình ảnh để đảm bảo sự đều đặn trong việc giữ gìn sức khỏe. Ngoài ra, các phòng khám còn cung cấp tư vấn y tế, đưa ra các phương pháp phòng ngừa và giáo dục về lối sống lành mạnh.";
            return View();
        }
        #endregion

        #region Đô
        [Route("list-clinic")]
        [Route("danh-sach-phong-kham")]
        public IActionResult ListClinic()
        {
            @ViewBag.Title = "Cơ sở y tế";
            @ViewBag.Description = "Cơ sở y tế là nơi cung cấp dịch vụ chăm sóc sức khỏe và đặt biệt là nơi mà bệnh nhân có thể tìm kiếm sự chẩn đoán và điều trị từ các chuyên gia y tế. Thông thường, mỗi phòng khám được thiết kế để phục vụ một lĩnh vực cụ thể của y học, từ phòng khám gia đình cho đến các phòng khám chuyên sâu như phòng mắt, nhi khoa, hay phòng khám nha khoa. Phòng khám thường có đội ngũ chuyên gia y tế đa dạng, bao gồm bác sĩ, y tá, và nhân viên y tế khác nhau, tất cả đều hợp tác để đảm bảo việc chăm sóc tốt nhất cho bệnh nhân. Các dịch vụ mà phòng khám cung cấp có thể bao gồm kiểm tra sức khỏe định kỳ, chẩn đoán bệnh lý, xét nghiệm, và điều trị theo đơn thuốc hoặc phẫu thuật nhỏ. Phòng khám thường trang bị các thiết bị y tế hiện đại và công nghệ tiên tiến để hỗ trợ trong quá trình chẩn đoán và điều trị.";
            return View();
        }

        [Route("list-post")]
        [Route("danh-sach-tin-tuc")]
        public IActionResult ListPost()
        {
            ViewBag.LinkFirst = "/danh-sach-tin-tuc";
            ViewBag.NameFirst = "Danh sách tin tức";
            ViewBag.Hide = "d-none";
            ViewBag.Title = "Tin tức";
            //ViewBag.Description = "Tin tức về các sự kiện và cập nhật từ thế giới y tế đóng vai trò quan trọng trong việc thông tin và giáo dục cộng đồng về sức khỏe. Phòng khám thường xuyên cung cấp những thông tin mới nhất về các dịch vụ y tế, phương pháp chẩn đoán tiên tiến, và những tiến triển trong lĩnh vực y học.\r\n\r\nTin tức phòng khám thường đề cập đến các sự kiện quan trọng như các chương trình kiểm tra sức khỏe cộng đồng, chiến dịch tiêm phòng, và các chương trình tư vấn về lối sống lành mạnh. Thông qua việc chia sẻ kiến thức về các vấn đề y tế hiện đại và cách duy trì sức khỏe, tin tức phòng khám giúp tạo ra một cộng đồng thông tin và ý thức về quan trọng của việc chăm sóc sức khỏe.";
            return View();
        }

        [Route("list-product")]
        [Route("danh-sach-san-pham")]
        public IActionResult ListProduct()
        {
            ViewBag.LinkFirst = "/danh-sach-san-pham";
            ViewBag.NameFirst = "Danh sách sản phẩm"; 
            ViewBag.Hide = "d-none";
            return View();
        }

        [Route("consulting-history")]
        [Route("lich-su-tu-van")]
        public IActionResult ConsultingHistory()
        {
            ViewBag.LinkFirst = "/lich-su-tu-van";
            ViewBag.NameFirst = "Lịch sử khám";
            ViewBag.Hide = "d-none";
            return View();
        }

        [Route("list-doctor")]
        [Route("danh-sach-bac-si")]
        public IActionResult ListDoctor()
        {
            ViewBag.LinkFirst = "/danh-sach-bac-si";
            ViewBag.NameFirst = "Danh sách bác sĩ";
            ViewBag.Hide = "d-none";
            ViewBag.accountId = this.GetLoggedInUserId();
            return View();
        }
        [Route("personal-information")]
        [Route("thong-tin-ca-nhan")]
        public IActionResult PersonalInformation()
        {
            var id = this.GetLoggedInUserId();
            ViewBag.id = id;
            return View();
        }
        [Route("delete-account")]
        [Route("xoa-tai-khoan")]
        public IActionResult DeleteAccount()
        {
            var id = this.GetLoggedInUserId();
            ViewBag.id = id;
            return View();
        }
        #endregion

        #region Hoàn


        #endregion

        #region Huy
        /// <summary>
        /// chi tiết phòng khám
        /// </summary>
        /// <returns></returns>
        [Route("chi-tiet-phong-kham/{id}")]
        public IActionResult DetailClinic(string id)
        {
            int ClindId = Convert.ToInt32(new String(id.TakeWhile(Char.IsDigit).ToArray()));
            ViewBag.ClinicId = ClindId;
            return View();
        }
        #endregion

        #region Bách
        [Route("chi-tiet-tin-tuc/{Id}")]
        [Route("chinh-sach-bao-mat")]
        public IActionResult PostDetail(int Id)
        {
            if (Id == 0)
            {
                Id = 1000001;
            }
            ViewBag.Id = Id;
            return View();
        }

        #endregion

        #region Cường
        [Route("chi-tiet-san-pham/{id}")]
        public IActionResult ProductDetail(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.NameFirst = "Chi tiết sản phẩm";
            return View();
        }

        [Route("tao-don-hang")]
        [Route("don-hang/{id}")]
        public IActionResult OrderDetail(int id)
        {
            ViewBag.NameFirst = "Đơn hàng";
            ViewBag.NameSecond = "Chi tiết đơn hàng";
            ViewBag.OrderId = id;
            return View();
        }
        [Route("chi-tiet-dich-vu/{id}")]
        public IActionResult ServiceDetail(int id)
        {
            ViewBag.NameFirst = "Chi tiết dịch vụ";
            ViewBag.ServiceId = id;
            return View();
        }
        [Route("tin-nhan")]
        [Route("tin-nhan/{targetAccountId}")]

        public IActionResult Message(int? targetAccountId)
        {
            ViewBag.targetAccountId = 0;
            if (targetAccountId != null)
            {
                ViewBag.targetAccountId = targetAccountId;
            }
            ViewBag.NameFirst = "Tin nhắn";
            return View();
        }
        #endregion
        #endregion

        [Route("phieu-danh-gia-sau-buoi-tu-van/{bookingId}")]
        [Route("app/phieu-danh-gia-sau-buoi-tu-van/{bookingId}")]
        public IActionResult BookingDetailConsultant(int bookingId)
        {
            var roleId = this.GetLoggedInRoleId();
            bool checkRole = false;
            if (roleId == SystemConstant.ROLE_USER)
            {
                checkRole = true;
            }
            ViewBag.CheckRole = checkRole;
            return View();
        }

        #region Web App
        [Route("app/chi-tiet-tin-tuc/{Id}")]
        [Route("app/chinh-sach-bao-mat")]
        public IActionResult PostDetailApp(int Id )
        {
            if (Id == 0)
            {
                Id = 1000001;
            }
            ViewBag.Id = Id;
            return View();
        }
        [Route("app/chi-tiet-nguoi-dung")]
        async public Task<IActionResult> ShowCardInforWV()
        {
            ViewBag.AccountId = this.GetLoggedInUserId();
            var accountId = this.GetLoggedInUserId();
            var accObj = await accountService.Detail(accountId);
            if (accObj != null)
            {
                ViewBag.Name = accObj.Name;
                ViewBag.Photo = accObj.Photo;
            }
            return View();
        }
        #endregion
        [Route("order-history")]
        [Route("lich-su-don-hang")]
        public IActionResult OrderHistory()
        {
            ViewBag.LinkFirst = "/lich-su-don-hang";
            ViewBag.NameFirst = "Lịch sử đơn hàng";
            ViewBag.Hide = "d-none";
            return View();
        }
        /// <summary>
        /// Author: NickyTran
        /// Created: 21/06/2024
        /// Description: Trang chủ xét nghiệm
        /// </summary>
        /// <returns></returns>
        [Route("xet-nghiem")]
        public IActionResult TestAtHome()
        {
            ViewBag.LinkFirst = "/xet-nghiem";
            ViewBag.NameFirst = "Xét nghiệm";
            ViewBag.NameSecond = "Giới thiệu";
            return View();
        }

        [Route("dat-lich-xet-nghiem")]
        public IActionResult BookingTest(string? serviceId)
        {
            ViewBag.LinkFirst = "/dat-lich-xet-nghiem";
            ViewBag.NameFirst = "Xét nghiệm";
            ViewBag.NameSecond = "Đặt lịch";
            ViewBag.ServiceId = serviceId;
            return View();
        }

        [Route("lich-su-xet-nghiem")]
        public IActionResult BookingTestHistory()
        {
            ViewBag.LinkFirst = "/lich-su-xet-nghiem";
            ViewBag.NameFirst = "Xét nghiệm";
            ViewBag.NameSecond = "Lịch sử xét nghiệm";
            return View();
        }

        [Route("danh-sach-xet-nghiem")]
        public IActionResult ListTest()
        {
            ViewBag.LinkFirst = "/danh-sach-xet-nghiem";
            ViewBag.NameFirst = "Xét nghiệm";
            ViewBag.NameSecond = "Danh sách xét nghiệm";
            return View();
        }
    }
}
