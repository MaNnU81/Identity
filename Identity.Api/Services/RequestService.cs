using identity.service.model;
using Identity.Api.model;
using Identity.Api.model.DTOs;
using Identity.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Identity.Api.Services
{
    public class RequestService : IRequestService
    {
        private readonly IdentityContext _identityContext;
        public RequestService(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<List<RequestViewModel>> GetAllRequests()
        {

            return await _identityContext.Requests
                .Select(request => new RequestViewModel
                {
                    Id = request.Id,
                    Text = request.Text,
                    CreatedAt = request.CreatedAt,
                    ExecutedAt = request.ExecutedAt,
                    Success = request.Success,
                    UserId = request.UserId




                })
                .ToListAsync();

        }

        public async Task<RequestViewModel?> CreateRequest(int userId, RequestCreateModel model)
        {



            if (!await _identityContext.Users.AnyAsync(u => u.Id == userId))
            {
                return null;
            }

            var request = new Request
            {
                Text = model.Text,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
                UserId = userId

            };
            _identityContext.Requests.Add(request);
            await _identityContext.SaveChangesAsync();

            return new RequestViewModel
            {
                Id = request.Id,
                Text = request.Text,
                CreatedAt = request.CreatedAt,
                ExecutedAt = request.ExecutedAt,
                Success = request.Success,
                UserId = request.UserId
            };


                
        }

        public async Task<bool> UpdateRequest(int id, RequestUpdateModel model)
        {

            var request = await _identityContext.Requests
    .FirstOrDefaultAsync(r => r.Id == id);


           
            if (request == null)
            {
                
                return false;
            }

            // Aggiorna solo i campi forniti
            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                request.Text = model.Text;
            }

            if (model.ExecutedAt.HasValue)
            {
                request.ExecutedAt = model.ExecutedAt.Value.Kind == DateTimeKind.Utc
         ? model.ExecutedAt.Value.ToLocalTime()
         : model.ExecutedAt.Value;
            }

            if (model.Success.HasValue)
            {
                request.Success = model.Success.Value;
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


        public async Task<bool> DeleteRequest(int id)
        {
            try
            {
                var request = await _identityContext.Requests
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (request == null)
                {
                   
                    return false;
                }

                _identityContext.Requests.Remove(request);
                await _identityContext.SaveChangesAsync();

                
                return true;
            }
            catch (DbUpdateException ex)
            {
                
                return false;
            }
        }

        public async Task<RequestViewModel?> GetRequestById(int id)
        {
            try
            {
                var request = await _identityContext.Requests
                    .Where(r => r.Id == id)
                    .Select(r => new RequestViewModel
                    {
                        Id = r.Id,
                        Text = r.Text,
                        CreatedAt = r.CreatedAt,
                        ExecutedAt = r.ExecutedAt,
                        Success = r.Success,
                        UserId = r.UserId
                    })
                    .FirstOrDefaultAsync();

                if (request == null)
                {
                   
                    return null;
                }

                // Conversione DateTime per consistency (se necessario)
                if (request.ExecutedAt.HasValue)
                {
                    request.ExecutedAt = request.ExecutedAt.Value.Kind == DateTimeKind.Unspecified
                        ? DateTime.SpecifyKind(request.ExecutedAt.Value, DateTimeKind.Utc)
                        : request.ExecutedAt.Value.ToUniversalTime();
                }

               
                return request;
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

    }
}
