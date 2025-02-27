
        using HomeDoctorSolution.Models;
        using HomeDoctorSolution.Repository;
        using HomeDoctorSolution.Services.Interfaces;
        using HomeDoctorSolution.Util;
        using HomeDoctorSolution.Util.Parameters;
        using HomeDoctorSolution.Models.ViewModels;
        using System;
        using System.Collections.Generic;
        using System.Threading.Tasks;
        
        namespace HomeDoctorSolution.Services
        {
            public class QuestionService : IQuestionService
            {
                IQuestionRepository questionRepository;
                public QuestionService(
                    IQuestionRepository _questionRepository
                    )
                {
                    questionRepository = _questionRepository;
                }
                public async Task Add(Question obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await questionRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = questionRepository.Count();
                    return result;
                }
        
                public async Task Delete(Question obj)
                {
                    obj.Active = 0;
                    await questionRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await questionRepository.DeletePermanently(id);
                }
        
                public async Task<Question> Detail(int? id)
                {
                    return await questionRepository.Detail(id);
                }
        
                public async Task<List<Question>> List()
                {
                    return await questionRepository.List();
                }
        
                public async Task<List<Question>> ListPaging(int pageIndex, int pageSize)
                {
                    return await questionRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<QuestionViewModel>> ListServerSide(QuestionDTParameters parameters)
                {
                    return await questionRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Question>> Search(string keyword)
                {
                    return await questionRepository.Search(keyword);
                }
        
                public async Task Update(Question obj)
                {
                    await questionRepository.Update(obj);
                }
            }
        }
    
    