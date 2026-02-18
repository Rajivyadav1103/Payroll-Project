using Microsoft.AspNetCore.Mvc;
using Payrolls.DAO;
using Payrolls.Models;
using System;
using System.Linq;

namespace Payrolls.Controllers
{
    public class DeductionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeductionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Create(int id, string deductionHeadName, string deductionHeadCode, int orderKey)
        {
            if (id == 0)
            {
                // Add new
                Deduction dh = new Deduction();
                dh.DeductionHeadName = deductionHeadName;
                dh.DeductionHeadCode = deductionHeadCode;
                dh.OrderKey = orderKey;
                dh.IsActive = true;
                dh.CreatedDate = DateTime.Now;

                _context.Deduction.Add(dh);
                _context.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "Deduction saved successfully!"
                });
            }
            else
            {
                // Edit existing
                var existing = _context
                    .Deduction
                    .FirstOrDefault(x => x.DeductionHeadId == id);

                if (existing == null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Deduction Head not found!"
                    });
                }
                existing.DeductionHeadName = deductionHeadName;
                existing.DeductionHeadCode = deductionHeadCode;
                existing.OrderKey = orderKey;

                _context.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "Deduction updated successfully!"
                });
            }
        }

        [HttpGet]
        public JsonResult GetActiveDeductionHeads(string name, string code)
        {
            List<Deduction> list = _context.Deduction
                .Where(x => x.IsActive == true &&
                            (string.IsNullOrEmpty(name) || x.DeductionHeadName.Contains(name)) &&
                            (string.IsNullOrEmpty(code) || x.DeductionHeadCode.Contains(code)))
                .ToList();

            return Json(list);
        }

        [HttpGet]
        public JsonResult GetItemById(int id)
        {
            var item = _context.Deduction.FirstOrDefault(x => x.DeductionHeadId == id);
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
        public JsonResult DeleteDeductionHead(int id)
        {
            var existing = _context.Deduction.FirstOrDefault(x => x.DeductionHeadId == id);
            if (existing == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Data not found!"
                });
            }

            // Soft delete
            existing.IsActive = false;
            _context.SaveChanges();

            return Json(new
            {
                Success = true,
                Message = "Deduction deleted successfully!"
            });
        }

        [HttpGet]
        public JsonResult GetActiveDeduction()
        {
            var existing = _context.Deduction
                                .Where(x => x.IsActive == true)
                                .ToList();
            return Json(existing);

        }
    }
}
