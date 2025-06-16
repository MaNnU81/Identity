
using Identity.Api.model.DTOs;

namespace Identity.Api.Services.Interfaces
{
    public interface IRoleService
    {
    
        Task<RoleViewModel?> CreateRole(int userId, RoleCreateModel model);
        Task<bool> DeleteRole(int id);

        Task<List<RoleViewModel>> GetAllRoles();
        Task<bool> UpdateRole(int id, RoleUpdateModel model);
       
    }
}
