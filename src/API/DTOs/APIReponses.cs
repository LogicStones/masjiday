﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DTOs
{
    public class PrayerTimingModel
    {
        public int MasjidId { get; set; }
        public string Fajar { get; set; }
        public string Zohar { get; set; }
        public string Asar { get; set; }
        public string Magrib { get; set; }
        public string Isha { get; set; }
        public string Juma { get; set; }
    }

    public class PushNotificationsModel
    {
        public int UnreadNotifications { get; set; }
        public List<NotificationDetails> Notifications { get; set; }
    }

    public class NotificationDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TimeStamp { get; set; }
        public string IsRead { get; set; } = "false";
    }
}