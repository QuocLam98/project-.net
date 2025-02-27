
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
        public interface IQuestionRepository
        {
            Task <List< Question>> List();

            Task <List< Question>> Search(string keyword);

            Task <List< Question>> ListPaging(int pageIndex, int pageSize);

            Task <Question> Detail(int ? postId);

            Task <Question> Add(Question Question);

            Task Update(Question Question);

            Task Delete(Question Question);

            Task <int> DeletePermanently(int ? QuestionId);

            int Count();

            Task <DTResult<QuestionViewModel>> ListServerSide(QuestionDTParameters parameters);
        }
    }
