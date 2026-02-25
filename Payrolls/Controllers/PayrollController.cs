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
        public JsonResult GetPayrollTable(int yearId, int monthId)
        {
            try
            {
                List<MonthlyPayrollVM> data = new List<MonthlyPayrollVM>();

                // Use your existing connection string name "PrimaryConnnection"
                string connectionString = _configuration.GetConnectionString("PrimaryConnnection");

                // Optional: Add check to make sure connection string is found
                if (string.IsNullOrEmpty(connectionString))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Connection string 'PrimaryConnnection' not found in appsettings.json"
                    });
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"select *, (BasicSalary + TotalAllowance - TotalDeduction) as MonthlySalary
                         From (
                            select e.EmployeeId, e.Fullname,e.BasicSalary,
                                   isnull(al.TotalAllowance,0) as TotalAllowance,
                                   isnull(de.TotalDeduction,0) as TotalDeduction
                            From Employee e
                            left outer join (
                                select EmployeeID, sum(Amount) as TotalAllowance
                                From EmployeeAllowanceInfos
                                where isActive = 1
                                  and AllowanceYear = @yearId
                                  and AllowanceMonth = @monthId
                                group by EmployeeID
                            ) al on e.EmployeeId = al.EmployeeID
                            left outer join (
                                select EmployeeID, sum(Amount) as TotalDeduction
                                From EmployeeDeductionInfo
                                where isActive = 1
                                  and DeductionYear = @yearId
                                  and DeductionMonth = @monthId
                                group by EmployeeID
                            ) de on e.EmployeeId = de.EmployeeID
                            where e.IsActive = 1
                         ) x";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@yearId", yearId);
                        cmd.Parameters.AddWithValue("@monthId", monthId);

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                data.Add(new MonthlyPayrollVM
                                {
                                    EmployeeId = Convert.ToInt32(rdr["EmployeeId"]),
                                    Fullname = rdr["Fullname"].ToString(),
                                    BasicSalary = Convert.ToDecimal(rdr["BasicSalary"]),
                                    TotalAllowance = Convert.ToDecimal(rdr["TotalAllowance"]),
                                    TotalDeduction = Convert.ToDecimal(rdr["TotalDeduction"]),
                                    MonthlySalary = Convert.ToDecimal(rdr["MonthlySalary"]),
                                    FullnameWithId = rdr["Fullname"] + " (" + rdr["EmployeeId"] + ")"
                                });
                            }
                        }
                    }
                }

                return Json(new
                {
                    success = true,
                    data = data
                });
            }
            catch (SqlException sqlEx)
            {
                return Json(new
                {
                    success = false,
                    message = $"Database Error: {sqlEx.Message}"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}

