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
        public static async Task SendPushNotifications(Notification model)
        {
            var lstTokens = DevicesManager.GetAllDevicesToken();

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

            SaveNotification(model.Title, model.Description);
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

        public static void SaveNotification(string Title, string Description)
        {
            using (var dbContext = new masjidayEntities())
            {
                dbContext.PushNotifications.Add(new PushNotification
                {
                    Description = Description,
                    TimeStamp = DateTime.Now,
                    Title = Title
                });
                dbContext.SaveChanges();
            }
        }
    }
}