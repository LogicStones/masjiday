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
    }
}