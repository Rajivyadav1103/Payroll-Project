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
                .Where(x => x.IsActive == true)
                .ToList();

            return View(data);
        }

        [HttpPost]
        public JsonResult Create(string UserGroupName, string UserGroupCode)
        {
            UserGroup ug = new UserGroup
            {
                UserGroupName = UserGroupName,
                UserGroupCode = UserGroupCode
            };

            _context.UserGroups.Add(ug);
            _context.SaveChanges();

            return Json(new { message = "Saved successfully" });
        }




    }
}
