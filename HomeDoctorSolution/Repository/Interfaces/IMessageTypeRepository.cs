
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
        public interface IMessageTypeRepository
        {
            Task <List< MessageType>> List();

            Task <List< MessageType>> Search(string keyword);

            Task <List< MessageType>> ListPaging(int pageIndex, int pageSize);

            Task <MessageType> Detail(int ? postId);

            Task <MessageType> Add(MessageType MessageType);

            Task Update(MessageType MessageType);

            Task Delete(MessageType MessageType);

            Task <int> DeletePermanently(int ? MessageTypeId);

            int Count();

            Task <DTResult<MessageType>> ListServerSide(MessageTypeDTParameters parameters);
        }
    }
