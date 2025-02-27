using HomeDoctorSolution.Models.ViewModel;
using HomeDoctorSolution.Models;

namespace HomeDoctorSolution.Repository.Interfaces
{
    public interface IConsultantRepository
    {
        Task<Consultant> Add(Consultant consultant);
        Task<ConsultantViewModal> DetailConsultant(int? id);
        Task<Consultant> Detail(int? id);
        Task<List<Consultant>> ListByBookingId(int bookingId);
        Task<Consultant> DetailConsultantByBookingId(int id);
        Task Update(Consultant Consultant);
        Task<List<Consultant>> GetRatingOfCounselor();
        Task<Consultant> DetailByBookingId(int id);
        Task<List<ConsulantViewModel>> ListReviewConsultant(int counselorsId, int pageIndex, int pageSize);

        /// Author: HuyDQ
        /// Created: 08/11/2023
        /// Description: check tài khoản nào có thể xem được view màn phiếu đánh giá
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckAccountViewConsultant(int bookingId, int accountId);
        Task<List<Consultant>> ListConsultantByAccountId(int accountId);
        Task<int> DeletePermanently(int? objId);
    }
}
