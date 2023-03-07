using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class AnalyticsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public String GetLic()
        {
            return new MapObjects().GetGeoJsonLic_251();
        }

        public String GetWells(string lng, string lat, string radius)
        {
            return new MapObjects().GetGeoJsonWells_251(lng, lat, radius);
        }

    }
}