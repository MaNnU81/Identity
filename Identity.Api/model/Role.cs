using identity.service.model;

namespace Identity.Api.model
{
    public class Role
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}