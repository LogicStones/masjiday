using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DTOs
{
    public class DeviceInfoModel
    {
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public int MasjidId { get; set; }
    }

    public class DeviceTokenModel
    {
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
    }

    public class ReadNotificationModel
    {
        public string DeviceId { get; set; }
        public int NotificationId { get; set; }
    }
}