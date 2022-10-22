using Admin.Datatable;
using Admin.Integrations;
using Admin.Models;
using Admin.Utilities;
using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Admin.Services
{
    public static class NotificationsManager
    {

        #region Admin

        public static async Task SendPushNotifications(Notification model)
        {
            var lstTokens = DevicesManager.GetAllDevicesToken(model.MasajidIds);
            if (lstTokens.Any())
            {
                await CloudNotifications.BroadCastFCM
                    (
                        model.Description,
                        ConfigurationManager.AppSettings["LogoURL"],
                        model.Title,
                        new Dictionary<string, string>
                                {
                                { "MessageKey", "admin_new_notification" }
                                },
                        lstTokens
                    );
            }
            SaveMasjidNotification(SaveNotification(model.Title, model.Description), model.MasajidIds);
        }

        public static CallBackData FetchAllNotifications(Paging paging)
        {
            CallBackData callBackData = new CallBackData();

            try
            {
                CommonFilters company = paging.SearchJson.Deserialize<CommonFilters>();
                using (var dbContext = new masjidayEntities())
                {
                    var notifications = dbContext.FetchNotificationsForAdmin(paging.DisplayLength, paging.DisplayStart, paging.SortColumn, paging.SortOrder, company.Search).ToList();

                    //orders.Select(o => { o.OrderDate = o.BookingDateTime.AddMinutes(o.TimeZoneDifferenceInMinutes).ToString("D", CultureInfo.CreateSpecificCulture("de-DE")); return o; }).ToList();
                    //orders.Select(o => { o.PaymentMethod = Enum.GetName(typeof(PaymentMethods), o.PaymentMethodID).ToString(); return o; }).ToList();
                    //orders.Select(o => { o.TotalAmount = (o.TotalActualAmount + o.TotalExtraAmount).ToString("0.00"); return o; }).ToList();

                    callBackData = notifications.ToDataTable(paging);
                }
            }
            catch (Exception ex)
            {
                callBackData.msg.Success = false;
                callBackData.msg.MessageDetail = Message.ErrorMessage;
                Logger.WriteLog(ex);
            }
            return callBackData;
        }

        public static int SaveNotification(string Title, string Description)
        {
            using (var dbContext = new masjidayEntities())
            {
                var notification = new PushNotification
                {
                    Description = Description,
                    TimeStamp = DateTime.UtcNow,
                    Title = Title
                };
                dbContext.PushNotifications.Add(notification);
                dbContext.SaveChanges();
                return notification.Id;
            }
        }

        public static void SaveMasjidNotification(int NotificationId, string MasajidIds)
        {
            using (var dbContext = new masjidayEntities())
            {
                var masajid = MasajidIds.Split(',').ToList();
                foreach (var m in masajid)
                {
                    dbContext.MasjidNotifications.Add(new MasjidNotification
                    {
                        MasjidId = int.Parse(m),
                        NotificationId = NotificationId
                    });
                }
                dbContext.SaveChanges();
            }
        }

        #endregion

        #region API

        public static PushNotificationsModel GetNotificatinsByDeviceId(int PageNo, int Length, string DeviceId)
        {
            using (var dbContext = new masjidayEntities())
            {
                var masjidId = dbContext.RegisteredDevices.Where(rd => rd.Id.Equals(DeviceId)).FirstOrDefault().MasjidId;
                var masjid = dbContext.Masajids.Where(m => m.Id == masjidId).FirstOrDefault();
                var totalNotifications = dbContext.MasjidNotifications.Where(mn => mn.MasjidId == masjidId).Count();

                var readNotifications = (from rn in dbContext.ReadNotifications
                                         join mn in dbContext.MasjidNotifications on rn.NotificationId equals mn.NotificationId
                                         where rn.DeviceId == DeviceId && mn.MasjidId == masjidId
                                         select rn).Count();

                var lstNotifications = (from pn in dbContext.PushNotifications
                                        join mn in dbContext.MasjidNotifications on pn.Id equals mn.NotificationId
                                        where mn.MasjidId == masjidId
                                        orderby pn.TimeStamp descending
                                        select pn).Skip(PageNo * Length).Take(Length).ToList();

                var lstResponse = new List<NotificationDetails>();

                foreach (var item in lstNotifications)
                {
                    lstResponse.Add(new NotificationDetails
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Title = item.Title,
                        TimeStamp = item.TimeStamp.AddMinutes(masjid.OffSet).ToString("dd/MM/yyyy HH:mm")
                    });
                }

                var lstReadNotifications = dbContext.ReadNotifications.Where(d => d.DeviceId.Equals(DeviceId)).ToList();

                if (lstReadNotifications.Any())
                {
                    foreach (var n in lstResponse)
                    {
                        foreach (var r in lstReadNotifications)
                        {
                            if (r.NotificationId == n.Id)
                                n.IsRead = "true";
                        }
                    }
                }

                return new PushNotificationsModel
                {
                    Notifications = lstResponse,
                    UnreadNotifications = totalNotifications - readNotifications
                };
            }
        }

        public static void MarkNotificationRead(string deviceId, int notificationId)
        {
            using (var dbContext = new masjidayEntities())
            {
                if (!dbContext.ReadNotifications.Where(rn => rn.NotificationId == notificationId && rn.DeviceId.Equals(deviceId)).Any())
                {
                    var device = dbContext.ReadNotifications.Add(new ReadNotification
                    {
                        DeviceId = deviceId,
                        NotificationId = notificationId
                    });

                    dbContext.SaveChanges();
                }
            }
        }

        public static async Task SendNotification()
        {
            using (var dbContext = new masjidayEntities())
            {
                var lstMasajid = dbContext.Masajids.ToList();
                var today = DateTime.UtcNow.Date;
                var dbPrayerTimings = dbContext.PrayerTimings.Where(pt => pt.Date == today).ToList();
                var dbRegisteredDevices = dbContext.RegisteredDevices.ToList();

                foreach (var m in lstMasajid)
                {
                    string prayerName = string.Empty;
                    int minutesDifference = int.Parse(ConfigurationManager.AppSettings["PreNotificationInterval"].ToString());
                    var currentTime = DateTime.UtcNow.AddMinutes(m.OffSet).TimeOfDay;

                    var lstRegisteredDevices = dbRegisteredDevices.Where(rd => rd.MasjidId == m.Id).ToList();

                    if (lstRegisteredDevices.Any())
                    {
                        var prayerTiming = dbPrayerTimings.Where(pt => pt.MasjidId == m.Id).FirstOrDefault();

                        //data issue, every masjid must have prayer timing. Following check can be removed.
                        if (prayerTiming == null)
                            continue;

                        if (prayerTiming.Fajar.Subtract(currentTime).TotalMinutes > 0 && prayerTiming.Fajar.Subtract(currentTime).TotalMinutes <= minutesDifference)
                            prayerName = "Fajar";
                        else if ((prayerTiming.Juma.Subtract(currentTime).TotalMinutes > 0 && prayerTiming.Juma.Subtract(currentTime).TotalMinutes <= minutesDifference && DateTime.Now.DayOfWeek == DayOfWeek.Friday) ||
                            (prayerTiming.Zohar.Subtract(currentTime).TotalMinutes > 0 && prayerTiming.Zohar.Subtract(currentTime).TotalMinutes <= minutesDifference))
                            prayerName = DateTime.Now.DayOfWeek == DayOfWeek.Friday ? "Juma" : "Zohar";
                        else if (prayerTiming.Asar.Subtract(currentTime).TotalMinutes > 0 && prayerTiming.Asar.Subtract(currentTime).TotalMinutes <= minutesDifference)
                            prayerName = "Asar";
                        else if (prayerTiming.Magrib.Subtract(currentTime).TotalMinutes > 0 && prayerTiming.Magrib.Subtract(currentTime).TotalMinutes <= minutesDifference)
                            prayerName = "Magrib";
                        else if (prayerTiming.Isha.Subtract(currentTime).TotalMinutes > 0 && prayerTiming.Isha.Subtract(currentTime).TotalMinutes <= minutesDifference)
                            prayerName = "Isha";

                        if (!string.IsNullOrEmpty(prayerName))
                        {
                            var lstDeviceTokens = lstRegisteredDevices.Select(rd => rd.DeviceToken).ToList();

                            if (lstDeviceTokens.Any())
                                await BroadCastNotifications(m.Name, prayerName + " Prayer after 15 minutes.", lstDeviceTokens);
                        }
                    }
                }
            }
        }

        private static async Task BroadCastNotifications(string title, string body, List<string> lstDevicesToken)
        {
            await CloudNotifications.BroadCastFCM
                (
                    body,
                    ConfigurationManager.AppSettings["LogoURL"],
                    title,
                    new Dictionary<string, string>
                            {
                                { "MessageKey", "prayer_alert" }
                            },
                    lstDevicesToken
                );
        }

        #endregion

    }
}