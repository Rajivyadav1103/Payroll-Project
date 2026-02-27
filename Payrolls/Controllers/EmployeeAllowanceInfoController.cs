using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payrolls.DAO;
using Payrolls.Models;
using Payrolls.Models.viewModel;

namespace Payrolls.Controllers
{
    public class EmployeeAllowanceInfoController : BaseController
    {
         ApplicationDbContext _Context;

        public EmployeeAllowanceInfoController(ApplicationDbContext context)
        {
            _Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Adddata(int mainid, int employeeid, int allowanceid, int month, int year, int amount)
        {
            if (mainid == 0)
            {
                EmployeeAllowanceInfo edi = new EmployeeAllowanceInfo
                {
                    EmployeeID = employeeid,
                    AllowanceId = allowanceid,
                    AllowanceMonth = month,
                    AllowanceYear = year,
                    Amount = amount,
                    isActive = true
                };

                _Context.EmployeeAllowanceInfos.Add(edi);
                _Context.SaveChanges();

                return Json(new { Success = true, Message = "Data saved successfully!" });
            }
            else
            {
                var existingRow = _Context.EmployeeAllowanceInfos.FirstOrDefault(x => x.EmployeeAllowanceID == mainid);

                if (existingRow == null)
                    return Json(new { Success = false, Message = "Data not found!" });

                existingRow.EmployeeID = employeeid;
                existingRow.AllowanceId = allowanceid;
                existingRow.AllowanceMonth = month;
                existingRow.AllowanceYear = year;
                existingRow.Amount = amount;

                _Context.SaveChanges();

                return Json(new { Success = true, Message = "Data updated successfully!" });
            }
        }

        [HttpPost]
        public JsonResult deletedata(int id)
        {
            var data = _Context.EmployeeAllowanceInfos.FirstOrDefault(x => x.EmployeeAllowanceID == id);
            if (data == null)
                return Json(new { Success = false, Message = "Data not found!" });

            data.isActive = false;
            _Context.SaveChanges();

            return Json(new { Success = true, Message = "Data deleted!" });
        }

        public JsonResult edit_req(int employeeallowanceid)
        {
            var itemdata = _Context.EmployeeAllowanceInfos.FirstOrDefault(x => x.EmployeeAllowanceID == employeeallowanceid);
            if (itemdata == null)
                return Json(new { success = false, message = "Data not found" });

            return Json(new { success = true, message = "Found data", data = itemdata });
        }

        public JsonResult loaddata(string empName, string allowName)
        {
            var data = _Context.EmployeeAllowanceInfos
                .Include(x => x.Employee)
                .Include(x => x.Allowance)
                .Where(x => x.isActive == true &&
                            (string.IsNullOrEmpty(empName) || x.Employee.Fullname.Contains(empName)) &&
                            (string.IsNullOrEmpty(allowName) || x.Allowance.AllowanceName.Contains(allowName)))
                .Select(s => new EmployeeAllowanceInfoVM
                {
                    EmployeeAllowanceID = s.EmployeeAllowanceID,
                    EmployeeID = s.EmployeeID,
                    AllowanceId = s.AllowanceId,
                    AllowanceYear = s.AllowanceYear,
                    AllowanceMonth = s.AllowanceMonth,
                    Amount = s.Amount,
                    EmployeeFullname = s.Employee.Fullname,
                    AllowanceName = s.Allowance.AllowanceName
                })
                .ToList();

            return Json(data);
        }
    }
}