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
    public static class MasajidManager
    {
        public static CallBackData FetchAllMasajid(Paging paging)
        {
            CallBackData callBackData = new CallBackData();

            try
            {
                CommonFilters company = paging.SearchJson.Deserialize<CommonFilters>();
                using (var dbContext = new masjidayEntities())
                {
                    var masajid = dbContext.FetchMasajidForAdmin(paging.DisplayLength, paging.DisplayStart, paging.SortColumn, paging.SortOrder, company.Search).ToList();
                    
                    //orders.Select(o => { o.TotalAmount = (o.TotalActualAmount + o.TotalExtraAmount).ToString("0.00"); return o; }).ToList();

                    callBackData = masajid.ToDataTable(paging);
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

        public static void SaveMasjid(Masjid model)
        {
            using (var dbContext = new masjidayEntities())
            {
                var masjid = new Masajid
                {
                    Name = model.Name,
                    OffSet = model.OffSet,
                    CityId = 1
                };

                dbContext.Masajids.Add(masjid);
                dbContext.SaveChanges();

                dbContext.PrayerTimings.Add(new PrayerTiming
                {
                    MasjidId = masjid.Id,
                    Isha = model.Isha,
                    Asar = model.Asar,
                    Fajar = model.Fajar,
                    Juma = model.Juma,
                    Magrib = model.Magrib,
                    Zohar = model.Zohar
                });
                dbContext.SaveChanges();
            }
        }

        public static void UpdateMasjid(Masjid model)
        {
            using (var dbContext = new masjidayEntities())
            {
                var masjid = dbContext.Masajids.Where(m => m.Id == model.Id).FirstOrDefault();
                var timing = dbContext.PrayerTimings.Where(pt => pt.MasjidId == masjid.Id).FirstOrDefault();

                masjid.Name = model.Name;
                masjid.OffSet = model.OffSet;
                masjid.CityId = 1;

                timing.Isha = model.Isha;
                timing.Asar = model.Asar;
                timing.Fajar = model.Fajar;
                timing.Juma = model.Juma;
                timing.Magrib = model.Magrib;
                timing.Zohar = model.Zohar;

                dbContext.SaveChanges();
            }
        }

        public static Masjid GetMasjidById(int id)
        {
            using (var dbContext = new masjidayEntities())
            {
                return (from m in dbContext.Masajids
                        join pt in dbContext.PrayerTimings on m.Id equals pt.MasjidId
                        where m.Id == id
                        select new Masjid
                        {
                            Id = id,
                            Name = m.Name,
                            OffSet = m.OffSet,
                            Fajar = pt.Fajar,
                            Zohar = pt.Zohar,
                            Asar = pt.Asar,
                            Magrib = pt.Magrib,
                            Isha = pt.Isha,
                            Juma = pt.Juma
                        }).FirstOrDefault();
            }
        }

        public static List<Audiance> GetAllMasjid()
        {
            using (var dbContext = new masjidayEntities())
            {
                return (from m in dbContext.Masajids
                        select new Audiance
                        {
                            Id = m.Id,
                            Name = m.Name
                        }).ToList();
            }
        }

    }
}