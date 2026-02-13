using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Payrolls.DAO;
using Payrolls.Models;

namespace Payrolls.Controllers
{
    public class UserGroupController : Controller
    {
        ApplicationDbContext _context;
        public UserGroupController(ApplicationDbContext context) 
        {
                       _context= context;
        }

        public IActionResult Index()
        {
            List<UserGroup>data =_context
                .UserGroups
                .Where(x => x.IsActive == true)
                .ToList();

            return View(data);
        }

        [HttpPost]
        public JsonResult Create([FromBody] UserGroup model)
        {
            if (model.UserGroupId == 0)
            {
                // INSERT
                _context.UserGroups.Add(new UserGroup
                {
                    UserGroupName = model.UserGroupName,
                    UserGroupCode = model.UserGroupCode,
                    IsActive = true,
                    createddate = DateTime.Now
                });

                _context.SaveChanges();

                return Json(new { success = true, message = "Saved successfully" });
            }
            else
            {
                // UPDATE
                var existing = _context.UserGroups
                                       .FirstOrDefault(x => x.UserGroupId == model.UserGroupId);

                if (existing == null)
                    return Json(new { success = false, message = "Data not found" });

                existing.UserGroupName = model.UserGroupName;
                existing.UserGroupCode = model.UserGroupCode;

                _context.SaveChanges();

                return Json(new { success = true, message = "Updated successfully" });
            }
        }


        //========= to load data in table========== 
        [HttpGet]
        public JsonResult GetActiveUserGroups(string groupName,string groupCode, int UserGroupId)
        {
            List<UserGroup> ug = _context
                              .UserGroups
                                    
                                    .Where(x => x.IsActive == true
                                    && (UserGroupId == 0 || x.UserGroupId == UserGroupId)
                                    && (string.IsNullOrEmpty(groupName) || x.UserGroupName.Contains(groupName))
                                    && (string.IsNullOrEmpty(groupCode) || x.UserGroupCode.Contains(groupCode))
                                    )

                                    .ToList();




            return Json(ug);
        }



        [HttpGet]
        public JsonResult GetItemById(int id)
        {
            UserGroup row = _context
                        .UserGroups
                        .Where(x => x.UserGroupId == id)
                        .FirstOrDefault();
            if (row == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Data not found in Database!!!"
                });
            }
            else
            {
                return Json(new
                {
                    Success = true,
                    Message = "",
                    Data = row
                });
            }
        }

        //=====delete
        [HttpGet]
        public JsonResult DeleteUserGroup(int id)
        {
            var existingRow = _context
                                .UserGroups
                                .Where(x => x.UserGroupId == id)
                                .FirstOrDefault();
            if (existingRow == null)
            {
                return Json(new
                {
                    success = false,
                    Message = "Data Not Found in Database!"
                });
            }
            else
            {
                //HARD DELETE
                //_context.UserGroup.Remove(existingRow);
                //_context.SaveChanges();


                //SOFT DELETE
                existingRow.IsActive = false;
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    Message = "Data Deleted Successfully"
                });
            }
        }



    }
}
