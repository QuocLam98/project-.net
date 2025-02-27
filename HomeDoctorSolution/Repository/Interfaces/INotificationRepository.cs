
    using HomeDoctorSolution.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HomeDoctorSolution.Util;
    using HomeDoctorSolution.Util.Parameters;
    using HomeDoctorSolution.Models.ViewModels;


    namespace HomeDoctorSolution.Repository
    {
        public interface INotificationRepository
        {
            Task <List< Notification>> List();

            Task <List< Notification>> Search(string keyword);

            Task <List< Notification>> ListPaging(int pageIndex, int pageSize);

            Task <Notification> Detail(int ? postId);

            Task <Notification> Add(Notification Notification);

            Task Update(Notification Notification);

            Task Delete(Notification Notification);

            Task <int> DeletePermanently(int ? NotificationId);

            int Count();

            Task <DTResult<NotificationViewModel>> ListServerSide(NotificationDTParameters parameters);
        }
    }
