
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
        public interface IOnlineStatusRepository
        {
            Task <List< OnlineStatus>> List();

            Task <List< OnlineStatus>> Search(string keyword);

            Task <List< OnlineStatus>> ListPaging(int pageIndex, int pageSize);

            Task <OnlineStatus> Detail(int ? postId);

            Task <OnlineStatus> Add(OnlineStatus OnlineStatus);

            Task Update(OnlineStatus OnlineStatus);

            Task Delete(OnlineStatus OnlineStatus);

            Task <int> DeletePermanently(int ? OnlineStatusId);

            int Count();

            Task <DTResult<OnlineStatusViewModel>> ListServerSide(OnlineStatusDTParameters parameters);
        }
    }
