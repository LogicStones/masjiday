﻿using API.DTOs;
using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/dashboard")]

    public class DashboardController : ApiController
    {
        ResponseEntity response = new ResponseEntity();
        masjidayEntities dbContext = new masjidayEntities();

        [HttpGet]
        [Route("countries")]
        public HttpResponseMessage GetCountries()
        {
            try
            {


                response.Error = false;
                response.Data = dbContext.Countries.ToList();
                response.Message = AppMessages.msgSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = AppMessages.serverError;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("cities")]
        public HttpResponseMessage GetCities(int CountryId)
        {
            try
            {


                if (CountryId <= 0)
                {
                    response.Error = true;
                    response.Message = AppMessages.invalidParameters;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.Error = false;
                response.Data = dbContext.Cities.Where(c => c.CountryId == CountryId).ToList();
                response.Message = AppMessages.msgSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = AppMessages.serverError;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("masajid")]
        public HttpResponseMessage GetMasajid(int CityId)
        {
            try
            {


                if (CityId <= 0)
                {
                    response.Error = true;
                    response.Message = AppMessages.invalidParameters;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.Error = false;
                response.Data = dbContext.Masajids.Where(c => c.CityId == CityId).ToList();
                response.Message = AppMessages.msgSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = AppMessages.serverError;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPost]
        [Route("set-device-info")]
        public HttpResponseMessage SetDeviceInfo([FromBody] DeviceInfoModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.DeviceId) || string.IsNullOrEmpty(model.DeviceToken) || model.MasjidId <= 0)
                {
                    response.Error = true;
                    response.Message = AppMessages.invalidParameters;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                var device = dbContext.RegisteredDevices.Where(d => d.Id.Equals(model.DeviceId)).FirstOrDefault();

                if (device != null)
                {
                    device.MasjidId = model.MasjidId;
                    device.DeviceToken = model.DeviceToken;
                }
                else
                {
                    dbContext.RegisteredDevices.Add(new RegisteredDevice
                    {
                        Id = model.DeviceId,
                        MasjidId = model.MasjidId,
                        DeviceToken = model.DeviceToken
                    });
                }

                dbContext.SaveChanges();

                response.Error = false;
                response.Message = AppMessages.msgSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = AppMessages.serverError;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPost]
        [Route("update-device-token")]
        public HttpResponseMessage UpdateDeviceToken([FromBody] DeviceTokenModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.DeviceId) || string.IsNullOrEmpty(model.DeviceToken))
                {
                    response.Error = true;
                    response.Message = AppMessages.invalidParameters;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                var device = dbContext.RegisteredDevices.Where(d => d.Id.Equals(model.DeviceId)).FirstOrDefault();

                if (device != null)
                {
                    device.DeviceToken = model.DeviceToken;
                    dbContext.SaveChanges();
                }

                response.Error = false;
                response.Message = AppMessages.msgSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = AppMessages.serverError;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("prayer-timing")]
        public HttpResponseMessage GetPrayerTiming(int MasjidId)
        {
            try
            {
                if (MasjidId <= 0)
                {
                    response.Error = true;
                    response.Message = AppMessages.invalidParameters;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                var masjidTiming = dbContext.PrayerTimings.Where(c => c.MasjidId == MasjidId).FirstOrDefault();


                response.Error = false;
                response.Data = new PrayerTimingModel
                {
                    MasjidId = masjidTiming.MasjidId,
                    Fajar = masjidTiming.Fajar.Hours.ToString("00") + ":" + masjidTiming.Fajar.Minutes.ToString("00"),
                    Zohar = masjidTiming.Zohar.Hours.ToString("00") + ":" + masjidTiming.Fajar.Minutes.ToString("00"),
                    Asar = masjidTiming.Asar.Hours.ToString("00") + ":" + masjidTiming.Fajar.Minutes.ToString("00"),
                    Magrib = masjidTiming.Magrib.Hours.ToString("00") + ":" + masjidTiming.Fajar.Minutes.ToString("00"),
                    Isha = masjidTiming.Isha.Hours.ToString("00") + ":" + masjidTiming.Fajar.Minutes.ToString("00"),
                    Juma = masjidTiming.Juma.Hours.ToString("00") + ":" + masjidTiming.Fajar.Minutes.ToString("00")
                };
                response.Message = AppMessages.msgSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = AppMessages.serverError;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("notifications")]
        public HttpResponseMessage GetNotifications(int PageNo, int Length)
        {
            try
            {
                if (PageNo < 0 || Length <= 0)
                {
                    response.Error = true;
                    response.Message = AppMessages.invalidParameters;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                var lstNotifications = dbContext.PushNotifications.OrderByDescending(n => n.TimeStamp).Skip(PageNo * Length).Take(Length).ToList();

                var lstResponse = new List<PushNotificationModel>();

                foreach (var item in lstNotifications)
                {
                    lstResponse.Add(new PushNotificationModel
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Title = item.Title,
                        TimeStamp = item.TimeStamp.ToString("dd/MM/yyyy HH:mm")
                    });
                }

                response.Error = false;
                response.Data = lstResponse;
                response.Message = AppMessages.msgSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = AppMessages.serverError;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
    }

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
}
