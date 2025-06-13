namespace Identity.Api.model
{
    public class UserCreateModel
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
