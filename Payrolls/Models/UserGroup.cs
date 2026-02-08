namespace Payrolls.Models
 
{
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public string UserGroupName { get; set; }

        public string UserGroupCode { get; set; }

        public string IsActive { get; set; }    

        public DateTime createddate { get; set; }


    }
}
