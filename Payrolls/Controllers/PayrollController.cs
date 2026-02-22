using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Payrolls.DAO;
using Payrolls.Helper;
using Payrolls.Helpers;
using Payrolls.Models;
using Payrolls.Models.viewModel;
using System.Data;

namespace Payrolls.Controllers
{
    public class PayrollController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PayrollController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Generate()
        {
            return View();
        }

        [HttpGet]
        public JsonResult CheckData(int yearId, int monthId)
        {
            try
            {
                var allowances = _context.EmployeeAllowanceInfos
                    .Where(a => a.AllowanceYear == yearId && a.AllowanceMonth == monthId && a.isActive)
                    .Select(a => new {
                        a.EmployeeID,
                        a.Amount,
                        EmployeeName = a.Employee.Fullname,
                        AllowanceName = a.Allowance.AllowanceName
                    })
                    .ToList();

                var deductions = _context.EmployeeDeductionInfo
                    .Where(d => d.DeductionYear == yearId && d.DeductionMonth == monthId && d.isActive)
                    .Select(d => new {
                        d.EmployeeID,
                        d.Amount,
                        EmployeeName = d.Employee.Fullname,
                        DeductionName = d.DeductionHead.DeductionHeadName
                    })
                    .ToList();

                return Json(new
                {
                    allowances,
                    deductions,
                    allowanceCount = allowances.Count,
                    deductionCount = deductions.Count
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}