
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System.Threading.Tasks;
using HomeDoctorSolution.Models.ModelDTO;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface INotificationService : IBaseService<Notification>
    {
        Task<DTResult<NotificationViewModel>> ListServerSide(NotificationDTParameters parameters);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>Push notificaiton</returns>
        Task PushNotificationFCM(int accountId, string title, string msg, int? id, string? key);

        Task<HomeDoctorResponse> SetDeviceFireBase(FireBaseDTO obj, int accountId);
    }
}
