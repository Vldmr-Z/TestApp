using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class LicController : Controller
    {
        // GET: Lic
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Get()
        {
            List<Lic> model = new Lic().Get();
            return PartialView("GridLic", model);
        }
    }
}