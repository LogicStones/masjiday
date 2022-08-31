using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Services
{
    public class DevicesManager
    {
        public static List<string> GetAllDevicesToken(string masajidIds) 
        {
            using (var dbContext = new masjidayEntities())
            {
                var lstMasajid = masajidIds.Split(',').ToList();
                return dbContext.RegisteredDevices.Where(rd => lstMasajid.Contains(rd.MasjidId.ToString())).Select(u => u.DeviceToken).ToList();
            }
        }
    }
}