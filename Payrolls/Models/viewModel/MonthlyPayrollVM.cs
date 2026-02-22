namespace Payrolls.Models.viewModel
{
    public class MonthlyPayrollVM
    {

        public int EmployeeId { get; set; }
        public string Fullname { get; set; }
        public string FullnameWithId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TotalAllowance { get; set; }   
        public decimal TotalDeduction { get; set; }
        public decimal MonthlySalary { get; set; }


    }
}
