
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
    public class NotificationService : INotificationService
    {
        INotificationRepository notificationRepository;
        IAccountRepository accountRepository;
        public NotificationService(
            INotificationRepository _notificationRepository,
            IAccountRepository _accountRepository
            )
        {
            notificationRepository = _notificationRepository;
            accountRepository = _accountRepository;
        }
        public async Task Add(Notification obj)
        {
            obj.Active = 1;
            obj.CreatedTime = DateTime.Now;
            await notificationRepository.Add(obj);
        }

        public int Count()
        {
            var result = notificationRepository.Count();
            return result;
        }

        public async Task Delete(Notification obj)
        {
            obj.Active = 0;
            await notificationRepository.Delete(obj);
        }

        public async Task<int> DeletePermanently(int? id)
        {
            return await notificationRepository.DeletePermanently(id);
        }

        public async Task<Notification> Detail(int? id)
        {
            return await notificationRepository.Detail(id);
        }

        public async Task<List<Notification>> List()
        {
            return await notificationRepository.List();
        }

        public async Task<List<Notification>> ListPaging(int pageIndex, int pageSize)
        {
            return await notificationRepository.ListPaging(pageIndex, pageSize);
        }

        public async Task<DTResult<NotificationViewModel>> ListServerSide(NotificationDTParameters parameters)
        {
            return await notificationRepository.ListServerSide(parameters);
        }

        public async Task<List<Notification>> Search(string keyword)
        {
            return await notificationRepository.Search(keyword);
        }

        public async Task Update(Notification obj)
        {
            await notificationRepository.Update(obj);
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 15/12/2023
        /// </summary>
        /// <returns>Push notificaiton</returns>
        public async Task<HomeDoctorResponse> SetDeviceFireBase(FireBaseDTO obj, int accountId)
        {

            //Kiểm tra đã tồn tại device trong account
            var accountObj = await accountRepository.Detail(accountId);
            //Nếu chưa có => update GuId trong Account
            if(accountObj.Id > 0)
            {
                accountObj.GuId = obj.DeviceToken;
                await accountRepository.SetDevice(accountObj);
                return HomeDoctorResponse.SUCCESS();

            }
            else
            {
                return HomeDoctorResponse.BAD_REQUEST();
            }
        }

        public async Task PushNotificationFCM(int accountId, string title, string msg, int? id, string? key)
        {
            string deviceToken = "";
            //Kiểm tra đã tồn tại device trong account
            var accountObj = await accountRepository.Detail(accountId);
            //Nếu chưa có => update GuId trong Account
            if (accountObj != null)
            {
                deviceToken = accountObj.GuId != null ? accountObj.GuId : "";
                FCMExtentions.SendNotification(deviceToken, title, msg, id, key);
            }
        }
        public async Task PushNotificationFCM2(int accountId, string title, string msg, int? id, string? key, CustomFirebaseDTO? customObj)
        {
            string deviceToken = "";
            //Kiểm tra đã tồn tại device trong account
            var accountObj = await accountRepository.Detail(accountId);
            //Nếu chưa có => update GuId trong Account
            if (accountObj != null)
            {
                deviceToken = accountObj.GuId != null ? accountObj.GuId : "";
                FCMExtentions.SendNotification2(deviceToken, title, msg, id, key, customObj);
            }
        }

    }
}

