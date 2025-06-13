using System;
using identity.service.model;
using Identity.Api.Interfaces;
using Identity.Api.model;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Services
{
    public class UserService : IUserService
    {

        private readonly  IdentityContext _identityContext;
        public UserService(IdentityContext identityContext)
        {
            _identityContext =  identityContext;
        }


        // Example method to demonstrate functionality      

       
        public async Task<List<UserViewModel>> GetAllUsers()
        {

            return await _identityContext.Users
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirtName = user.FirtName,
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
                
                FirtName = userCreateModel.FirtName,
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
                Id = x.Id,
                FirtName = x.FirtName,
                SecondName = x.SecondName,
                Password = x.Password,
                Email = x.Email
            }).ToListAsync();

            return result;

        }



    }

}
