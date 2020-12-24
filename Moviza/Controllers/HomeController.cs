using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moviza.Controllers
{
    public class HomeController : Controller
    {
        //cache html not data
    //    [OutputCache(Duration =10,Location =System.Web.UI.OutputCacheLocation.Server,VaryByParam ="*",NoStore =false)]
        public ActionResult Index()
        {
            return View();
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