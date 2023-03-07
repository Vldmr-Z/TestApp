using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class WellController : Controller
    {
        // GET: Well
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Get(Int32? licId = null)
        {
            List<Well> model = new Well().Get(licId);
            return PartialView("GridWells", model);
        }
    }
}