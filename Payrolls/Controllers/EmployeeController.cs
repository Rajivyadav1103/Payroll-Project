using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payrolls.DAO;
using Payrolls.Models;

namespace Payrolls.Controllers
{

   
    public class EmployeeController : Controller
    {

         ApplicationDbContext _context;



        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }




        
        [HttpGet]
        public JsonResult Create(int id, string fullname, string email, string contactNo, DateTime joinDate, decimal basicSalary)
        {
            if (id == 0)
            {
                Employee emp = new Employee();
                emp.Fullname = fullname;
                emp.Email = email;
                emp.ContactNo = contactNo;
                emp.JoinDate = joinDate;
                emp.BasicSalary = basicSalary;
                emp.IsActive = true;

                _context.Employee.Add(emp);
                _context.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "Employee created successfully!"
                });
            }
            else
            {
                var existing = _context
                    .Employee
                    .FirstOrDefault(x => x.EmployeeId == id);

                if (existing == null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Employee not found!"
                    });
                }

                existing.Fullname = fullname;
                existing.Email = email;
                existing.ContactNo = contactNo;
                existing.JoinDate = joinDate;
                existing.BasicSalary = basicSalary;

                _context.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "Employee updated successfully!"
                });
            }
        }
       


        [HttpGet]
        public JsonResult GetActiveEmployees(string fullname, string email)
        {
            List<Employee> list = _context
                .Employee
                .Where(x => x.IsActive == true &&
                            (string.IsNullOrEmpty(fullname) || x.Fullname.Contains(fullname)) &&
                            (string.IsNullOrEmpty(email) || x.Email.Contains(email)))
                .ToList();

            return Json(list);
        }
     



       
        [HttpGet]
        public JsonResult GetEmployeeById(int id)
        {
            var item = _context
                .Employee
                .FirstOrDefault(x => x.EmployeeId == id);

            if (item == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Employee not found!"
                });
            }

            return Json(new
            {
                Success = true,
                Data = item
            });
        }
     
        [HttpGet]
        public JsonResult DeleteEmployee(int id)
        {
            var existing = _context
                .Employee
                .FirstOrDefault(x => x.EmployeeId == id);

            if (existing == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Employee not found!"
                });
            }

            existing.IsActive = false;
            _context.SaveChanges();

            return Json(new
            {
                Success = true,
                Message = "Employee deleted successfully!"
            });
        }
      



    }
}
