using Admin.Services;
using API.DTOs;
using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/dashboard")]

    public class DashboardController : ApiController
    {
        ResponseEntity response = new ResponseEntity();
        masjidayEntities dbContext = new masjidayEntities();

        //[HttpGet]
        //[Route("countries")]
        //public HttpResponseMessage GetCountries()
        //{
        //    try
        //    {
        //        response.Error = false;
        //        response.Data = dbContext.Countries.ToList();
        //        response.Message = AppMessages.msgSuccess;
        //        return Request.CreateResponse(HttpStatusCode.OK, response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = true;
        //        response.Message = AppMessages.serverError;
        //        return Request.CreateResponse(HttpStatusCode.OK, response);
        //    }
        //}

        //[HttpGet]
        //[Route("cities")]
        //public HttpResponseMessage GetCities(int CountryId)
        //{
        //    try
        //    {
        //        if (CountryId <= 0)
        //        {
        //            response.Error = true;
        //            response.Message = AppMessages.invalidParameters;
        //            return Request.CreateResponse(HttpStatusCode.OK, response);
        //        }

        //        response.Error = false;
        //        response.Data = dbContext.Cities.Where(c => c.CountryId == CountryId).ToList();
        //        response.Message = AppMessages.msgSuccess;
        //        return Request.CreateResponse(HttpStatusCode.OK, response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = true;
        //        response.Message = AppMessages.serverError;
        //        return Request.CreateResponse(HttpStatusCode.OK, response);
        //    }
        //}

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
                response.Data = MasajidManager.GetMasajidByCityId(CityId);
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

                DevicesManager.SetDeviceInfo(model.MasjidId, model.DeviceId, model.DeviceToken);

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

                DevicesManager.UpdateDeviceToken(model.DeviceId, model.DeviceToken);

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

                response.Data = MasajidManager.GetPrayerTiming(MasjidId);
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
        [Route("notifications")]
        public HttpResponseMessage GetNotifications(int PageNo, int Length, string DeviceId)
        {
            try
            {
                if (PageNo < 0 || Length <= 0)
                {
                    response.Error = true;
                    response.Message = AppMessages.invalidParameters;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                response.Data = NotificationsManager.GetNotificatinsByDeviceId(PageNo, Length, DeviceId);
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
        [Route("read-notification")]
        public HttpResponseMessage ReadNotification([FromBody] ReadNotificationModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.DeviceId) || model.NotificationId < 0)
                {
                    response.Error = true;
                    response.Message = AppMessages.invalidParameters;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                NotificationsManager.MarkNotificationRead(model.DeviceId, model.NotificationId);
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
        [Route("send-prayer-notification")]
        public async Task<HttpResponseMessage> SendPrayerNotification()
        {
            try
            {
                await NotificationsManager.SendNotification();
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
    }
}