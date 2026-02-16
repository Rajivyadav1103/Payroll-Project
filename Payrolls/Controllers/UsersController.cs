using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payrolls.DAO;
using Payrolls.Models;
using Payrolls.Models.viewModel;
using System.Linq;

namespace Payrolls.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST search users
        [HttpPost]
        public JsonResult GetUsersBySearch([FromBody] string search)
        {
            var users = _context.users
                .Where(u => u.IsActive && (string.IsNullOrEmpty(search) || u.UserName.Contains(search)))
                .Select(u => new
                {
                    u.UserID,
                    u.UserName,
                    u.FullName,
                    u.Email,
                    u.MobileNumber,
                    u.Validfrom,
                    u.Validto,
                    u.IsActive
                })
                .ToList();

            return Json(users);
        }

        // Save user (create or edit)
        [HttpPost]
        public JsonResult SaveUsers([FromBody] usersVM vm)
        {
            if (vm == null) return Json(new { success = false, message = "Invalid data." });

            Users user;

            if (vm.UserID == 0)
            {
                // New user
                if (_context.users.Any(u => u.UserName == vm.UserName))
                    return Json(new { success = false, message = "Username already exists." });

                user = new Users();
                _context.users.Add(user);
            }
            else
            {
                // Edit existing
                user = _context.users.FirstOrDefault(u => u.UserID == vm.UserID);
                if (user == null) return Json(new { success = false, message = "User not found." });
            }

            // Password hash only for new or changed password
            var passwordHasher = new PasswordHasher<Users>();
            if (!string.IsNullOrEmpty(vm.Password))
                user.Password = passwordHasher.HashPassword(user, vm.Password);

            user.UserName = vm.UserName;
            user.FullName = vm.FullName;
            user.Email = vm.Email;
            user.MobileNumber = vm.MobileNumber;
            user.Validfrom = vm.Validfrom;
            user.Validto = vm.Validto;
            user.IsActive = vm.IsActive;

            _context.SaveChanges();
            return Json(new { success = true, message = "User saved successfully." });
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            var user = _context.users.FirstOrDefault(u => u.UserID == id);
            if (user == null) return Json(new { success = false, message = "User not found." });

            // Soft delete: mark as inactive
            user.IsActive = false;
            _context.SaveChanges();

            return Json(new { success = true, message = "User deleted successfully (soft delete)." });
        }

    }
}
