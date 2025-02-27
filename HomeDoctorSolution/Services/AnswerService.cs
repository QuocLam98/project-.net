
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
            public class AnswerService : IAnswerService
            {
                IAnswerRepository answerRepository;
                public AnswerService(
                    IAnswerRepository _answerRepository
                    )
                {
                    answerRepository = _answerRepository;
                }
                public async Task Add(Answer obj)
                {
                    obj.Active = 1;
                    obj.CreatedTime = DateTime.Now;
                    await answerRepository.Add(obj);
                }
        
                public int Count()
                {
                    var result = answerRepository.Count();
                    return result;
                }
        
                public async Task Delete(Answer obj)
                {
                    obj.Active = 0;
                    await answerRepository.Delete(obj);
                }
        
                public async Task<int> DeletePermanently(int? id)
                {
                    return await answerRepository.DeletePermanently(id);
                }
        
                public async Task<Answer> Detail(int? id)
                {
                    return await answerRepository.Detail(id);
                }
        
                public async Task<List<Answer>> List()
                {
                    return await answerRepository.List();
                }
        
                public async Task<List<Answer>> ListPaging(int pageIndex, int pageSize)
                {
                    return await answerRepository.ListPaging(pageIndex, pageSize);
                }
        
                public async Task<DTResult<AnswerViewModel>> ListServerSide(AnswerDTParameters parameters)
                {
                    return await answerRepository.ListServerSide(parameters);
                }
        
                public async Task<List<Answer>> Search(string keyword)
                {
                    return await answerRepository.Search(keyword);
                }
        
                public async Task Update(Answer obj)
                {
                    await answerRepository.Update(obj);
                }
            }
        }
    
    