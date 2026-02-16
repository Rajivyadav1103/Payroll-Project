using Microsoft.AspNetCore.Mvc;

namespace Payrolls.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
