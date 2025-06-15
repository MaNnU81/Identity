using System;
using identity.service.model;
using Identity.Api.model.DTOs;
using Identity.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Services
{
    public class UserService : IUserService
    {

        private readonly IdentityContext _identityContext;
        public UserService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }


        // Example method to demonstrate functionality      


        public async Task<List<UserViewModel>> GetAllUsers()
        {

            return await _identityContext.Users
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Password = user.Password,
                    Email = user.Email
                })
                .ToListAsync();

        }

        public async Task<int?> CreateUser(UserCreateModel userCreateModel)
        {

            var user = new User
            {

                FirstName = userCreateModel.FirstName,
                SecondName = userCreateModel.SecondName,
                Password = userCreateModel.Password,
                Email = userCreateModel.Email
            };

            _identityContext.Users.Add(user);
            await _identityContext.SaveChangesAsync();
            return user.Id;

        }

        public async Task<List<UserViewModel>> GetUsersByFilter(UsersFilterModel filter)
        {

            IQueryable<User> query = _identityContext.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                query = query.Where(x => x.FirstName == filter.FirstName);
            }
            if (!string.IsNullOrWhiteSpace(filter.SecondName))
            {
                query = query.Where(x => x.SecondName.Contains(filter.SecondName));
            }

            var result = await query.Select(x => new UserViewModel
            {

                FirstName = x.FirstName,
                SecondName = x.SecondName,
            }).ToListAsync();

            return result;
        }

        public async Task<UserViewModel> GetUsersById(int id)
        {
            var entity = await _identityContext.Users.FindAsync(id);

            if (entity == null)
            {
                return null;
            }
            return new UserViewModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                SecondName = entity.SecondName,
                Password = entity.Password,
                Email = entity.Email
            };
        }





        public async Task<bool> UpdateUser(int id, UsersUpdateModel user)
        {
            var entity = await _identityContext.Users.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(user.FirstName))
                entity.FirstName = user.FirstName;

            if (!string.IsNullOrWhiteSpace(user.SecondName))
                entity.SecondName = user.SecondName;

            if (!string.IsNullOrWhiteSpace(user.Password))
                entity.Password = user.Password;

            if (!string.IsNullOrWhiteSpace(user.Email))
                entity.Email = user.Email;


            _identityContext.Entry(entity).State = EntityState.Modified;
            await _identityContext.SaveChangesAsync();

            return true;
        }

        public async Task<int?> DeleteUser(int id)
        {
            User? entity = await _identityContext.Users.FirstOrDefaultAsync();


            if (entity == null)
            {
                return null;
            }

            _identityContext.Users.Remove(entity);
            await _identityContext.SaveChangesAsync();

            return id;
        }
    }

}



