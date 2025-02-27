
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
            public class QuestionTypeService : IQuestionTypeService
            {
                IQuestionTypeRepository questionTypeRepository;
                public QuestionTypeService(
                    IQuestionTypeRepository _questionTypeRepository
                    )
                {
                    questionTypeRepository = _questionTypeRepository;
                }
                public async Task Add(QuestionType obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await questionTypeRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = questionTypeRepository.Count();
                    return result;
                }
        
                public async Task Delete(QuestionType obj)
                {
                    obj.Active = 0;
                    await questionTypeRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await questionTypeRepository.DeletePermanently(id);
                }
        
                public async Task<QuestionType> Detail(int? id)
                {
                    return await questionTypeRepository.Detail(id);
                }
        
                public async Task<List<QuestionType>> List()
                {
                    return await questionTypeRepository.List();
                }
        
                public async Task<List<QuestionType>> ListPaging(int pageIndex, int pageSize)
                {
                    return await questionTypeRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<QuestionType>> ListServerSide(QuestionTypeDTParameters parameters)
                {
                    return await questionTypeRepository.ListServerSide(parameters);
                }
        
                public async Task<List<QuestionType>> Search(string keyword)
                {
                    return await questionTypeRepository.Search(keyword);
                }
        
                public async Task Update(QuestionType obj)
                {
                    await questionTypeRepository.Update(obj);
                }
            }
        }
    
    