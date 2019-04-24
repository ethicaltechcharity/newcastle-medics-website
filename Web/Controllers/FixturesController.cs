using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class FixturesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Fixture(int id)
        {
            return View();
        }


    }
}