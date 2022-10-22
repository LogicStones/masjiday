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
    public class MasajidController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult FetchMasajid()
        {
            return Json(MasajidManager.FetchAllMasajid(Request.FetchPaging()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUpdateMasjid(int? masjidId)
        {
            return PartialView("_AddUpdateMasjid", masjidId != null ? MasajidManager.GetMasjidById((int)masjidId) : new Masjid());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateMasjid(Masjid model)
        {
            if (model.Id == 0)
            {
                MasajidManager.SaveMasjid(model);
                TempData["Message"] = "Masjid added successfully.";
            }
            else
            {
                 MasajidManager.UpdateMasjid(model);
                TempData["Message"] = "Masjid updated successfully.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult UploadPrayerTimings(int? masjidId)
        {
            return PartialView("_UploadPrayerTimings", new PrayerTimingDTO { MasjidId = (int)masjidId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPrayerTimings(PrayerTimingDTO model)
        {
            if (model.FileUpload.ContentLength > 0)
            {
                if (System.IO.Path.GetExtension(model.FileUpload.FileName).ToLower().Equals(".csv"))
                {
                    MasajidManager.UpdateTiming(model);
                    TempData["Message"] = "Masjid added successfully.";
                }
                else
                    TempData["Message"] = "Failed: Invalid file type.";
            }
            else
                TempData["Message"] = "Failed: File not found.";

            return RedirectToAction("Index");
        }
    }
}