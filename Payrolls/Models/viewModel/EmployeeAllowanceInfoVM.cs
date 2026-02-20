namespace Payrolls.Models.viewModel
{
    public class EmployeeAllowanceInfoVM
    {
        public int EmployeeAllowanceID { get; set; }

        public int EmployeeID { get; set; }
        public int AllowanceId { get; set; }
        public int AllowanceYear { get; set; }
        public int AllowanceMonth { get; set; }
        public int Amount { get; set; }



        public string EmployeeFullname { get; set; }
       
        public string AllowanceName { get; internal set; }
    }
}
