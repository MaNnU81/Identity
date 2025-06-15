using Identity.Api.model.DTOs;

namespace Identity.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task <int?>CreateUser(UserCreateModel userCreateModel);
        Task<int?> DeleteUser(int id);
        Task<List<UserViewModel>> GetAllUsers();
        Task<List<UserViewModel>> GetUsersByFilter(UsersFilterModel filter);
        Task<UserViewModel> GetUsersById(int id);
       
        Task<bool> UpdateUser(int id, UsersUpdateModel user);
    }
}
