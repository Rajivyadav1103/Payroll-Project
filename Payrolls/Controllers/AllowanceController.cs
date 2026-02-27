using Microsoft.AspNetCore.Mvc;
using Payrolls.DAO;
using Payrolls.Models;

namespace Payrolls.Controllers
{
    public class AllowanceController : BaseController
    {
        ApplicationDbContext _context;
        public AllowanceController(ApplicationDbContext context)
        {
            _context = context; 

        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult Create(int id, string allowanceName, string allowanceCode, int orderKey)
        {
            if (id == 0)
            {

                Allowance ah = new Allowance();
                ah.AllowanceName = allowanceName;
                ah.AllowanceCode = allowanceCode;
                ah.OrderKey = orderKey;
                ah.IsActive = true;
                ah.CreatedDate = DateTime.Now;

                _context.Allowance.Add(ah);
                _context.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "Allowance  saved successfully!"
                });
            }
            else
            {

                var existing = _context
                    .Allowance
                    .FirstOrDefault(x => x.AllowanceId == id);

                if (existing == null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Allowance Head not found!"
                    });
                }

                existing.AllowanceName = allowanceName;
                existing.AllowanceCode = allowanceCode;
                existing.OrderKey = orderKey;

                _context.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "Allowance Head updated successfully!"
                });
            }
        }

        [HttpGet]
        public JsonResult GetActiveAllowance(string name, string code)
        {
            List<Allowance> list = _context.Allowance
                .Where(x => x.IsActive == true &&
                            (string.IsNullOrEmpty(name) || x.AllowanceName.Contains(name)) &&
                            (string.IsNullOrEmpty(code) || x.AllowanceCode.Contains(code)))
                .ToList();

            return Json(list);
        }

        [HttpGet]
        public JsonResult GetItemById(int id)
        {
            var item = _context
                .Allowance
                .FirstOrDefault(x => x.AllowanceId == id);

            if (item == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Data not found!"
                });
            }

            return Json(new
            {
                Success = true,
                Data = item
            });
        }

        [HttpGet]
        public JsonResult DeleteAllowance(int id)
        {
            var existing = _context
                .Allowance
                .FirstOrDefault(x => x.AllowanceId == id);

            if (existing == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Data not found!"
                });
            }

            existing.IsActive = false;
            _context.SaveChanges();

            return Json(new
            {
                Success = true,
                Message = "Allowance Head deleted successfully!"
            });
        }


    }
}
