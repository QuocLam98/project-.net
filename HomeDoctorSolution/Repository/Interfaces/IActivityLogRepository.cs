
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
        public interface IActivityLogRepository
        {
            Task <List< ActivityLog>> List();

            Task <List< ActivityLog>> Search(string keyword);

            Task <List< ActivityLog>> ListPaging(int pageIndex, int pageSize);

            Task <ActivityLog> Detail(int ? postId);

            Task <ActivityLog> Add(ActivityLog ActivityLog);

            Task Update(ActivityLog ActivityLog);

            Task Delete(ActivityLog ActivityLog);

            Task <int> DeletePermanently(int ? ActivityLogId);

            int Count();

            Task <DTResult<ActivityLogViewModel>> ListServerSide(ActivityLogDTParameters parameters);
        }
    }
