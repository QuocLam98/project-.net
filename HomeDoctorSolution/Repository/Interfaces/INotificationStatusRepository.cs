
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
        public interface INotificationStatusRepository
        {
            Task <List< NotificationStatus>> List();

            Task <List< NotificationStatus>> Search(string keyword);

            Task <List< NotificationStatus>> ListPaging(int pageIndex, int pageSize);

            Task <NotificationStatus> Detail(int ? postId);

            Task <NotificationStatus> Add(NotificationStatus NotificationStatus);

            Task Update(NotificationStatus NotificationStatus);

            Task Delete(NotificationStatus NotificationStatus);

            Task <int> DeletePermanently(int ? NotificationStatusId);

            int Count();

            Task <DTResult<NotificationStatus>> ListServerSide(NotificationStatusDTParameters parameters);
        }
    }
