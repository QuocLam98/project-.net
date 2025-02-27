
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeDoctorSolution.Models.ModelDTO;

namespace HomeDoctorSolution.Services
{
    public class TagService : ITagService
    {
        ITagRepository tagRepository;
        public TagService(
            ITagRepository _tagRepository
            )
        {
            tagRepository = _tagRepository;
        }
        public async Task Add(Tag obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await tagRepository.Add(obj);
        }

        public int Count()
        {
            var result = tagRepository.Count();
            return result;
        }

        public async Task Delete(Tag obj)
        {
            obj.Active = 0;
            await tagRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await tagRepository.DeletePermanently(id);
        }

        public async Task<Tag> Detail(int? id)
        {
            return await tagRepository.Detail(id);
        }

        public async Task<List<Tag>> List()
        {
            return await tagRepository.List();
        }

        public async Task<List<Tag>> ListPaging(int pageIndex, int pageSize)
        {
            return await tagRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<Tag>> ListServerSide(TagDTParameters parameters)
        {
            return await tagRepository.ListServerSide(parameters);
        }

        public async Task<List<Tag>> Search(string keyword)
        {
            return await tagRepository.Search(keyword);
        }

        public async Task Update(Tag obj)
        {
            await tagRepository.Update(obj);
        }
        public async Task<HomeDoctorResponse> AddTagForPostAsync(InsertTagDTO obj)
        {
            return await tagRepository.AddTagForPostAsync(obj);
        }
        public Task<object> ListSelectTagAsync(TagForSelect2Aggregates obj)
        {
            return tagRepository.ListSelectTagAsync(obj);
        }
    }
}

