using System.ComponentModel.DataAnnotations;

namespace Payrolls.Models
{
    public class Deduction
    {
        [Key]


        public int DeductionHeadId { get; set; }


        public string DeductionHeadName { get; set; }


        public string DeductionHeadCode { get; set; }

        public int OrderKey { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
