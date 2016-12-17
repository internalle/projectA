using ProjectA.i18n;
using ProjectA.Services.Features.Measurements;
using System.Web.Mvc;

namespace ProjectA.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var unitsResult = Dispatch(new ListAllUnits.Request());
            return View(unitsResult.Response);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}