using Identity.Api.model.DTOs;

namespace Identity.Api.Services.Interfaces
{
    public interface IRequestService
    {
        Task<RequestViewModel?> CreateRequest(int userId, RequestCreateModel model);
        Task<List<RequestViewModel>> GetAllRequests();
        Task<RequestViewModel?> GetRequestById(int id);

        Task<bool> UpdateRequest(int id, RequestUpdateModel model);
        Task<bool> DeleteRequest(int id);
    }
}
