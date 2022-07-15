using Admin.Utilities;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Integrations
{
    public class CloudNotifications
    {
        //public static async Task SendNewOrderNotification(Dictionary<string, string> data, string DeviceToken)
        //{
        //    InitializeFirebaseApp();

        //    var message = new Message()
        //    {
        //        Webpush = new WebpushConfig
        //        {
        //            //Data = data,
        //            //Headers = data,
        //            Notification = new WebpushNotification
        //            {
        //                Title = data["title"],
        //                Icon = data["icon"],
        //                Body = data["body"],
        //                Renotify = true,
        //                Silent = false,
        //                RequireInteraction = true,
        //                TimestampMillis = 10000, // 10 seconds
        //                Tag = data["tag"]
        //            },
        //            FcmOptions = new WebpushFcmOptions
        //            {
        //                Link = data["link"]
        //            }
        //        },
        //        Token = DeviceToken,
        //        //Condition = "",
        //        //Topic = ""
        //        //Data = data,
        //        //Android = new AndroidConfig
        //        //{
        //        //    CollapseKey = "",
        //        //    FcmOptions = new AndroidFcmOptions
        //        //    {
        //        //        AnalyticsLabel = ""
        //        //    },
        //        //    Priority = Priority.High,
        //        //    RestrictedPackageName = "",
        //        //    TimeToLive = new TimeSpan (0, 0, 1, 0, 0),
        //        //    Data = data,
        //        //    Notification = new AndroidNotification
        //        //    {

        //        //    }
        //        //},
        //        //Apns = new ApnsConfig
        //        //{
        //        //    Aps = new Aps
        //        //    {

        //        //    },
        //        //    CustomData = new Dictionary<string, object>(),
        //        //    FcmOptions = new ApnsFcmOptions
        //        //    {
        //        //        AnalyticsLabel = "",
        //        //        ImageUrl = ""
        //        //    },
        //        //    Headers = data
        //        //},
        //        //FcmOptions = new FcmOptions
        //        //{
        //        //    AnalyticsLabel = ""
        //        //},
        //        //Notification = new Notification
        //        //{
        //        //    Title = "",
        //        //    Body = "",
        //        //    ImageUrl = ""
        //        //},                    
        //    };

        //    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        //}

        //public static async Task UniCastFCM(string body, string imageUrl, string title, Dictionary<string, string> data, string DeviceToken)
        //{
        //    InitializeFirebaseApp();

        //    var message = new Message()
        //    {
        //        Notification = new Notification
        //        {
        //            Body = body,
        //            Title = title,
        //            ImageUrl = imageUrl
        //        },
        //        Data = data,
        //        Apns = new ApnsConfig
        //        {
        //            Aps = new Aps
        //            {
        //                Badge = 0,
        //                Sound = "",
        //                ContentAvailable = true,
        //                MutableContent = true,
        //                //    { "body", "" },
        //                //    { "loc_key", fcmKey }
        //            }
        //        },
        //        Token = DeviceToken
        //    };

        //    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        //}

        //public static async Task MultiCastFCM(string body, string imageUrl, string title, Dictionary<string, string> data, List<string> DeviceTokens)
        //{
        //    InitializeFirebaseApp();

        //    var message = new MulticastMessage()
        //    {
        //        Notification = new Notification
        //        {
        //            Body = body,
        //            Title = title,
        //            ImageUrl = imageUrl
        //        },
        //        Data = data,
        //        Apns = new ApnsConfig
        //        {
        //            Aps = new Aps
        //            {
        //                Badge = 0,
        //                Sound = "",
        //                ContentAvailable = true,
        //                MutableContent = true,
        //                //    { "body", "" },
        //                //    { "loc_key", fcmKey }
        //            }
        //        },
        //        Tokens = DeviceTokens   // max 500 tokens
        //    };

        //    await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
        //}

        public static async Task BroadCastFCM(string body, string imageUrl, string title, Dictionary<string, string> data, List<string> DeviceTokens)
        {
            InitializeFirebaseApp();

            var message = new MulticastMessage()
            {
                Notification = new Notification
                {
                    Body = body,
                    Title = title,
                    ImageUrl = imageUrl
                },
                Data = data,
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        Badge = 0,
                        Sound = "",
                        ContentAvailable = true,
                        MutableContent = true,
                        //    { "body", "" },
                        //    { "loc_key", fcmKey }
                    }
                },
                Webpush = new WebpushConfig
                {
                    Notification = new WebpushNotification
                    {
                        Body = body,
                        Title = title,
                        Icon = imageUrl
                    }
                },
                Tokens = DeviceTokens // max 500 tokens
            };

            await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
        }

        private static void InitializeFirebaseApp()
        {
            try
            {

                var defaultApp = FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(AppDomain.CurrentDomain.BaseDirectory + "masjiday-firebase-adminsdk-service-account.json")
                });
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
    }
}
