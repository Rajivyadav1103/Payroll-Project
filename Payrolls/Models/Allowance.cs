using System.ComponentModel.DataAnnotations;

namespace Payrolls.Models
{
    public class Allowance
    {

        [Key]
        public int AllowanceId { get; set; }


        public string AllowanceName { get; set; }


        public string AllowanceCode { get; set; }

        public int OrderKey { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
