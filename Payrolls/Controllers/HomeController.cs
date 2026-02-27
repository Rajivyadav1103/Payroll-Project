using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Payrolls.DAO;
using System.Data;

namespace Payrolls.Controllers
{
    public class HomeController : BaseController
    {
         ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ---------------------------------------------
        // Helper Method (REPLACES GetDataTable)
        // ---------------------------------------------
        private DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();

            var conn = _context.Database.GetDbConnection();

            using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
            {
                cmd.CommandType = CommandType.Text;

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // ---------------------------------------------
        // Total Deduction Per Employee
        // ---------------------------------------------
        [HttpGet]
        public JsonResult GetTotalDeductionPerEmployee()
        {
            string query = @"SELECT EmployeeName, SUM(Amount) AS TotalDeduction 
                             FROM (
                                SELECT eai.*, e.Fullname AS EmployeeName
                                FROM EmployeeDeductionInfo eai
                                LEFT JOIN Employee e ON eai.EmployeeID = e.EmployeeId
                                WHERE eai.isActive = 1
                             ) x
                             GROUP BY EmployeeName";

            DataTable dt = ExecuteQuery(query);

            return Json(new
            {
                success = true,
                data = dt
            });
        }

        // ---------------------------------------------
        // Total Allowance Per Employee
        // ---------------------------------------------
        [HttpGet]
        public JsonResult GetTotalAllowancePerEmployee()
        {
            string query = @"SELECT EmployeeName, SUM(Amount) AS TotalAllowance
                             FROM (
                                SELECT eai.*, e.Fullname AS EmployeeName
                                FROM EmployeeAllowanceInfo eai
                                LEFT JOIN Employee e ON eai.EmployeeID = e.EmployeeId
                                WHERE eai.isActive = 1
                             ) x
                             GROUP BY EmployeeName";

            DataTable dt = ExecuteQuery(query);

            return Json(new
            {
                success = true,
                data = dt
            });
        }

        // ---------------------------------------------
        // Allowance Distribution
        // ---------------------------------------------
        [HttpGet]
        public JsonResult GetAllowanceDistribution()
        {
            string query = @"SELECT x.AllowanceName, SUM(x.Amount) AS Total
                             FROM (
                                SELECT a.AllowanceName, ea.Amount
                                FROM Allowance a
                                LEFT JOIN EmployeeAllowanceInfo ea 
                                ON a.AllowanceId = ea.AllowanceId
                                WHERE ea.isActive = 1
                             ) x
                             GROUP BY x.AllowanceName";

            DataTable dt = ExecuteQuery(query);

            return Json(new
            {
                success = true,
                data = dt
            });
        }
    }
}