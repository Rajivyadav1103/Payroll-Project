using Microsoft.AspNetCore.Mvc;
using Payrolls.DAO;
using Payrolls.Models.viewModel;


namespace Payrolls.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            HttpContext.Session.SetString("USER_ID", "");
            return View();
        }


        [HttpPost]
        public JsonResult SignIn(LoginVM vm)
        {
            try
            {
                var usr = _context.users
                    .Where(x => x.IsActive == true
                        && x.UserName == vm.Username)
                    .FirstOrDefault();

                if (usr == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "User Not Found"
                    });
                }

                if (usr.Password.Trim() != vm.Password.Trim())
                {
                    return Json(new
                    {
                        success = false,
                        message = "Password Not Matched"
                    });
                }

                HttpContext.Session.SetString("USER_ID", usr.UserID.ToString());

                return Json(new
                {
                    success = true,
                    message = "User Authenticated Successfully"
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Error occurred"
                });
            }
        }


    }
}
