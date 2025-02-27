
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
        public interface IMessageStatusRepository
        {
            Task <List< MessageStatus>> List();

            Task <List< MessageStatus>> Search(string keyword);

            Task <List< MessageStatus>> ListPaging(int pageIndex, int pageSize);

            Task <MessageStatus> Detail(int ? postId);

            Task <MessageStatus> Add(MessageStatus MessageStatus);

            Task Update(MessageStatus MessageStatus);

            Task Delete(MessageStatus MessageStatus);

            Task <int> DeletePermanently(int ? MessageStatusId);

            int Count();

            Task <DTResult<MessageStatus>> ListServerSide(MessageStatusDTParameters parameters);
        }
    }
