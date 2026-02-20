using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payrolls.Models
{
    public class EmployeeAllowanceInfo
    {
        [Key]
        public int EmployeeAllowanceID { get; set; }
        public int EmployeeID { get; set; }
        public int AllowanceId { get; set; }
        public int AllowanceYear { get; set; }
        public int AllowanceMonth { get; set; }
        public int Amount { get; set; }
        public bool isActive { get; set; }



        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }


        [ForeignKey("AllowanceId")]
        public virtual Allowance Allowance { get; set; }

    }
}
