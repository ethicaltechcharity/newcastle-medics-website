using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}

