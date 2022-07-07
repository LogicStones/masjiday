using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Services
{
    public class DevicesManager
    {
        public static List<string> GetAllDevicesToken() 
        {
            using (var dbContext = new masjidayEntities())
            {
                return dbContext.RegisteredDevices.Select(u => u.DeviceToken).ToList();
            }
        }
    }
}