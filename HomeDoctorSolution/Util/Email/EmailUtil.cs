using HomeDoctorSolution.Models;
using System.Net.Mail;
using System.Net;

namespace HomeDoctorSolution.Util.Email
{
    public class EmailUtil
    {
        //public static string EMAIL_CREDENTIAL_NAME = "lacvietauctionvn@gmail.com";
        //public static string EMAIL_CREDENTIAL_PASSWORD = "Novatic25";
        public static string EMAIL_CREDENTIAL_NAME = AppSettingConfig.Instance.Get<string>("EmailConfig:Username");
        public static string EMAIL_CREDENTIAL_PASSWORD = AppSettingConfig.Instance.Get<string>("EmailConfig:Password");
        public static string EMAIL_CREDENTIAL_NAME_OFFICE365 = AppSettingConfig.Instance.Get<string>("EmailConfig:Office365Username");
        public static string EMAIL_CREDENTIAL_PASSWORD_OFFICE365 = AppSettingConfig.Instance.Get<string>("EmailConfig:Office365Password");
        public static string EMAIL_SENDER_NAME = AppSettingConfig.Instance.Get<string>("EmailConfig:NameSender");
        //public static string currentUrl = SystemConstant.DEFAULT_URL;
        public static string currentUrl = "https://bacsitainha.vn/";
        private static string currentUrlLogo = SystemConstant.DEFAULT_URL_LOGO;
        //private static string headerEmail = @"

        //";
        public static HomeDoctorResponse SendEmail(string recipients, string subject, string body)
        {
            //var novaticResponse = SendEmailOffice365(recipients, subject, body);
            var novaticResponse = SendEmailOffice365(recipients, subject, body);
            return novaticResponse;
        }
        public static HomeDoctorResponse SendEmailOffice365(string recipients, string subject, string body)
        {
            var novaticResponse = HomeDoctorResponse.SUCCESS();

            string emailUsername = EMAIL_CREDENTIAL_NAME_OFFICE365;
            string emailPassword = EMAIL_CREDENTIAL_PASSWORD_OFFICE365;
            string senderName = EMAIL_SENDER_NAME;
            //string recipients = "hung.nguyen@novatic.vn,nguyenhungbk92@gmail.com";
            //string subject = "Hello!";
            //string body = "<h1>Hello World</h1>";

            try
            {
                var toAddresses = recipients.Split(',');
                foreach (var to in toAddresses)
                {
                    int tryAgain = 20;
                    bool failed = false;
                    do
                    {
                        Thread.Sleep(3000);
                        new Thread(() =>
                        {
                            SmtpClient client = new SmtpClient();
                            client.Host = "smtp.office365.com";
                            client.Port = 587;
                            client.UseDefaultCredentials = false; // This require to be before setting Credentials property
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.Credentials = new NetworkCredential(emailUsername, emailPassword); // you must give a full email address for authentication 
                            client.TargetName = "STARTTLS/smtp.office365.com"; // Set to avoid MustIssueStartTlsFirst exception
                            client.EnableSsl = true;// Set to avoid secure connection exception
                            client.Timeout = 1000000000;                //Timeout = 1000000000
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(emailUsername, senderName); // sender must be a full email address
                            message.Subject = subject;
                            message.IsBodyHtml = true;
                            message.Body = body;
                            message.BodyEncoding = System.Text.Encoding.UTF8;
                            message.SubjectEncoding = System.Text.Encoding.UTF8;
                            message.To.Add(to.Trim());
                            try
                            {
                                client.Send(message);
                            }
                            catch (Exception ex)
                            {
                                //throw;
                                failed = true;
                                tryAgain--;
                                var exception = ex.Message.ToString();
                            }
                        }).Start();
                    } while (failed && tryAgain != 0);
                }
            }
            catch (Exception e)
            {
                novaticResponse = HomeDoctorResponse.BAD_REQUEST(e);
            }


            return novaticResponse;
        }
        public static HomeDoctorResponse SendEmailGmail(string recipients, string subject, string body)
        {
            var novaticResponse = HomeDoctorResponse.SUCCESS();
            string emailUsername = EMAIL_CREDENTIAL_NAME;
            string emailPassword = EMAIL_CREDENTIAL_PASSWORD;
            string senderName = EMAIL_SENDER_NAME;
            //string emailUsername = "novaticvn@outlook.com";
            //string emailPassword = "Novatic@25";
            //string recipients = "hung.nguyen@novatic.vn,nguyenhungbk92@gmail.com";
            try
            {
                var toAddresses = recipients.Split(',');
                foreach (var to in toAddresses)
                {
                    int tryAgain = 20;
                    bool failed = false;
                    do
                    {
                        Thread.Sleep(3000);
                        new Thread(() =>
                        {
                            SmtpClient client = new SmtpClient();
                            client.Host = "smtp.gmail.com";
                            client.Port = 587;
                            client.UseDefaultCredentials = false; // This require to be before setting Credentials property
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.Credentials = new NetworkCredential(emailUsername, emailPassword); // you must give a full email address for authentication 
                            //client.TargetName = "STARTTLS/smtp.office365.com"; // Set to avoid MustIssueStartTlsFirst exception
                            client.EnableSsl = true;// Set to avoid secure connection exception
                            client.Timeout = 1000000000;                //Timeout = 1000000000
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(emailUsername, senderName); // sender must be a full email address
                            message.Subject = subject;
                            message.IsBodyHtml = true;
                            message.Body = body;
                            message.BodyEncoding = System.Text.Encoding.UTF8;
                            message.SubjectEncoding = System.Text.Encoding.UTF8;
                            message.To.Add(to.Trim());
                            try
                            {
                                client.Send(message);
                            }
                            catch (Exception ex)
                            {
                                //throw;
                                failed = true;
                                tryAgain--;
                                var exception = ex.Message.ToString();
                            }
                        }).Start();
                    } while (failed && tryAgain != 0);

                    //            foreach (var to in toAddresses)
                    //{
                    //	new Thread(() =>
                    //	{

                    //		// send  email 
                    //		var client = new SmtpClient("smtp.gmail.com", 587)
                    //		{
                    //			Credentials = new NetworkCredential(EmailUtil.EMAIL_CREDENTIAL_NAME, EmailUtil.EMAIL_CREDENTIAL_PASSWORD),
                    //			EnableSsl = true
                    //		};

                    //		MailMessage msg = new MailMessage(EmailUtil.EMAIL_CREDENTIAL_NAME, to, subject, body);
                    //		msg.IsBodyHtml = true;
                    //		client.Send(msg);
                    //	}).Start();
                }
            }
            catch (Exception e)
            {
                novaticResponse = HomeDoctorResponse.BAD_REQUEST(e);
            }
            return novaticResponse;
        }
        // Email quên mật khẩu
        public static string EmailForgotPassword(string username, string stringRandom)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bác sĩ tại nhà đã nhận được yêu cầu thay đổi mật khẩu của quý khách.</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin hãy click vào đường dẫn sau để đổi mật khẩu. Lưu ý link chỉ có hiệu lực trong vòng 24 giờ.</p>
                                      <p style='text-align:center;'>  <a href='" + stringRandom + @"' target='_blank' style='display: inline-block; padding: 16px 36px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;background: var(--Blue-linear, linear-gradient(90deg, #1470F5 0%, #388AFF 100%)); border-radius: 15px;'>Đổi mật khẩu</a></p>
            ";
            string content = EmailTemplate("Bạn đã quên mật khẩu?", body);
            return content;
        }

        public static string EmailForgotPassword(string username, string stringRandom, string code)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bác sĩ tại nhà đã nhận được yêu cầu thay đổi mật khẩu của quý khách.</p>
                                      <p style='text-align:center;'>  <a href='' target='_blank' style='display: inline-block; padding: 16px 36px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;background: var(--Blue-linear, linear-gradient(90deg, #1470F5 0%, #388AFF 100%)); border-radius: 15px;'>" + code+@"</a></p>
            ";
            string content = EmailTemplate("Bạn đã quên mật khẩu?", body);
            return content;
        }
        // Email đăng ký tài khoản
        public static string EmailRegister(string username, string stringRandom)
        {
            string body = @"
                   <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bác sĩ tại nhà yêu cầu xác thực tài khoản của bạn.</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin hãy click vào đường dẫn sau để xác thực tài khoản. Lưu ý link chỉ có hiệu lực trong vòng 24 giờ.</p>
                   <p style='text-align:center'><a href='" + stringRandom + @"' target='_blank' style='display: inline-block; padding: 16px 36px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; background: var(--Blue-linear, linear-gradient(90deg, #1470F5 0%, #388AFF 100%)); border-radius: 15px;'>Xác thực tài khoản</a></p>
                   ";
            string content = EmailTemplate("Yêu cầu xác thực tài khoản", body);
            return content;
        }

        //Gửi kết quả khảo sát 
        public static string EmailResultDetail(string resultSurveyShare)
        {
            string body = @"
                            <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bạn vừa nhận được chia sẻ kết quả trắc nghiệm sức khỏe tâm thần</p>
                            <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Vui lòng nhấn vào liên kết bên dưới.Lưu ý: Liên kết này chỉ có hiệu lực trong vòng 24 giờ</p>
                            <p style='text-align:center;'>  <a href=" + resultSurveyShare + @" target='_blank' style='display: inline-block; padding: 16px 36px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;background: var(--Blue-linear, linear-gradient(90deg, #1470F5 0%, #388AFF 100%)); border-radius: 15px;'>Kết quả Trắc Nghiệm SKTT</a></p>
            ";
            string content = EmailTemplate("Kết quả Trắc Nghiệm Sức Khỏe Tâm Thần", body);
            return content;
           
        }

        #region TƯ VẤN VIÊN BOOKING
        // account là thành thiếu niên
        // counselor là cán bộ tư vấn
        // Email thông báo có người booking tư vấn: gửi cho cán bộ tư vấn
        public static string EmailNewBooking(string counselorName, string accountName)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + counselorName + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Người dùng " + accountName + @" mới thực hiện đặt lịch tư vấn với bạn. Bạn vui lòng kiểm tra thông tin và xác nhận lịch tư vấn.
                </p>
                 ";
            string content = EmailTemplate("Thông báo có đặt lịch hẹn mới", body);
            return content;
        }
        public static string EmailNewBookingForUser(string accountBookingName, string accountBookingPhone, string accountBookingAddress, string counselorName, string counselorPhoto, string bookingStartTime, string serviceName, string bookingTypeName, string bookingStatusName, string bookingInfo, string accountName, string url)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + accountName + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bạn vừa đặt lịch tư vấn tại bác sĩ tại nhà. Dưới đây là thông tin đặt lịch của bạn.</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Thông tin người khám:</p>
                <ul style='font-size: 16px!important; margin: 0; padding-left: 15px; padding-bottom:10px'>
                    <li>Tên người khám: " + accountBookingName + @" </li>
                    <li>Số điện thoại: " + accountBookingPhone + @" </li>
                </ul>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Thông tin đặt lịch:</p>
                <ul style='font-size: 16px!important; margin: 0; padding-left: 15px; padding-bottom: 10px;'>
                    <li>Loại đặt lịch: " + bookingTypeName + @" </li>
                    <li>Trạng thái: " + bookingStatusName + @" </li>
                    <li>Địa chỉ: " + accountBookingAddress + @" </li>
                    <li>Thời gian: " + bookingStartTime + @" </li>
                    <li>Bác sĩ: " + counselorName + @" </li>
                    <li>Dịch vụ: " + serviceName + @" </li>
                    <li>Ghi chú: " + bookingInfo + @" </li>
                </ul>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bạn vui lòng chờ để bác sĩ xác nhận yêu cầu đặt lịch của bạn. Bạn có thể xem lại thông tin lịch khám của mình <a href=" + SystemConstant.DEFAULT_URL + url + @">tại đây</a>. Chúng tôi sẽ thông báo tới bạn sớm nhất khi bác sĩ xác nhận lịch khám của bạn.</p>
                 ";
            string content = EmailTemplate("Bạn đã đặt lịch khám!", body);
            return content;
        }
        public static string EmailNewBookingNotiAdmin(string accountBookingName, string accountBookingPhone, string accountBookingAddress, string counselorName, string counselorPhoto, string bookingStartTime, string serviceName, string bookingTypeName, string bookingStatusName, string bookingInfo, string accountName, string url)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Người dùng " + accountName + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>" + accountName + @" vừa đặt lịch tư vấn tại bác sĩ tại nhà. Dưới đây là thông tin đặt lịch của bạn.</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Thông tin người khám:</p>
                <ul style='font-size: 16px!important; margin: 0; padding-left: 15px; padding-bottom:10px'>
                    <li>Tên người khám: " + accountBookingName + @" </li>
                    <li>Số điện thoại: " + accountBookingPhone + @" </li>
                </ul>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Thông tin đặt lịch:</p>
                <ul style='font-size: 16px!important; margin: 0; padding-left: 15px; padding-bottom: 10px;'>
                    <li>Loại đặt lịch: " + bookingTypeName + @" </li>
                    <li>Trạng thái: " + bookingStatusName + @" </li>
                    <li>Địa chỉ: " + accountBookingAddress + @" </li>
                    <li>Thời gian: " + bookingStartTime + @" </li>
                    <li>Bác sĩ: " + counselorName + @" </li>
                    <li>Dịch vụ: " + serviceName + @" </li>
                    <li>Ghi chú: " + bookingInfo + @" </li>
                </ul>          
                 ";
            string content = EmailTemplate(accountName + " đặt lịch khám!", body);
            return content;
        }
        public static string EmailUpdateBooking(string counselorName, string accountName)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + counselorName + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Người dùng " + accountName + @" mới thực hiện thay đổi lịch tư vấn với bạn. Bạn vui lòng kiểm tra thông tin và xác nhận lịch tư vấn.
                </p>
                 ";
            string content = EmailTemplate("Thông báo có đặt lịch hẹn mới", body);
            return content;
        }
        // Email thông báo có người hủy booking: gửi cho cán bộ tư vấn
        public static string EmailCancelBookingToCounselor(string counselorName, string accountName, string reasonCancel)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + counselorName + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                Người dùng " + accountName + @" đã huỷ lịch tư vấn vì: " + reasonCancel + @".
                </p>
                 ";
            string content = EmailTemplate("Người dùng hủy lịch tư vấn", body);
            return content;
        }
        // Email cập nhật lịch tư vấn trong lúc chờ accept: gửi cho cán bộ tư vấn
        public static string EmailUpdateBookingWaitToCounselor(string counselorName, string accountName)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + counselorName + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Người dùng " + accountName + @" cập nhật lại thông tin đặt lịch trong lúc đang chờ xác nhận từ bạn. Bạn lưu ý kiểm tra lại thông tin đặt lịch nhé.
                </p>
                 ";
            string content = EmailTemplate("Thông báo cập nhật thông tin đặt lịch", body);
            return content;
        }
        // Email trước buổi tư vấn: gửi cho cán bộ tư vấn
        public static string EmailReminderBookingToCounselor(string counselorName, string accountName, string timeBooking)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + counselorName + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Hôm nay bạn có buổi tư vấn với người dùng " + accountName + @" vào lúc " + timeBooking + @". Bạn lưu ý thời gian cuộc tư vấn để đảm bảo chất lượng buổi tư vấn nhé.
                </p>
                 ";
            string content = EmailTemplate("Thông báo nhắc lịch tư vấn", body);
            return content;
        }
        // Email sau buổi tư vấn : gửi cho cán bộ tư vấn
        public static string EmailConsultantToCounselor(string counselorName, string accountName, string urlConsultant)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + counselorName + @"!</p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Bạn vừa thực hiện tư vấn với cán bộ tư vấn " + accountName + @" . vui lòng điền đủ thông tin vào phiếu đánh giá tư vấn
                 </p>
                 ";
            string content = EmailTemplate("Kết thúc buổi tư vấn", body);
            return content;
        }
        #endregion

        #region THANH THIẾU NIÊN BOOKING
        // userName là thành thiếu niên
        // counselorName là cán bộ tư vấn
        // Email xác nhận book lịch thành công: gửi cho thanh thiếu niên
        public static string EmailBookingSuccess(string username)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Lịch hẹn của bạn sẽ được gửi tới cán bộ tư vấn để chờ xác nhận. Chúng tôi sẽ gửi thông báo tới bạn sau khi cán bộ tư vấn xác nhận lịch thành công.</p>
                 ";
            string content = EmailTemplate("Đặt lịch tư vấn thành công", body);
            return content;
        }
        // Email cán bộ tư vấn xác nhận đã chấp thuận đặt lịch book: gửi cho thanh thiếu niên
        public static string EmailAcceptBookingToAccount(string username, string counselorName, string emailCounselor)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Cán bộ tư vấn " + counselorName + @" vừa xác nhận lịch khám của bạn. Email này là để xác nhận cuộc hẹn của bạn vào [Date and Time] . Vui lòng đến ít nhất 15 phút trước giờ hẹn của bạn để đề phòng bất kỳ sự chậm trễ hoặc trường hợp bất ngờ nào
                 </p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Nếu bạn cần đổi lịch hoặc hủy cuộc hẹn, vui lòng liên hệ với chúng tôi càng sớm càng tốt tại: " + emailCounselor + @" .
                 </p>
                 ";
            string content = EmailTemplate("Cán bộ tư vấn chấp nhận lịch khám với bạn", body);
            return content;
        }
        public static string EmailAcceptBookingToAccount2(string username, string counselorName, string emailCounselor,string dateAndTime)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Bác sĩ " + counselorName + @" vừa xác nhận lịch tư vấn của bạn. Email này là để xác nhận cuộc hẹn của bạn vào "+ dateAndTime+@" . Vui lòng đến ít nhất 15 phút trước giờ hẹn của bạn để đề phòng bất kỳ sự chậm trễ hoặc trường hợp bất ngờ nào
                 </p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Nếu bạn cần đổi lịch hoặc hủy cuộc hẹn, vui lòng liên hệ với chúng tôi càng sớm càng tốt tại: " + emailCounselor + @" .
                 </p>
                 ";
            string content = EmailTemplate("Bác sĩ "+ counselorName + " chấp nhận lịch tư vấn với bạn", body);
            return content;
        }
        // Email admin xác nhận đã chấp thuận đặt lịch tư vấn: Gửi cho bác sĩ
        public static string EmailAcceptBookingToCounselor(string counselorName, string emailCounselor , string dateAndTime)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + counselorName + @"!</p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Admin vừa xác nhận lịch khám của bạn. Email này là để xác nhận cuộc hẹn của bạn vào "+dateAndTime+@" . Vui lòng đến ít nhất 15 phút trước giờ hẹn của bạn để đề phòng bất kỳ sự chậm trễ hoặc trường hợp bất ngờ nào
                 </p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Nếu bạn cần đổi lịch hoặc hủy cuộc hẹn, vui lòng liên hệ với chúng tôi càng sớm càng tốt tại: " + emailCounselor + @" .
                 </p>
                 ";
            string content = EmailTemplate("Quản trị viên xét duyệt lịch khám với bạn", body);
            return content;
        }
        // Email cán bộ tư vấn hủy đặt lịch book: gửi cho thanh thiếu niên
        public static string EmailRejectBookingToAccount(string username, string counselorName, string reasonReject)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Cán bộ tư vấn " + counselorName + @" vừa xác nhận từ chối lịch tư vấn của bạn vì:  " + reasonReject + @", bạn thực hiện đăng ký lại lịch tư vấn nhé!
                 </p>
                 ";
            string content = EmailTemplate("Cán bộ tư vấn từ chối lịch tư vấn với bạn", body);
            return content;
        }
        // Email cán bộ tư vấn xác nhận từ chối đặt lịch book: gửi cho thanh thiếu niên
        public static string EmailCancelBookingToAccount(string username, string counselorName, string reasonReject)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Cán bộ tư vấn " + counselorName + @" vừa xác nhận hủy lịch tư vấn của bạn vì:  " + reasonReject + @", bạn thực hiện đăng ký lại lịch tư vấn nhé!
                 </p>
                 ";
            string content = EmailTemplate("Cán bộ tư vấn từ chối lịch tư vấn với bạn", body);
            return content;
        }
        // Email sau buổi tư vấn : gửi cho thanh thiếu niên có phiếu đánh giá
        public static string EmailConsultantToAccount(string username, string counselorName, string urlConsultant)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Bạn vừa thực hiện tư vấn với cán bộ tư vấn " + counselorName + @" . Vui lòng đánh giá chất lượng của buổi tư vấn: " + urlConsultant + @"
                 </p>
                 ";
            string content = EmailTemplate("Kết quả sau buổi tư vấn", body);
            return content;
        }

        // Email trước buổi tư vấn online: gửi cho thanh thiếu niên
        public static string EmailReminderBookingOnlineToAccount(string username, string counselorName, string timeBooking, string linkBooking)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                Hôm nay bạn có buổi tư vấn với cán bộ tư vấn " + counselorName + @" vào lúc " + timeBooking + @". Bạn vui lòng truy cập vào link online trước ít nhất 5 phút để phòng trường hợp xảy ra các vấn đề kỹ thuật và chúng tôi có thể hỗ trợ kỹ thuật giúp bạn nếu cần.
                </p>
                <p style='text-align:center;'>
                      <a href='" + linkBooking + @"' target='_blank' style='display: inline-block; padding: 16px 36px; font-		  	family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; color: #ffffff; text-decoration:  		  none; border-radius: 6px;background: var(--Blue-linear, linear-gradient(90deg, #1470F5 0%, #388AFF 			100%)); border-radius: 15px;'>Link buổi tư vấn
	                </a>
                </p>
                 ";
            string content = EmailTemplate("Thông báo nhắc lịch tư vấn", body);
            return content;
        }
        // Email trước buổi tư vấn offline: gửi cho thanh thiếu niên
        public static string EmailReminderBookingOfflineToAccount(string username, string counselorName, string timeBooking, string addressBooking)
        {
            string body = @"
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                Hôm nay bạn có buổi tư vấn với cán bộ tư vấn " + counselorName + @" vào lúc " + timeBooking + @" tại " + addressBooking + @". Bạn lưu ý thời gian cuộc tư vấn để đảm bảo chất lượng buổi tư vấn nhé.
                </p>
                 ";
            string content = EmailTemplate("Thông báo nhắc lịch tư vấn", body);
            return content;
        }
        public static string EmailRejectBookingToCounselor(string username, string counselorName, string reasonReject)
        {
            string body = @"
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + counselorName + @"!</p>
                 <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>
                    Người dùng " + username + @" vừa xác nhận từ chối lịch tư vấn vì:  " + reasonReject + @", bạn thực hiện đăng ký lại lịch tư vấn nhé!
                 </p>
                 ";
            string content = EmailTemplate("Tư vấn từ chối lịch tư vấn với bạn", body);
            return content;
        }
        #endregion

        //Email phê duyệt bài viết
        public static string EmailConfirmForumPost(string username, string url)
        {
            string content = @"
                          <!DOCTYPE html>
                        <html>
                        <head>

                          <meta charset='utf-8'>
                          <meta http-equiv='x-ua-compatible' content='ie=edge'>
                          <title>Password Reset</title>
                          <meta name='viewport' content='width=device-width, initial-scale=1'>
                          <link href='https://fonts.googleapis.com/css?family=Montserrat&display=swap' rel='stylesheet'>


                          <style type='text/css'>
  
                          body,
                          table,
                          td,
                          a {
                            -ms-text-size-adjust: 100%; /* 1 */
                            -webkit-text-size-adjust: 100%; /* 2 */
                          }

  
                          table,
                          td {
                            mso-table-rspace: 0pt;
                            mso-table-lspace: 0pt;
                          }

  
                          img {
                            -ms-interpolation-mode: bicubic;
                          }

  
                          a[x-apple-data-detectors] {
                            font-family: inherit !important;
                            font-size: inherit !important;
                            font-weight: inherit !important;
                            line-height: inherit !important;
                            color: inherit !important;
                            text-decoration: none !important;
                          }

  
                          div[style*='margin: 16px 0;'] {
                            margin: 0 !important;
                          }

                          body {
                            width: 100% !important;
                            height: 100% !important;
                            padding: 0 !important;
                            margin: 0 !important;
                          }

  
                          table {
                            border-collapse: collapse !important;
                          }

                          a {
                            color: #1a82e2;
                          }

                          img {
                            height: auto;
                            line-height: 100%;
                            text-decoration: none;
                            border: 0;
                            outline: none;
                          }
                          .passwordValue{
                            display: none;
                          }

                          .showEmailButton:active{
                            background: green !important;
                          }
                          .showEmailButton:active .passwordValue{
                            display: block !important;
                          }
                          .showEmailButton:active .passwordDummy{
                            display: none !important;
                          }
                          </style> 
                        </head>
                        <body style='background-color: #e9ecef;'>

                          <!-- start preheader -->
                          <div class='preheader' style='display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;'>
    
                          </div>
                          <!-- end preheader -->

                          <!-- start body -->
                          <table border='0' cellpadding='0' cellspacing='0' width='100%'>

                            <!-- start logo -->
                            <tr>
                              <td align='center' bgcolor='#e9ecef'>
        
                                <table border='0' cellpadding='0' cellspacing='0' width='90%' style='margin: 0 5%'>
                                  <tr>
                                    <td align='center' valign='top' style='padding: 36px 24px;'>
                                      <a href='" + currentUrl + @"' target='_blank' style='display: inline-block;'>
                                        <img src='" + currentUrl + @"" + currentUrlLogo + @"' alt='' border='0'   style='display: block;min-width: 48px;width: 200px;'>
                                      </a>
                                    </td>
                                  </tr>
                                </table>
        
                              </td>
                            </tr>
                            <!-- end logo -->

                            <!-- start hero -->
                            <tr>
                              <td align='center' bgcolor='#e9ecef'>
                                <!--[if (gte mso 9)|(IE)]>
        
                                <![endif]-->
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'  >
                                  <tr>
                                    <td align='left' bgcolor='#ffffff' style='padding: 36px 24px 0; font-family:Roboto,RobotoDraft,Helvetica,Arial,sans-serif; border-top: 3px solid #d4dadf;'>
                                      <h1 style='margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;'>Yêu cầu đăng bài viết</h1>
                                    </td>
                                  </tr>
                                </table>
        
                              </td>
                            </tr>
                            <!-- end hero -->

                            <!-- start copy block -->
                            <tr>
                              <td align='center' bgcolor='#e9ecef'>
                                <!--[if (gte mso 9)|(IE)]>
        
                                <![endif]-->
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' class='showEmailButton'>

                                  <!-- start copy -->
                                  <tr>
                                    <td align='left' bgcolor='#ffffff' style='padding: 24px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; line-height: 24px;'>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bác sĩ tại nhà gửi đến thông báo phê duyệt bài viết.</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bài viết của bạn đã được phê duyệt. Xem chi tiết bài viết của bạn</p>
                                    </td>
                                  </tr>
                                  <!-- end copy -->

                                  <!-- start button -->
                                  <tr>
                                    <td align='left' bgcolor='#ffffff'>
                                      <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                        <tr>
                                          <td align='center' bgcolor='#ffffff' style='padding: 12px;'>
                                            <table border='0' cellpadding='0' cellspacing='0'>
                                              <tr>
                                                <td align='center' bgcolor='#1a82e2' style='border-radius: 6px;'>
                                                  <a href='" + url + @"' target='_blank' style='display: inline-block; padding: 16px 36px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;'>Chi tiết bài viết</a>
                                                </td>
                                              </tr>
                                            </table>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr> 
                                  <!-- end button -->

                                  <!-- start copy -->
                                  <tr>
                                    <td align='left' bgcolor='#ffffff' style='padding: 20px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; line-height: 24px;'>
             
                                    </td>
                                  </tr>
          
                               

                                </table>
       
                              </td>
                            </tr>

                            <tr>
                              <td align='center' bgcolor='#e9ecef' style='padding: 24px;'>
        
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>

                                  <!-- start permission -->
                                  <tr>
                                    <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 14px; line-height: 20px; color: #666;'>
                                      <p style='margin: 0;'>Bạn nhận được email này vì chúng tôi đã nhận được yêu cầu từ tài khoản của bạn.</p>
                                    </td>
                                  </tr>
                                  <!-- end permission -->

                                  <!-- start unsubscribe -->
                                  <tr>
                                    <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 14px; line-height: 20px; color: #666;'>
                                      <p style='margin: 0;'>Copyright " + DateTime.UtcNow.Year + @" <a href='" + currentUrl + @"' target='_blank'>Bác sĩ tại nhà</a></p>
                                      <p style='margin: 0;'>All rights reserved.</p>
                                    </td>
                                  </tr>
                                  <!-- end unsubscribe -->

                                </table>
        
                              </td>
                            </tr>
                            <!-- end footer -->

                          </table>
                          <!-- end body -->

                        </body>
                        </html>

                                                                               ";
            return content;
        }
        public static string EmailCancelForumPost(string username, string url)
        {
            string content = @"
                          <!DOCTYPE html>
                        <html>
                        <head>

                          <meta charset='utf-8'>
                          <meta http-equiv='x-ua-compatible' content='ie=edge'>
                          <title>Password Reset</title>
                          <meta name='viewport' content='width=device-width, initial-scale=1'>
                          <link href='https://fonts.googleapis.com/css?family=Montserrat&display=swap' rel='stylesheet'>


                          <style type='text/css'>
  
                          body,
                          table,
                          td,
                          a {
                            -ms-text-size-adjust: 100%; /* 1 */
                            -webkit-text-size-adjust: 100%; /* 2 */
                          }

  
                          table,
                          td {
                            mso-table-rspace: 0pt;
                            mso-table-lspace: 0pt;
                          }

  
                          img {
                            -ms-interpolation-mode: bicubic;
                          }

  
                          a[x-apple-data-detectors] {
                            font-family: inherit !important;
                            font-size: inherit !important;
                            font-weight: inherit !important;
                            line-height: inherit !important;
                            color: inherit !important;
                            text-decoration: none !important;
                          }

  
                          div[style*='margin: 16px 0;'] {
                            margin: 0 !important;
                          }

                          body {
                            width: 100% !important;
                            height: 100% !important;
                            padding: 0 !important;
                            margin: 0 !important;
                          }

  
                          table {
                            border-collapse: collapse !important;
                          }

                          a {
                            color: #1a82e2;
                          }

                          img {
                            height: auto;
                            line-height: 100%;
                            text-decoration: none;
                            border: 0;
                            outline: none;
                          }
                          .passwordValue{
                            display: none;
                          }

                          .showEmailButton:active{
                            background: green !important;
                          }
                          .showEmailButton:active .passwordValue{
                            display: block !important;
                          }
                          .showEmailButton:active .passwordDummy{
                            display: none !important;
                          }
                          </style> 
                        </head>
                        <body style='background-color: #e9ecef;'>

                          <!-- start preheader -->
                          <div class='preheader' style='display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;'>
    
                          </div>
                          <!-- end preheader -->

                          <!-- start body -->
                          <table border='0' cellpadding='0' cellspacing='0' width='100%'>

                            <!-- start logo -->
                            <tr>
                              <td align='center' bgcolor='#e9ecef'>
        
                                <table border='0' cellpadding='0' cellspacing='0' width='90%' style='margin: 0 5%'>
                                  <tr>
                                    <td align='center' valign='top' style='padding: 36px 24px;'>
                                      <a href='" + currentUrl + @"' target='_blank' style='display: inline-block;'>
                                        <img src='" + currentUrl + @"" + currentUrlLogo + @"' alt='' border='0'   style='display: block;min-width: 48px;width: 200px;'>
                                      </a>
                                    </td>
                                  </tr>
                                </table>
        
                              </td>
                            </tr>
                            <!-- end logo -->

                            <!-- start hero -->
                            <tr>
                              <td align='center' bgcolor='#e9ecef'>
                                <!--[if (gte mso 9)|(IE)]>
        
                                <![endif]-->
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'  >
                                  <tr>
                                    <td align='left' bgcolor='#ffffff' style='padding: 36px 24px 0; font-family:Roboto,RobotoDraft,Helvetica,Arial,sans-serif; border-top: 3px solid #d4dadf;'>
                                      <h1 style='margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;'>Yêu cầu đăng bài viết</h1>
                                    </td>
                                  </tr>
                                </table>
        
                              </td>
                            </tr>
                            <!-- end hero -->

                            <!-- start copy block -->
                            <tr>
                              <td align='center' bgcolor='#e9ecef'>
                                <!--[if (gte mso 9)|(IE)]>
        
                                <![endif]-->
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' class='showEmailButton'>

                                  <!-- start copy -->
                                  <tr>
                                    <td align='left' bgcolor='#ffffff' style='padding: 24px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; line-height: 24px;'>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Xin chào " + username + @"!</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bác sĩ tại nhà gửi đến thông báo phê duyệt bài viết.</p>
                                      <p style='margin: 0; padding-bottom: 10px;font-size: 18px!important;'>Bài viết của bạn đã bị từ chối. Xem chi tiết bài viết của bạn</p>
                                    </td>
                                  </tr>
                                  <!-- end copy -->

                                  <!-- start button -->
                                  <tr>
                                    <td align='left' bgcolor='#ffffff'>
                                      <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                        <tr>
                                          <td align='center' bgcolor='#ffffff' style='padding: 12px;'>
                                            <table border='0' cellpadding='0' cellspacing='0'>
                                              <tr>
                                                <td align='center' bgcolor='#1a82e2' style='border-radius: 6px;'>
                                                  <a href='" + url + @"' target='_blank' style='display: inline-block; padding: 16px 36px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;'>Chi tiết bài viết</a>
                                                </td>
                                              </tr>
                                            </table>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr> 
                                  <!-- end button -->

                                  <!-- start copy -->
                                  <tr>
                                    <td align='left' bgcolor='#ffffff' style='padding: 20px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 16px; line-height: 24px;'>
             
                                    </td>
                                  </tr>
          
                               

                                </table>
       
                              </td>
                            </tr>

                            <tr>
                              <td align='center' bgcolor='#e9ecef' style='padding: 24px;'>
        
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>

                                  <!-- start permission -->
                                  <tr>
                                    <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 14px; line-height: 20px; color: #666;'>
                                      <p style='margin: 0;'>Bạn nhận được email này vì chúng tôi đã nhận được yêu cầu từ tài khoản của bạn.</p>
                                    </td>
                                  </tr>
                                  <!-- end permission -->

                                  <!-- start unsubscribe -->
                                  <tr>
                                    <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif; font-size: 14px; line-height: 20px; color: #666;'>
                                      <p style='margin: 0;'>Copyright " + DateTime.UtcNow.Year + @" <a href='" + currentUrl + @"' target='_blank'>Bác sĩ tại nhà</a></p>
                                      <p style='margin: 0;'>All rights reserved.</p>
                                    </td>
                                  </tr>
                                  <!-- end unsubscribe -->

                                </table>
        
                              </td>
                            </tr>
                            <!-- end footer -->

                          </table>
                          <!-- end body -->

                        </body>
                        </html>

                                                                               ";
            return content;
        }

        //Giao diện mới
        public static string EmailTemplate(string title,string content)
        {
            string body = @"
                    <!doctype html>
                    <html lang='en-US'>

                    <head>
                        <meta content='text/html; charset=utf-8' http-equiv='Content-Type' />
                        <style type='text/css'>
                            a:hover {text-decoration: none !important;}
                        </style>
                    </head>

                    <body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'>
                        <!--100% body table-->
                        <table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8'
                            style='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;'>
                            <tr>
                                <td>
                                    <table style='background-color: #f2f3f8; max-width:750px;  margin:0 auto;' width='100%' border='0'
                                        align='center' cellpadding='0' cellspacing='0'>
                                        <tr>
                                            <td style='height:80px;'>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style='text-align:center;'>
                                              <a href='" + currentUrl + @"' title='logo' target='_blank'>
                                                <img width='240' src='" + currentUrl + currentUrlLogo + @"' title='logo' alt='logo'>
                                              </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='height:20px;'>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'
                                                    style='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);'>
                                                    <tr>
                                                        <td style='height:40px;'>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style='padding:0 35px;text-align: left !important;'>
                                                            <h1 style='margin: 0'>"+ title + @"</h1>
                                                            <span
                                                                style='display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100%;'></span>
                                                            " + content + @"
                                                        </td>
                                   
                                                    </tr>
                                                    <tr>
                                                        <td style='padding:0 35px;text-align:left;line-height: 24px;font-size: 13px; font-style: italic; color: #455056a1;'>
                                                            Trân trọng!
                                                            <br>
                                                            Bác sĩ tại nhà
                                                            <br>
                                                            Địa chỉ: 382 Hồ Tùng Mậu, Phú Diễn, Bắc Từ Liêm, Hà Nội
                                                            <br>
                                                            Số điện thoại: 08690.565.868
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style='height:40px;'>&nbsp;</td>
                                                    </tr>

                                                </table>
                                            </td>
                                        <tr>
                                            <td style='height:20px;'>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style='text-align:center;'>
                                                <a style='text-decoration: none;' href='" + currentUrl + @"'><p style='font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;'>&copy; <strong>" + currentUrl + @"</strong></p></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='height:80px;'>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <!--/100% body table-->
                    </body>

                    </html>
            ";
            return body;
        }

    }
}
