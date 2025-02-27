using HomeDoctorSolution.Repository.Interfaces;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Models.ViewModel;
using HomeDoctorSolution.Models.ViewModels;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util.Email;
using HomeDoctorSolution.Controllers.Core;
using HomeDoctorSolution.Util;

namespace HomeDoctorSolution.Services
{
    public class ConsultantService : IConsultantService
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        IConsultantRepository consultantRepository;
        IAccountRepository accountRepository;
        IConfiguration config;
        INotificationService notificationService;
        public ConsultantService(
            IConsultantRepository _consultantRepository,
            IBookingRepository _bookingRepository,
            IAccountRepository _accountRepository,
            INotificationService _notificationService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment env
            )
        {
            consultantRepository = _consultantRepository;
            accountRepository = _accountRepository;
            notificationService = _notificationService;
            _env = env;
        }

        public async Task Add(Consultant obj)
        {
            obj.CreatedTime = DateTime.Now;
            await consultantRepository.Add(obj);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task Delete(Consultant obj)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await consultantRepository.DeletePermanently(id);
        }

        public async Task<Consultant> Detail(int? id)
        {
            return await consultantRepository.Detail(id);
        }

        public async Task<Consultant> DetailConsultantByBookingId(int id)
        {
            return await consultantRepository.DetailConsultantByBookingId(id);
        }

        public async Task<List<Consultant>> ListByBookingId(int bookingId)
        {
            return await consultantRepository.ListByBookingId(bookingId);
        }

        public async Task<ConsultantViewModal> DetailConsultant(int? id)
        {
            return await consultantRepository.DetailConsultant(id);
        }

        public Task<List<Consultant>> List()
        {
            throw new NotImplementedException();
        }

        public Task<List<Consultant>> ListPaging(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<Consultant>> Search(string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Consultant obj)
        {
            await consultantRepository.Update(obj);
        }

        //Task<List<Consultant>> IBaseService<Consultant>.Detail(int? id)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<Consultant> DetailByBookingId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckAccountViewConsultant(int bookingId, int accountId)
        {
            return await consultantRepository.CheckAccountViewConsultant(bookingId, accountId);
        }

        public async Task<List<ConsulantViewModel>> ListReviewConsultant(int counselorsId, int pageIndex, int pageSize)
        {
            return await consultantRepository.ListReviewConsultant(counselorsId, pageIndex, pageSize);
        }
        /// Author: HuyDQ
        /// Created: 09/11/2023
        /// Description: Gửi mail thông báo cho thanh thiếu niên sau khi có phiếu đánh giá 
        /// </summary>
        /// <returns></returns>
        public async Task<HomeDoctorResponse> SendEmailConsultantToAccount(int accountId, int counselorId, int bookingId)
        {
            var account = await accountRepository.DetailAccount(accountId);
            var counselor = await accountRepository.DetailAccount(counselorId);
            try
            {
                //Cấu hình gửi mail
                string url = BaseController.SystemURL;
                string consultantShare = url + "Consultant/Share/DetailConsultant/" + bookingId;
                string body = EmailUtil.EmailConsultantToAccount(account[0].Name, counselor[0].Name, consultantShare);
                EmailUtil.SendEmail(account[0].Email, "Kết quả sau buổi tư vấn", body);
                var happySmileResponse = HomeDoctorResponse.SUCCESS();
                return happySmileResponse;

                //Gửi thông báo
                var notiObj = new Notification();
                notiObj.Id = 0;
                notiObj.Active = 1;
                notiObj.AccountId = accountId;
                notiObj.NotificationStatusId = SystemConstant.NOTIFICATION_STATUS_UNREAD;
                notiObj.Name = "Bạn vừa thực hiện tư vấn với cán bộ tư vấn "
                                + counselor[0].Name +
                                "Đánh giá buổi tư vấn =>>" + consultantShare;
                notiObj.LinkDetail = "app/phieu-danh-gia-sau-buoi-tu-van" + bookingId;
                notiObj.SenderId = counselor[0].Id;
                notiObj.Description = SystemConstant.BOOKING_VOTE_BY_USER;
                notiObj.CreatedTime = DateTime.Now;
                await notificationService.Add(notiObj);
                await notificationService.PushNotificationFCM(notiObj.AccountId, "Bạn vừa thực hiện tư vấn với cán bộ tư vấn" + counselor[0].Name,
                                                                "Đánh giá buổi tư vấn =>>" + consultantShare, bookingId,
                                                                SystemConstant.BOOKING_VOTE_BY_USER);
            }
            catch (Exception e)
            {
                var HomeDoctorSolutionResponse = HomeDoctorResponse.BAD_REQUEST();
                return HomeDoctorSolutionResponse;
            }
        }

        public async Task<List<Consultant>> ListConsultantByAccountId(int accountId)
        {
            return await consultantRepository.ListConsultantByAccountId(accountId);
        }
    }
}
