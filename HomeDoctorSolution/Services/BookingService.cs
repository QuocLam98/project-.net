
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeDoctorSolution.Constants;
using HomeDoctorSolution.Util.Email;
using HomeDoctorSolution.Controllers.Core;
using HomeDoctor.Models.ViewModels;

namespace HomeDoctorSolution.Services
{
    public class BookingService : IBookingService
    {
        IBookingRepository bookingRepository;
        IAccountRepository accountRepository;
        INotificationService notificationService;
        IDoctorsRepository doctorsRepository;
        public BookingService(
            IBookingRepository _bookingRepository,
            IAccountRepository _accountRepository,
            INotificationService _notificationService,
            IDoctorsRepository _doctorsRepository
            )
        {
            bookingRepository = _bookingRepository;
            accountRepository = _accountRepository;
            notificationService = _notificationService;
            doctorsRepository = _doctorsRepository;
        }
        public async Task Add(Booking obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await bookingRepository.Add(obj);
        }

        public int Count()
        {
            var result = bookingRepository.Count();
            return result;
        }

        public async Task Delete(Booking obj)
        {
            obj.Active = 0;
            await bookingRepository.Delete(obj);
        }
        public async Task<BookingStaticsCount> CountStatics()
        {
            var listBooking = await bookingRepository.List();
            return new BookingStaticsCount
            {
                CountWait = listBooking.Where(x => x.BookingStatusId == BookingStatusId.WAIT).Count(),
                CountConfirm = listBooking.Where(x => x.BookingStatusId == BookingStatusId.CONFIRM).Count(),
                CountAccept = listBooking.Where(x => x.BookingStatusId == BookingStatusId.ACCEPT).Count(),
                CountSuccess = listBooking.Where(x => x.BookingStatusId == BookingStatusId.SUCCESS).Count(),
                CountCancel = listBooking.Where(x => x.BookingStatusId == BookingStatusId.CANCEL).Count(),
            };

        }

        public async Task<BookingStaticsCount> CountStaticsTest()
        {
            var listBooking = await bookingRepository.ListTest();
            return new BookingStaticsCount
            {
                CountWait = listBooking.Where(x => x.BookingStatusId == BookingStatusId.WAIT).Count(),
                CountConfirm = listBooking.Where(x => x.BookingStatusId == BookingStatusId.CONFIRM).Count(),
                CountAccept = listBooking.Where(x => x.BookingStatusId == BookingStatusId.ACCEPT).Count(),
                CountSuccess = listBooking.Where(x => x.BookingStatusId == BookingStatusId.SUCCESS).Count(),
                CountCancel = listBooking.Where(x => x.BookingStatusId == BookingStatusId.CANCEL).Count(),
            };
        }
        public async Task<int> DeletePermanently(int? id)
        {
            return await bookingRepository.DeletePermanently(id);
        }

        public async Task<Booking> Detail(int? id)
        {
            return await bookingRepository.Detail(id);
        }

        public async Task<BookingViewModel> DetailViewModel(int id)
        {
            return await bookingRepository.DetailViewModel(id);
        }

        public async Task<List<Booking>> List()
        {
            return await bookingRepository.List();
        }
        public async Task<List<BookingViewModel>> ListTestByAccountId(string startTime, string endTime, int bookingStatusId, string filterId, int accountId)
        {
            return await bookingRepository.ListTestByAccountId(startTime, endTime, bookingStatusId, filterId, accountId);
        }
        public async Task<List<Booking>> ListPaging(int pageIndex, int pageSize)
        {
            return await bookingRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<BookingViewModel>> ListServerSide(BookingDTParameters parameters)
        {
            return await bookingRepository.ListServerSide(parameters);
        }
        public async Task<DTResult<BookingViewModel>> ListTestServerSide(BookingDTParameters parameters)
        {
            return await bookingRepository.ListTestServerSide(parameters);
        }
        public async Task<List<Booking>> Search(string keyword)
        {
            return await bookingRepository.Search(keyword);
        }

        public async Task Update(Booking obj)
        {
            await bookingRepository.Update(obj);
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>List booking by accountId and bookingStatusId</returns>
        public async Task<List<BookingViewModel>> ListBookingByBookingStatusId(int accountId, int bookingStatusId, int pageIndex, int pageSize)
        {
            return await bookingRepository.ListBookingByBookingStatusId(accountId, bookingStatusId, pageIndex, pageSize);
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>Update booking by roleId</returns>
        public async Task UpdateByRole(Booking obj, int? roleId)
        {
            await bookingRepository.Update(obj);
            var doctor = await doctorsRepository.Detail(obj.CounselorId);
            var account = await accountRepository.Detail(doctor.AccountId);
            var userAccount = await accountRepository.Detail(obj.AccountId);
            if (roleId == RoleId.USER)
            {
                await SendNotificationBooking(obj.AccountId, (int)obj.CounselorId, obj.Id, obj.BookingStatusId, "BY_USER", obj.Reason, "update");
            }
            else if (roleId == RoleId.DOCTOR)
            {
                await SendNotificationBooking(obj.AccountId, (int)obj.CounselorId, obj.Id, obj.BookingStatusId, "BY_COUNSELOR", obj.Reason, "update");
                var emailTitleUser = "Thông báo yêu cầu đặt lịch đã được duyệt";
                var emailBodyUser = EmailUtil.EmailAcceptBookingToAccount2(userAccount.Username, doctor.Name, account.Email, obj.StartTime.ToString("dd/MM/yyyy hh:mm:ss"));
                EmailUtil.SendEmail(userAccount.Email, emailTitleUser, emailBodyUser);
            }
            else if (roleId == RoleId.ADMIN)
            {
                await SendNotificationBooking(obj.AccountId, (int)obj.CounselorId, obj.Id, obj.BookingStatusId, "BY_ADMIN", obj.Reason, "update");
                var emailTitle = "Thông báo yêu cầu đặt lịch đã được duyệt";
                var emailBody = EmailUtil.EmailAcceptBookingToCounselor(doctor.Name, "fsapple12347@gmail.com", obj.StartTime.ToString("dd/MM/yyyy hh:mm:ss"));
                EmailUtil.SendEmail(account.Email, emailTitle, emailBody);
            }
        }

        #region Send Notification
        public async Task SendNotificationBooking(int senderId, int counselorId, int bookingId, int bookingStatusId, string? deactiveBy, string? reason, string? action)
        {
            var notiObj = new Notification();
            string nameNoti = "";
            string nameTitleFCM = "";
            string nameMSGFCM = "";
            var receiveId = 0;
            var key = "";
            string bodyEMail = "";
            string titleMail = "";
            string email = "";
            var account = await accountRepository.Detail(senderId);
            var doctor = await doctorsRepository.Detail(counselorId);
            var counselor = await accountRepository.Detail(doctor.AccountId);
            //Chờ xếp lịch + Chỉ enduser có thao tác => Noti: Cán bộ tư vấn
            if (bookingStatusId == SystemConstant.BOOKING_STATUS_WAIT_ACCEPT)
            {
                if (action == "update")
                {
                    if (deactiveBy == "BY_USER")
                    {
                        nameNoti = "Người dùng " + account.Name + " vừa thực hiện thay đổi lịch tư vấn";
                        nameTitleFCM = "Thay đổi lịch tư vấn";
                        nameMSGFCM = "Người dùng " + account.Name + " vừa thực hiện thay đổi lịch tư vấn";

                        notiObj.SenderId = senderId;
                        notiObj.AccountId = counselorId;
                        notiObj.LinkDetail = "/chi-tiet-dat-lich-cua-giao-vien/" + bookingId;
                        receiveId = counselorId;
                        key = SystemConstant.BOOKING_WAITING;

                        titleMail = "Thông báo người dùng thay đổi lịch tư vấn";
                        bodyEMail = EmailUtil.EmailUpdateBooking(counselor.Name, account.Name);
                        email = counselor.Email;
                    }
                }
                else
                {
                    if (deactiveBy == "BY_USER")
                    {
                        nameNoti = "Bạn có yêu cầu đặt lịch mới! " + account.Name + " vừa thực hiện đặt lịch tư vấn";
                        nameTitleFCM = "Yêu cầu đặt lịch mới";
                        nameMSGFCM = "Yêu cầu đặt lịch mới từ " + account.Name;

                        notiObj.SenderId = senderId;

                        notiObj.AccountId = counselor.Id;
                        notiObj.LinkDetail = "/app/chi-tiet-dat-lich-cua-giao-vien/" + bookingId;
                        receiveId = counselorId;
                        key = SystemConstant.BOOKING_WAITING;

                        titleMail = "Thông báo có lịch hẹn tư vấn mới";
                        bodyEMail = EmailUtil.EmailNewBooking(counselor.Name, account.Name);
                        email = counselor.Email;
                    }
                }
            }
            //Đã xếp lịch + Chỉ Cán bộ tư vấn có quyền thao tác => Noti to EndUser
            else if (bookingStatusId == SystemConstant.BOOKING_STATUS_WAIT_DONE)
            {
                nameNoti = "Cán bộ tư vấn " + counselor.Name + " đã xếp lịch cho bạn! ";

                nameTitleFCM = "Yêu đặt lịch được phê duyệt";
                nameMSGFCM = "Cán bộ tư vấn " + counselor.Name + " đã sắp xếp lịch";

                notiObj.SenderId = counselorId;
                notiObj.AccountId = senderId;
                notiObj.LinkDetail = "/chi-tiet-dat-lich-cua-hoc-sinh/" + bookingId;
                receiveId = senderId;
                key = SystemConstant.BOOKING_CONSELOR_CONFIRM;


                email = account.Email;
            }
            //Hủy + Cả Hai
            else if (bookingStatusId == SystemConstant.BOOKING_STATUS_CANCEL)
            {
                //NOTI to COUNSELOR
                if (deactiveBy == "BY_USER")
                {
                    nameNoti = "Lịch tư vấn với " + account.Name + " đã bị hủy vì:" + reason;

                    nameTitleFCM = "Lịch tư vấn đã bị hủy";
                    nameMSGFCM = "Lịch tư vấn với " + account.Name + " đã bị hủy";

                    notiObj.SenderId = senderId;
                    notiObj.AccountId = counselorId;
                    notiObj.LinkDetail = "/chi-tiet-dat-lich-cua-giao-vien/" + bookingId;
                    receiveId = counselorId;
                    key = SystemConstant.BOOKING_USER_REJECT;

                    titleMail = "Thông báo có người dùng hủy lịch tư vấn";
                    bodyEMail = EmailUtil.EmailCancelBookingToCounselor(counselor.Name, account.Name, reason);
                    email = counselor.Email;
                }
                //NOTI to ENDUSER
                else if (deactiveBy == "BY_COUNSELOR")
                {
                    nameNoti = "Lịch tư vấn với " + counselor.Name + " đã bị hủy vì:" + reason;

                    nameTitleFCM = "Lịch tư vấn đã bị hủy";
                    nameMSGFCM = "Lịch tư vấn với " + counselor.Name + " đã bị hủy";

                    notiObj.SenderId = counselorId;
                    notiObj.AccountId = senderId;
                    notiObj.LinkDetail = "/chi-tiet-dat-lich-cua-giao-vien/" + bookingId;
                    receiveId = senderId;
                    key = SystemConstant.BOOKING_CONSELOR_REJECT;

                    titleMail = "Thông báo cán bộ tư vấn " + counselor.Name + " hủy lịch tư vấn";
                    bodyEMail = EmailUtil.EmailCancelBookingToAccount(account.Name, counselor.Name, reason);
                    email = account.Email;
                }
            }
            //Từ chối + Cả hai
            else if (bookingStatusId == SystemConstant.BOOKING_STATUS_REJECT)
            {
                //NOTI to COUNSELOR
                if (deactiveBy == "BY_USER")
                {
                    nameNoti = "Lịch tư vấn với " + account.Name + " đã bị từ chối";

                    nameTitleFCM = "Lịch tư vấn đã bị từ chối";
                    nameMSGFCM = "Lịch tư vấn với " + account.Name + " đã bị từ chối";

                    notiObj.SenderId = senderId;
                    notiObj.AccountId = counselorId;
                    notiObj.LinkDetail = "/chi-tiet-dat-lich-cua-giao-vien/" + bookingId;
                    receiveId = counselorId;
                    key = SystemConstant.BOOKING_USER_REJECT;

                    titleMail = "Thông báo từ chối lịch hẹn tư vấn";
                    bodyEMail = EmailUtil.EmailRejectBookingToCounselor(account.Name, counselor.Name, reason);
                    email = account.Email;
                }
                //NOTI to ENDUSER
                else if (deactiveBy == "BY_COUNSELOR")
                {
                    nameNoti = "Lịch tư vấn với " + counselor.Name + " đã bị từ chối. Do cán bộ tư vấn bận hoặc chưa sắp xếp được lịch tư vấn";

                    nameTitleFCM = "Lịch tư vấn đã bị từ chối";
                    nameMSGFCM = "Lịch tư vấn với " + counselor.Name + " đã bị từ chối. Do cán bộ tư vấn bận hoặc chưa sắp xếp được lịch tư vấn";

                    notiObj.SenderId = counselorId;
                    notiObj.AccountId = senderId;
                    notiObj.LinkDetail = "/chi-tiet-dat-lich-cua-giao-vien/" + bookingId;
                    receiveId = senderId;
                    key = SystemConstant.BOOKING_CONSELOR_REJECT;

                    titleMail = "Thông báo từ chối lịch hẹn tư vấn";
                    bodyEMail = EmailUtil.EmailRejectBookingToAccount(account.Name, counselor.Name, reason);
                    email = account.Email;
                }
            }
            //Hoàn thành + Chỉ cán bộ tư vấn có quyền thao tác => Noti to Enduser
            else if (bookingStatusId == SystemConstant.BOOKING_STATUS_DONE)
            {
                nameNoti = "Buổi tư vấn với cán bộ tư vấn " + counselor.Name + " của bạn đã hoàn thành.";

                nameTitleFCM = "Hoàn thành tư vấn";
                nameMSGFCM = "Buổi tư vấn với cán bộ tư vấn " + counselor.Name + " của bạn đã hoàn thành.";

                notiObj.SenderId = counselorId;
                notiObj.AccountId = senderId;
                notiObj.LinkDetail = "/phieu-danh-gia-sau-buoi-tu-van/" + bookingId;
                receiveId = senderId;
                key = SystemConstant.BOOKING_DONE;
                string url = BaseController.SystemURL;
                var linkShare = url + "phieu-danh-gia-sau-buoi-tu-van/" + bookingId;
                titleMail = "Thông báo sau lịch hẹn tư vấn";
                bodyEMail = EmailUtil.EmailConsultantToAccount(account.Name, counselor.Name, linkShare);
                email = account.Email;
            }

            //Gửi noti
            notiObj.Id = 0;
            notiObj.Active = 1;
            //notiObj.AccountId = account.Id;
            notiObj.NotificationStatusId = SystemConstant.NOTIFICATION_STATUS_UNREAD;
            notiObj.Name = nameNoti;
            //notiObj.SenderId = counselor.Id;
            notiObj.Description = key;
            notiObj.CreatedTime = DateTime.Now;
            await notificationService.Add(notiObj);
            await notificationService.PushNotificationFCM(receiveId, nameTitleFCM, nameMSGFCM, bookingId, key);
            //Gửi email
            if (email != "" && titleMail != "" && bodyEMail != "")
            {
                EmailUtil.SendEmail(email, titleMail, bodyEMail);
            }
        }
        #endregion

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>List booking by accountId</returns>
        public async Task<List<BookingViewModel>> ListBookingByAccountId(int accountId)
        {
            return await bookingRepository.ListBookingByAccountId(accountId);
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>add booking byle Role</returns>
        public async Task AddByRole(Booking obj, int roleId)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await bookingRepository.Add(obj);
            if (roleId == RoleId.USER)
            {
                try
                {
                    //Send Email là đã tạo lịch khám
                    //Lấy thông tin booking vừa tạo và gửi mail
                    var bookingInfo = await bookingRepository.DetailViewModel(obj.Id);
                    if (bookingInfo != null)
                    {

                        var accountSendMail = await accountRepository.Detail(bookingInfo.AccountId);
                        if (accountSendMail != null)
                        {
                            if (SystemConstant.IsValidEmail(accountSendMail.Email))
                            {
                                Action sendEmail = () =>
                                {
                                    var infoBK = bookingInfo.Info.Length > 0 ? bookingInfo.Info : "Không có ghi chú";
                                    var url = "chi-tiet-lich-su-tu-van/" + bookingInfo.Id;
                                    var titleMail = "Bạn đã đặt lịch khám!";
                                    var bodyEMail = EmailUtil.EmailNewBookingForUser(bookingInfo.Name, bookingInfo.Url, bookingInfo.Address, bookingInfo.CounselorName, bookingInfo.Photo, bookingInfo.StartTime.ToString("dd/MM/yyyy hh:mm"), bookingInfo.ServiceName, bookingInfo.BookingTypeName, bookingInfo.BookingStatusName, infoBK, accountSendMail.Name, url);
                                    EmailUtil.SendEmail(accountSendMail.Email, titleMail, bodyEMail);
                                    var titleAdminEmail = "Thông báo người dùng đặt lịch";
                                    var bodyAdminEmail = EmailUtil.EmailNewBookingNotiAdmin(bookingInfo.Name, bookingInfo.Url, bookingInfo.Address, bookingInfo.CounselorName, bookingInfo.Photo, bookingInfo.StartTime.ToString("dd/MM/yyyy hh:mm"), bookingInfo.ServiceName, bookingInfo.BookingTypeName, bookingInfo.BookingStatusName, infoBK, accountSendMail.Name, url);
                                    EmailUtil.SendEmail("fsapple12347@gmail.com", titleAdminEmail, bodyAdminEmail);
                                };
                                Task task = new Task(sendEmail);
                                task.Start();
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }
                //await SendNotificationBooking(obj.AccountId, (int)obj.CounselorId, obj.Id, obj.BookingStatusId, "BY_USER", "", "add");
            }
            else if (roleId == RoleId.DOCTOR)
            {
                //await SendNotificationBooking(obj.AccountId, (int)obj.CounselorId, obj.Id, obj.BookingStatusId, "BY_COUNSELOR", "", "add");
            }
        }

        public int CountListBookingByAccountId(int? accountId, int bookingStatusId)
        {
            return bookingRepository.CountListBookingByAccountId(accountId, bookingStatusId);
        }
    }
}

