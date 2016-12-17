using ProjectA.Framework.Messaging;
using ProjectA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectA.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Result<T> Dispatch<T>(BaseRequest<T> request) where T : BaseResponse
        {
            var result = DependencyResolver.Current.GetService<IDispatcher>().Dispatch(request);
            if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                if (result.Exception is AggregateException)
                {
                    var exceptions = (result.Exception as AggregateException).InnerExceptions;
                    var fluentExceptions = exceptions.Where(x => x.Source == "FluentValidator").ToList();

                    foreach (var ex in fluentExceptions)
                    {
                        ModelState.AddModelError(ex.Data["Property"] as string, ex.Message);
                    }
                }
            }
            return result;
        }

        public ActionResult ChangeCulture(string culture)
        {
            var returnUrl = Request.QueryString["ReturnUrl"] as string ?? Request.UrlReferrer?.AbsolutePath ?? "~/";

            var cookie = new HttpCookie("_culture", culture);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);

            return Redirect(returnUrl);
        }
    }
}