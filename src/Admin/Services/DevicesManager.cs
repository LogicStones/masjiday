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

        public static void UpdateDeviceToken(string deviceId, string deviceToken)
        {
            using (var dbContext = new masjidayEntities())
            {
                var device = dbContext.RegisteredDevices.Where(d => d.Id.Equals(deviceId)).FirstOrDefault();

                if (device != null)
                {
                    device.DeviceToken = deviceToken;
                    dbContext.SaveChanges();
                }
            }
        }

        public static void SetDeviceInfo(int masjidId, string deviceId, string deviceToken)
        {
            using (var dbContext = new masjidayEntities())
            {
                var device = dbContext.RegisteredDevices.Where(d => d.Id.Equals(deviceId)).FirstOrDefault();

            if (device != null)
            {
                device.MasjidId = masjidId;
                device.DeviceToken = deviceToken;
            }
            else
            {
                dbContext.RegisteredDevices.Add(new RegisteredDevice
                {
                    Id = deviceId,
                    MasjidId = masjidId,
                    DeviceToken = deviceToken
                });
            }

            dbContext.SaveChanges();
        } }
    }
}