using identity.service.model;

namespace Identity.Api.model
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; } 

        public DateTime CreatedDate { get; set; }


        //navigation properties 
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

    }
}
