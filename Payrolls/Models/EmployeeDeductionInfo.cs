using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payrolls.Models
{
    public class EmployeeDeductionInfo
    {

        [Key]
        public int EmployeeDeductionId { get; set; }

        public int EmployeeID { get; set; }
        public int DeductionHeadId { get; set; }

        public int DeductionYear { get; set; }
        public int DeductionMonth { get; set; }
        public int Amount { get; set; }

        public bool isActive { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("DeductionHeadId")]
        public virtual Deduction DeductionHead { get; set; }

    }
}
