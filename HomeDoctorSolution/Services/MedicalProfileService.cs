
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Parameters;
using HomeDoctorSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace HomeDoctorSolution.Services
{
    public class MedicalProfileService : IMedicalProfileService
    {
        IMedicalProfileRepository medicalProfileRepository;
        public MedicalProfileService(
            IMedicalProfileRepository _medicalProfileRepository
            )
        {
            medicalProfileRepository = _medicalProfileRepository;
        }
        public async Task Add(MedicalProfile obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await medicalProfileRepository.Add(obj);
        }

        public int Count()
        {
            var result = medicalProfileRepository.Count();
            return result;
        }

        public async Task Delete(MedicalProfile obj)
        {
            obj.Active = 0;
            await medicalProfileRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await medicalProfileRepository.DeletePermanently(id);
        }

        public async Task<MedicalProfile> Detail(int? id)
        {
            return await medicalProfileRepository.Detail(id);
        }

        public async Task<List<MedicalProfile>> List()
        {
            return await medicalProfileRepository.List();
        }

        public async Task<List<MedicalProfile>> ListPaging(int pageIndex, int pageSize)
        {
            return await medicalProfileRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<MedicalProfileViewModel>> ListServerSide(MedicalProfileDTParameters parameters)
        {
            return await medicalProfileRepository.ListServerSide(parameters);
        }

        public async Task<List<MedicalProfile>> Search(string keyword)
        {
            return await medicalProfileRepository.Search(keyword);
        }

        public async Task Update(MedicalProfile obj)
        {
            await medicalProfileRepository.Update(obj);
        }

        /// <summary>
        /// Author: TrungHieuTr
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<HomeDoctorResponse> AddOrUpdateMedicalProfile(MedicalProfileViewModel model)
        {
            try
            {
                //Mapper
                var configMedicalProfile = new MapperConfiguration(cfg => {
                    cfg.CreateMap<MedicalProfile, MedicalProfileViewModel>();
                });
                var medicalProfile = configMedicalProfile.CreateMapper().Map<MedicalProfile>(model);
                var checkExistAcc = await medicalProfileRepository.CheckExistByAccountId(model.AccountId);
                if (checkExistAcc)
                {
                    var obj = await medicalProfileRepository.DetailByAccountId(model.AccountId);
                    medicalProfile.Id = obj[0].Id;
                    medicalProfile.CreatedTime = DateTime.Now;
                    await medicalProfileRepository.Update(medicalProfile);
                    var homeDoctorResponse = HomeDoctorResponse.SUCCESS(medicalProfile);
                    return homeDoctorResponse;
                }
                else
                {
                    medicalProfile.Active = 1;
                    medicalProfile.CreatedTime = DateTime.Now;
                    await medicalProfileRepository.Add(medicalProfile);
                    var homeDoctorResponse = HomeDoctorResponse.SUCCESS(medicalProfile);
                    return homeDoctorResponse;
                }
            }
            catch(Exception ex)
            {
                var homeDoctor = HomeDoctorResponse.BadRequest(ex.Message);
                return homeDoctor;
            }
        }
        public async Task<List<MedicalProfileViewModel>> MedicalProfile(int? accountId)
        {
            return await medicalProfileRepository.MedicalProfile(accountId);
        }
    }
}

