using ProjectA.i18n;
using System.Web.Mvc;

namespace ProjectA.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = ResourceProvider.ByString("Resource.Hello");

            return View();
        }
    }
}