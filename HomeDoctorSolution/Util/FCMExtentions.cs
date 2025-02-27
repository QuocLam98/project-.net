using FirebaseAdmin.Messaging;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Services;
using HomeDoctorSolution.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Net;

namespace HomeDoctorSolution.Util
{
    public class FCMExtentions
    {
        private IAccountService _accountService;
        private static string serverKey = "AAAAyyWDycY:APA91bHztpLoh3JVnMYS_RW0FlNPy3nmeFBaER5P_a7oZm_RYoWXxpNjh3A5okma35EPLLQmQlIAA4nv38fNujPb1VqlFXWx8lAumYzjBat-P-TFiHnzcDAAU6DmoTBhjJ5JW2x1YDq3";
        private static string senderId = "872507754950";
        private static string webAddr = "https://fcm.googleapis.com/fcm/send";

        public FCMExtentions(IAccountService accountService)
        {
            _accountService = accountService;

        }
        public static string SendNotification(string DeviceToken, string title, string msg, int? id, string? key)
        {

            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";

            var payload = new
            {
                to = DeviceToken,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = msg,
                    title = title,
                },
                data = new
                {
                    id = id,
                    key = key,
                }
            };
            var serializer = new JavaScriptSerializer();
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
        public static string SendNotification2(string DeviceToken, string title, string msg, int? id, string? key, CustomFirebaseDTO obj)
        {

            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";

            var payload = new
            {
                to = DeviceToken,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = msg,
                    title = title,
                },
                data = new
                {
                    id = id,
                    key = key,
                    roomId = obj.RoomId,
                    name = obj.Name,
                    photo = obj.Photo,
                    role = obj.Role,
                    accountRoomId = obj.AccoundRoomId
                }
            };
            var serializer = new JavaScriptSerializer();
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}
