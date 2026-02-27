using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payrolls.DAO;
using Payrolls.Models;
using Payrolls.Models.viewModel;
using System.Linq;
using System.Collections.Generic;

namespace Payrolls.Controllers
{
    public class EmployeeDeductionInfoController : BaseController
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


        // Add or Update Data
       [HttpPost]
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

                return Json(new { Success = true, Message = "Data saved successfully!" });
            }
            else
            {
                var existingRow = _context.EmployeeDeductionInfo.FirstOrDefault(x => x.EmployeeDeductionId == mainid);
                if (existingRow == null)
                    return Json(new { Success = false, Message = "Data not found!" });

                existingRow.EmployeeID = employeeid;
                existingRow.DeductionHeadId = deductionid;
                existingRow.DeductionMonth = month;
                existingRow.DeductionYear = year;
                existingRow.Amount = amount;

                _context.SaveChanges();

                return Json(new { Success = true, Message = "Data updated successfully!" });
            }
        }

        // Delete (soft delete)
        [HttpPost]
        public JsonResult deletedata(int id)
        {
            var data = _context.EmployeeDeductionInfo.FirstOrDefault(x => x.EmployeeDeductionId == id);
            if (data == null)
                return Json(new { Success = false, Message = "Data not found!" });

            data.isActive = false;
            _context.SaveChanges();

            return Json(new { Success = true, Message = "Data deleted successfully!" });
        }
        // Edit request
        [HttpGet]
        public JsonResult edit_req(int employeedeductionid)
        {
            var itemdata = _context.EmployeeDeductionInfo
                                   .Include(x => x.Employee)
                                   .Include(x => x.DeductionHead)
                                   .FirstOrDefault(x => x.EmployeeDeductionId == employeedeductionid);

            if (itemdata == null)
                return Json(new { success = false, message = "Data not found!" });

            return Json(new { success = true, message = "Data found", data = itemdata });
        }

        // Load data with optional filters
        [HttpGet]
        public JsonResult loaddata(int employeeid = 0, int deductionheadid = 0)
        {
            var data = _context.EmployeeDeductionInfo
                               .Include(x => x.Employee)
                               .Include(x => x.DeductionHead)
                               .Where(x => x.isActive &&
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
//ssw