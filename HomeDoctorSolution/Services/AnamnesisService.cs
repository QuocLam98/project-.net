
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
    public class AnamnesisService : IAnamnesisService
    {
        IAnamnesisRepository anamnesisRepository;
        public AnamnesisService(
            IAnamnesisRepository _anamnesisRepository
            )
        {
            anamnesisRepository = _anamnesisRepository;
        }
        public async Task Add(Anamnesis obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await anamnesisRepository.Add(obj);
        }

        public int Count()
        {
            var result = anamnesisRepository.Count();
            return result;
        }

        public async Task Delete(Anamnesis obj)
        {
            obj.Active = 0;
            await anamnesisRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await anamnesisRepository.DeletePermanently(id);
        }

        public async Task<Anamnesis> Detail(int? id)
        {
            return await anamnesisRepository.Detail(id);
        }

        public async Task<List<Anamnesis>> List()
        {
            return await anamnesisRepository.List();
        }

        public async Task<List<Anamnesis>> ListPaging(int pageIndex, int pageSize)
        {
            return await anamnesisRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<AnamnesisViewModel>> ListServerSide(AnamnesisDTParameters parameters)
        {
            return await anamnesisRepository.ListServerSide(parameters);
        }

        public async Task<List<Anamnesis>> Search(string keyword)
        {
            return await anamnesisRepository.Search(keyword);
        }

        public async Task Update(Anamnesis obj)
        {
            await anamnesisRepository.Update(obj);
        }
        public async Task<HomeDoctorResponse> AddOrUpdateAnamnessis(Anamnesis model)
        {
            try
            {
                var checkExistAcc = await anamnesisRepository.CheckExistByAccountId(model.AccountId);
                if (checkExistAcc)
                {
                    var obj = await anamnesisRepository.DetailByAccountId(model.AccountId);
                    model.Id = obj[0].Id;
                    model.CreatedTime = DateTime.Now;
                    await anamnesisRepository.Update(model);
                    var homeDoctorResponse = HomeDoctorResponse.SUCCESS(model);
                    return homeDoctorResponse;
                }
                else
                {
                    model.Active = 1;
                    model.CreatedTime = DateTime.Now;
                    await anamnesisRepository.Add(model);
                    var homeDoctorResponse = HomeDoctorResponse.SUCCESS(model);
                    return homeDoctorResponse;
                }
            }
            catch (Exception e)
            {
                var homeDoctorResponse = HomeDoctorResponse.BAD_REQUEST(e.Message);
                return homeDoctorResponse;
            }
        }
    }
}

