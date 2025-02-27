using HomeDoctorSolution.Models;
using HomeDoctorSolution.Models.ViewModel;

namespace HomeDoctorSolution.Services.Interfaces

{
    public interface IConsultantService : IBaseService<Consultant>
    {
        Task<Consultant> DetailByBookingId(int id);

        Task<ConsultantViewModal> DetailConsultant(int? id);
        Task<Consultant> DetailConsultantByBookingId(int id);
        Task<List<Consultant>> ListByBookingId(int bookingId);

        /// Author: HuyDQ
        /// Created: 08/11/2023
        /// Description: check tài khoản nào có thể xem được view màn phiếu đánh giá
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckAccountViewConsultant(int bookingId, int accountId);

        /// Author: HoanNK
        /// Created: 10/11/2023
        /// Description: Danh sách lịch sử đánh giá tư vấn viên
        /// </summary>
        /// <returns></returns>
        Task<List<ConsulantViewModel>> ListReviewConsultant(int counselorsId, int pageIndex, int pageSize);

        /// Author: HuyDQ
        /// Created: 09/11/2023
        /// Description: Gửi mail thông báo cho thanh thiếu niên sau khi có phiếu đánh giá 
        /// </summary>
        /// <returns></returns>
        Task<HomeDoctorResponse> SendEmailConsultantToAccount(int accountId, int counselorId, int bookingId);

        Task<List<Consultant>> ListConsultantByAccountId(int accountId);
    }
}
