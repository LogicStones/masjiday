using Admin.Datatable;
using Admin.Models;
using Admin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new Notification());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendNotification(Notification model)
        {
            await NotificationsManager.SendPushNotifications(model);
            TempData["Message"] = "Notification sent successfully.";

            return RedirectToAction("Index");
        }

        public ActionResult History()
        {
            return View(new Notification());
        }
        
        public JsonResult FetchNotifications()
        {
            return Json(NotificationsManager.FetchAllNotifications(Request.FetchPaging()), JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult ViewOrderDetails(Guid OrderID)
        //{
        //    return PartialView("~/Views/Order/_OrderDetail.cshtml", NotificationsManager.GetNotificationDetails(NotificationId));
        //}
    }
}