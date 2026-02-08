using Microsoft.AspNetCore.Mvc;
using Payrolls.DAO;

namespace Payrolls.Controllers
{
    public class AllowanceController : Controller
    {
        ApplicationDbContext _context;
        public AllowanceController(ApplicationDbContext context)
        {
            _context = context; 

        }
        public IActionResult Index()
        {



            return View();
        }
    }
}
