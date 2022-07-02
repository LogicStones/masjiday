using DatabaseModel;
using System;
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

                response.Error = false;
                response.Data = dbContext.PrayerTimings.Where(c => c.MasjidId == MasjidId).FirstOrDefault();
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

                response.Error = false;
                response.Data = dbContext.PushNotifications.OrderByDescending(n => n.TimeStamp).Skip(PageNo * Length).Take(Length).ToList();
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
