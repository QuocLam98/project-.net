using FirebaseAdmin.Messaging;
using HomeDoctor.Models.ModelDTO;
using HomeDoctor.Services;
using HomeDoctor.Services.Interfaces;
using HomeDoctorSolution.Models.ModelDTO;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Net;

namespace HomeDoctor.Util.Extensions
{
    public class FCMExtentions
    {
        private IAccountService _accountService;
        private static string serverKey = "AAAAs1_TAhI:APA91bGzUngmm2e3BN6A0cOFwvV0x-8U9Pcebclizx_V59_2UHhrMzn_ENngBuYq4nFJoQ7EHx0qjFi7CNFCne6Pyq_34tc8k1C8drP3uWVvJSrDteMNM9wPrrOCu9HIZM7k99rNaub3";
        private static string senderId = "770406810130";
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
            var payload = new Object();
            if (key == SystemConstant.SEND_MESSAGE)
            {
                payload = new
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
            }
            else
            {
                payload = new
                {
                    to = DeviceToken,
                    //priority = "high",
                    //content_available = true,
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
            }
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
            var payload = new Object();
            if (key == SystemConstant.SEND_MESSAGE)
            {
                payload = new
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
            }
            else
            {
                payload = new
                {
                    to = DeviceToken,
                    //priority = "high",
                    //content_available = true,
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
            }

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
