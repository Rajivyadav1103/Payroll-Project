using Microsoft.AspNetCore.Mvc;
using Payrolls.DAO;
using Payrolls.Models;
using Payrolls.Models.viewModel;
using System.Linq;
using System.Collections.Generic;

namespace Payrolls.Controllers
{
    public class EmployeeDeductionInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDeductionInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Add/Update Data
        [HttpGet]
        public JsonResult Adddata(int mainid, int employeeid, int deductionid, int month, int year, int amount)
        {
            if (employeeid == 0 || deductionid == 0)
                return Json(new { Success = false, Message = "Employee or Deduction Head not selected." });

            if (mainid == 0)
            {
                EmployeeDeductionInfo edi = new EmployeeDeductionInfo
                {
                    EmployeeID = employeeid,
                    DeductionHeadId = deductionid,
                    DeductionMonth = month,
                    DeductionYear = year,
                    Amount = amount,
                    isActive = true
                };

                _context.EmployeeDeductionInfo.Add(edi);
                _context.SaveChanges();

                return Json(new { Success = true, Message = "Data saved in database successfully!" });
            }
            else
            {
                var existingRow = _context.EmployeeDeductionInfo.FirstOrDefault(x => x.EmployeeDeductionId == mainid);
                if (existingRow == null)
                    return Json(new { Success = false, Message = "Data Not Found in Database!" });

                existingRow.EmployeeID = employeeid;
                existingRow.DeductionHeadId = deductionid;
                existingRow.DeductionMonth = month;
                existingRow.DeductionYear = year;
                existingRow.Amount = amount;

                _context.SaveChanges();
                return Json(new { Success = true, Message = "Data updated Successfully!" });
            }
        }


        // Delete
        [HttpGet]
        public JsonResult deletedata(int id)
        {
            var data = _context.EmployeeDeductionInfo.FirstOrDefault(x => x.EmployeeDeductionId == id);
            if (data == null)
                return Json(new { Success = false, Message = "Deduction Info Not Found!" });

            data.isActive = false;
            _context.SaveChanges();
            return Json(new { Success = true, Message = "Data deleted!" });
        }

        // Edit
        [HttpGet]
        public JsonResult edit_req(int employeedeductionid)
        {
            var itemdata = _context.EmployeeDeductionInfo.FirstOrDefault(x => x.EmployeeDeductionId == employeedeductionid);
            if (itemdata == null)
                return Json(new { success = false, message = "data not found" });

            return Json(new { success = true, message = "found data", data = itemdata });
        }

        // Load data with filters
        [HttpGet]
        public JsonResult loaddata(int employeeid = 0, int deductionheadid = 0)
        {
            List<EmployeeDeductionInfoVM> data = _context.EmployeeDeductionInfo
                .Where(x => x.isActive == true &&
                            (employeeid == 0 || x.EmployeeID == employeeid) &&
                            (deductionheadid == 0 || x.DeductionHeadId == deductionheadid))
                .Select(s => new EmployeeDeductionInfoVM
                {
                    EmployeeDeductionId = s.EmployeeDeductionId,
                    EmployeeID = s.EmployeeID,
                    DeductionHeadId = s.DeductionHeadId,
                    DeductionYear = s.DeductionYear,
                    DeductionMonth = s.DeductionMonth,
                    Amount = s.Amount,
                    EmployeeFullname = s.Employee.Fullname,
                    DeductionHeadName = s.DeductionHead.DeductionHeadName
                })
                .ToList();

            return Json(data);
        }
    }
}
