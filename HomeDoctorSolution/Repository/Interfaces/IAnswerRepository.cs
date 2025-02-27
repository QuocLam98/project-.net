
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
        public interface IAnswerRepository
        {
            Task <List< Answer>> List();

            Task <List< Answer>> Search(string keyword);

            Task <List< Answer>> ListPaging(int pageIndex, int pageSize);

            Task <Answer> Detail(int ? postId);

            Task <Answer> Add(Answer Answer);

            Task Update(Answer Answer);

            Task Delete(Answer Answer);

            Task <int> DeletePermanently(int ? AnswerId);

            int Count();

            Task <DTResult<AnswerViewModel>> ListServerSide(AnswerDTParameters parameters);
        }
    }
