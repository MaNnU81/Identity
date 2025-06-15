using identity.service.model;
using Identity.Api.model.DTOs;
using Identity.Api.Services;
using Identity.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<RequestsController> _logger;

        public RequestsController(IRequestService requestService, ILogger<RequestsController> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRequests()
        {

            var requests = await _requestService.GetAllRequests();




            if (requests == null || !requests.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(requests);
        }

        [HttpPost("{userId}")]  
        public async Task<IActionResult> PostRequest([FromRoute] int userId, [FromBody] RequestCreateModel model)
        {

            _logger.LogInformation("Creating comment for request {RequestId}", userId);

           
            var createdRequest = await _requestService.CreateRequest(userId, model);


            return createdRequest == null
                ? NotFound($"User with ID {userId} not found.") :
                CreatedAtAction(nameof(GetAllRequests), new {userId, id = createdRequest.Id }, createdRequest);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(
    [FromRoute] int id,
    [FromBody] RequestUpdateModel model)
        {
            _logger.LogInformation("Updating request {RequestId}", id);

            var updateResult = await _requestService.UpdateRequest(id, model);

            if (!updateResult)
            {
                _logger.LogWarning("Failed to update request {RequestId}", id);
                return NotFound($"Request with ID {id} not found or update failed");
            }

            _logger.LogInformation("Request {RequestId} updated successfully", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest([FromRoute] int id)
        {
            _logger.LogInformation("Attempting to delete request with ID {RequestId}", id);

            var result = await _requestService.DeleteRequest(id);

            return result
                ? NoContent()
                : NotFound(new
                {
                    Message = $"Request with ID {id} not found or could not be deleted",
                    Solution = "Verify the request exists and is not referenced by other entities"
                });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById([FromRoute] int id)
        {
            _logger.LogInformation("Fetching request with ID {RequestId}", id);

            var request = await _requestService.GetRequestById(id);

            return request != null
                ? Ok(request)
                : NotFound(new
                {
                    Message = $"Request with ID {id} not found",
                    Solution = "Verify the request exists in the database"
                });
        }
    }
}
