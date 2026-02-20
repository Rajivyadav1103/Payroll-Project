namespace Payrolls.Models.viewModel
{
    public class EmployeeDeductionInfoVM
    {

        public int EmployeeDeductionId { get; set; }
        public int EmployeeID { get; set; }
        public int DeductionHeadId { get; set; }
        public int DeductionYear { get; set; }
        public int DeductionMonth { get; set; }
        public int Amount { get; set; }

        public string EmployeeFullname { get; set; }
        public string DeductionHeadName { get; set; }

    }
}
