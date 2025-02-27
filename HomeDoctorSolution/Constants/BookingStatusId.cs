namespace HomeDoctorSolution.Constants
{
    public class BookingStatusId
    {
        //chờ xếp lịch
        public const int WAIT = 1000001;
        //đã xếp lịch
        public const int ACCEPT = 1000002;
        //tư vấn thành công
        public const int SUCCESS = 1000003;
        //Hủy
        public const int CANCEL = 1000004;
        //Từ chối
        public const int REJECT = 1000005;
        //Đã xét duyệt
        public const int CONFIRM = 1000006;

        public const int SUCCESS_WAIT = 102;

        public static string TEXT_CONFIRM = "Tôi đã xác nhận";
    }
}
