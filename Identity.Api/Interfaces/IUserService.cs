using Identity.Api.model;

namespace Identity.Api.Interfaces
{
    public interface IUserService
    {
        Task <int?>CreateUser(UserCreateModel userCreateModel);
        Task<List<UserViewModel>> GetAllUsers();
        Task<List<UserViewModel>> GetUsersByFilter(UsersFilterModel filter);
    }
}
