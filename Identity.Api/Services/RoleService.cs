using Identity.Api.model.DTOs;
using identity.service.model;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Services.Interfaces;
using Identity.Api.model;

namespace Identity.Api.Services
{
    public class RoleService: IRoleService
    {

        private readonly IdentityContext _identityContext;
        public RoleService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<List<RoleViewModel>> GetAllRoles()
        {
            return await _identityContext.Roles
                .Select(role => new RoleViewModel
                {
                    Id = role.Id,
                    Code = role.Code,
                    Description = role.Description,
                })
                .ToListAsync();
        }
        public async Task<RoleViewModel?> CreateRole(int userId, RoleCreateModel model)
        {



            if (!await _identityContext.Users.AnyAsync(u => u.Id == userId))
            {
                return null;
            }

            var role = new Role
            {
                Id = model.Id,
               Code = model.Code,
               Description = model.Description

            };
            _identityContext.Roles.Add(role);
            await _identityContext.SaveChangesAsync();

            return new RoleViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description
            };



        }

        public async Task<bool> DeleteRole(int id)
        {
            try
            {
                var role = await _identityContext.Roles
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (role == null)
                {

                    return false;
                }

                _identityContext.Roles.Remove(role);
                await _identityContext.SaveChangesAsync();


                return true;
            }
            catch (DbUpdateException ex)
            {

                return false;
            }
        }

        public async Task<bool> UpdateRole(int id, RoleUpdateModel model)
        {

            var role = await _identityContext.Roles
    .FirstOrDefaultAsync(r => r.Id == id);



            if (role == null)
            {

                return false;
            }

            // Aggiorna solo i campi forniti
            //if (!string.IsNullOrWhiteSpace(model.Id))
            //{
            //    role.Id = model.Id;
            //}

            if (!string.IsNullOrWhiteSpace(model.Code))
            {
                role.Code = model.Code;

            }

            if (!string.IsNullOrWhiteSpace(model.Description))
            {
                role.Description = model.Description;

            }


            try
            {
                var affectedRows = await _identityContext.SaveChangesAsync();
                return affectedRows > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<RoleViewModel?> GetRoleById(int id)
        {
            try
            {
                var role = await _identityContext.Roles
                    .Where(r => r.Id == id)
                    .Select(r => new RoleViewModel
                    {
                        Id = r.Id,
                        Code = r.Code,
                        Description = r.Description
                    })
                    .FirstOrDefaultAsync();

                if (role == null)
                {

                    return null;
                }



                return role;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }

}
