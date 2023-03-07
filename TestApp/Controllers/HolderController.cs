using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HolderController : Controller
    {
        public ActionResult Index()
        {
            return View(new Holder().Get());
        }
    }
}