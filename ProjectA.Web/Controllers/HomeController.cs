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

        public new ActionResult ChangeCulture(string culture)
        {
            return base.ChangeCulture(culture);
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