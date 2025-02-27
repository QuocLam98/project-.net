using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeDoctorSolution.Util
{
    public class SystemConstant
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            return Regex.IsMatch(email, pattern);
        }

        public static string ACTIVATION_PAGE_URL = "activation.html";
        public static string DEFAULT_URL = "https://bacsitainha.vn/";
		public static string DEFAULT_URL_LOGO = "/homedoctor/img/header/header-logo.png";
		public static int ROLE_ANONYMOUS_USER = 0;
        public static int ROLE_SYSTEM_ADMIN = 1000001;
        public static string SECURITY_KEY_NAME = "happySecurityToken";
        public static int FORGOT_PASSSWORD_TOKEN_EXPRIED = 1;
        public static string FORGOT_PASSWORD_EMAIL = "FORGOT_PASSWORD_EMAIL";
        public static string REGISTER_ACCOUNT = "REGISTER_ACCOUNT";
        public static int CATEGORY_LIBRARY_ID = 1000066;
        public static int ACCOUNT_DEFAULT_BOOKING = 1000003;
        public static int CATEGORY_NEWS_ID = 1000080;

        #region Survey SurveySection 
        public static int SECTION_LOAU = 1000019;
        public static int SECTION_STRESS = 1000024;
        public static int SECTION_TRAMCAM = 1000020;
        public static int SURVEY_ID = 1000001;
        #endregion
        #region Doctor Status Value
        public static int DOCTOR_STATUS_ACTIVE = 1000001;
        public static int DOCTOR_STATUS_INACTIVE = 1000002;
        #endregion
        #region Account Status Value
        public static int ACCOUNT_STATUS_ACTIVE = 1000001;
		public static int ACCOUNT_STATUS_INACTIVE = 1000002;
		#endregion
		#region Account Type Value
		public static int ACCOUNT_TYPE_DEFAULT = 1000001;
		public static int ACCOUNT_TYPE_TEENAGER = 1000002;
		public static int ACCOUNT_TYPE_TEACHER = 1000003;
		#endregion
		#region Role Value
		public static int ROLE_SUPER_ADMIN = 1000001;
		public static int ROLE_USER = 1000002;
		public static int ROLE_DOCTOR = 1000003;
        public static int ROLE_CENSOR = 1000004;
        public static int ROLE_TEENAGER_MOD = 1000005;
        public static int ROLE_ADMIN_SCHOOL = 1000006;
        #endregion
        #region Notification
        public static int NOTIFICATION_STATUS_READ = 1000001;
        public static int NOTIFICATION_STATUS_UNREAD = 1000002;
        #endregion

        #region PostCommentStatus
        public static int POST_COMMENT_STATUS_COMMENT_ON = 1000001;
        public static int POST_COMMENT_STATUS_COMMENT_OFF = 1000002;
        #endregion

        #region PostPublishStatus
        public static int POST_PUBLISH_STATUS_PUBLIC = 1000001;
        public static int POST_PUBLISH_STATUS_DRAFT = 1000002;
        public static int POST_PUBLISH_STATUS_PRIVATE = 1000003;
        #endregion

        #region POST TYPE
        public static int POST_TYPE_NEW = 1000002;
        public static int POST_TYPE_TEXT = 1000001;
        #endregion

        #region POST LAYOUT
        public static int POST_LAYOUT_DEFAULT = 1000001;
        #endregion

        #region Message Status
        public static int MESSAGE_STATUS_SENDED = 1000001;
        #endregion
        #region Message TYPE
        public static int MESSAGE_TYPE_TEXT = 1000001;
        #endregion
        #region FORUM POST TYPE
        public static int FORUM_POST_TYPE_DEFAULT = 1000001;
        public static int FORUM_POST_TYPE_INCOGNITO = 1000002;
        #endregion
        #region FORUM POST STATUS
        public static int FORUM_POST_STATUS_CONFIRM = 1000001;
        public static int FORUM_POST_STATUS_WAITING = 1000002;
        public static int FORUM_POST_STATUS_CANCELED = 1000003;
        #endregion

        #region FORUM POST KEY
        public static string FORUM_POST_KEY_WAITING = "FORUM_POST_KEY_WAITING";
        public static string FORUM_POST_KEY_CANCEL = "FORUM_POST_KEY_CANCEL";
        public static string FORUM_POST_KEY_CONFIRM = "FORUM_POST_KEY_CONFIRM";
        #endregion

        #region NOTI_KEY
        public static string BOOKING_SUCCESS = "BOOKING_SUCCESS";
        public static string BOOKING_WAITING = "BOOKING_WAITING";
        public static string BOOKING_USER_CONFIRM = "BOOKING_USER_CONFIRM";
        public static string BOOKING_CONSELOR_CONFIRM = "BOOKING_CONSELOR_CONFIRM";
        public static string BOOKING_CONSELOR_CANCEL = "BOOKING_CONSELOR_CANCEL";
        public static string BOOKING_CONSELOR_REJECT = "BOOKING_CONSELOR_REJECT";
        public static string BOOKING_USER_REJECT = "BOOKING_USER_REJECT";
        public static string BOOKING_REMIND_WAITING_CONSELOR = "BOOKING_REMIND_WAITING_CONSELOR";
        public static string BOOKING_REMIND_WAITING_USER = "BOOKING_REMIND_WAITING_USER";
        public static string BOOKING_VOTE_BY_USER = "BOOKING_VOTE_BY_USER";
        public static string BOOKING_DONE = "BOOKING_DONE";
        public static string NEW_COMMENT_FORUM_POST = "NEW_COMMENT_FORUM_POST";
        public static string SURVEY_COMPLETE = "SURVEY_COMPLETE";

        #endregion

        #region BOOKING_TYPE
        public static int BOOKING_TYPE_ONLINE = 1000001;
        public static int BOOKING_TYPE_OFFLINE = 1000002;
        #endregion

        #region BOOKING_STATUS
        public static int BOOKING_STATUS_WAIT_ACCEPT = 1000001;//chờ
        public static int BOOKING_STATUS_WAIT_DONE = 1000002;//đã xếp lịch
        public static int BOOKING_STATUS_CANCEL = 1000004;//hủy
        public static int BOOKING_STATUS_REJECT = 1000005;//từ chối
        public static int BOOKING_STATUS_DONE = 1000003;//hoàn thành
        public static int BOOKING_STATUS_CONFIRM = 1000006;//đã xét duyệt
        public static int BOOKING_STATUS_WAIT_DONE_AND_WAIT_ACCEPT = 102;
        #endregion

        #region Chatbot
        public static int BOT_ACCOUNT_ID = 1000001;
        public static string CHAT_BOT_URL = "https://api.mindmaid.ai/v1/answers";
        public static string BOT_ID = "be15b219-be0e-4654-8b49-4d2ddc460eda";
        public static string IS_SSE = "false";
        #endregion

        #region ChatbotGPT
        public static string THREAD_BOT_URL = "https://api.openai.com/v1/threads";
        public static string VERSION_OPENAI ="assistants=v1";
        public static string TOKEN_OPEN_AI = "Bearer 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";
        public static string MESSAGE_ROLE = "user";
        public static string ASSISTANT_ID = "asst_pFSrhx8JKC5meXHvWdfENqcW";
        public static string THREAD_ID = "thread_4jAYOMdhHIGiG9N49sbCCusP";
        #endregion
        #region OrderDetailStatus
        public static int ORDER_DETAIL_STATUS_DEFAULT = 1000001;
        #endregion

        public static string SEND_MESSAGE = "SEND_MESSAGE";

        //Service
        public static int SERVICE_TEST_PARENT = 1000015;
    }
}
