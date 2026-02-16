namespace Payrolls.Models
{
    public class Employee
    {

        public int EmployeeId { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }

        public DateTime JoinDate { get; set; }

        public bool IsActive { get; set; }

        public decimal BasicSalary { get; set; }
    }
}
