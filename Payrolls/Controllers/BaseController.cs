using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Payrolls.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string ss = HttpContext.Session.GetString("USER_ID");

            if (string.IsNullOrEmpty(ss))
            {
                context.Result = Redirect("/Auth/Login");
            }

            base.OnActionExecuting(context);
        }
    }
}


