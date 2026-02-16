namespace Payrolls.Models.viewModel
{
    public class usersVM
    {

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public DateTime Validfrom { get; set; }

        public DateTime? Validto { get; set; }

        public bool IsActive { get; set; }
    }
}
