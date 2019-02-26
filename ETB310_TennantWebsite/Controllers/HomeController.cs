using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETB310_TennantWebsite.Models;

namespace ETB310_TennantWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateServiceCase(ServiceCaseViewModel vm)
        {
            return View(vm);
        }
        public ActionResult CreateServiceCase()
        {
            var vm = new ServiceCaseViewModel();
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}