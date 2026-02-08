using Microsoft.AspNetCore.Mvc;
using Payrolls.DAO;
using Payrolls.Models;

namespace Payrolls.Controllers
{
    public class UserGroupController : Controller
    {
        ApplicationDbContext _context;
        public UserGroupController(ApplicationDbContext context) 
        {
                       _context= context;
        }

        public IActionResult Index()
        {
            List<UserGroup>data =_context
                .UserGroups
                .Where(ug => ug.IsActive == "true")
                .ToList();

            return View(data);
        }
    }
}
