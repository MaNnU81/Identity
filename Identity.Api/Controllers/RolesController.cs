using Identity.Api.model.DTOs;
using Identity.Api.Services;
using Identity.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleService roleService, ILogger<RolesController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles()
        {

            var roles = await _roleService.GetAllRoles();




            if (roles == null || !roles.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(roles);
        }


        [HttpPost("{userId}")]
        public async Task<IActionResult> PostRole([FromRoute] int userId, [FromBody] RoleCreateModel model)
        {

            _logger.LogInformation("Creating comment for role {RoleId}", userId);


            var createdRole = await _roleService.CreateRole(userId, model);


            return createdRole == null
                ? NotFound($"User with ID {userId} not found.") :
                CreatedAtAction(nameof(GetAllRoles), new { userId, id = createdRole.Id }, createdRole);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            _logger.LogInformation("Attempting to delete role with ID {RoleId}", id);

            var result = await _roleService.DeleteRole(id);

            return result
                ? NoContent()
                : NotFound(new
                {
                    Message = $"Role with ID {id} not found or could not be deleted",
                    Solution = "Verify the role exists and is not referenced by other entities"
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(
   [FromRoute] int id,
   [FromBody] RoleUpdateModel model)
        {
            _logger.LogInformation("Updating role {RoleId}", id);

            var updateRole = await _roleService.UpdateRole(id, model);

            if (!updateRole)
            {
                _logger.LogWarning("Failed to update role {RoleId}", id);
                return NotFound($"Role with ID {id} not found or update failed");
            }

            _logger.LogInformation("Role {RoleId} updated successfully", id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            _logger.LogInformation("Fetching role with ID {RoleId}", id);

            var role = await _roleService.GetRoleById(id);

            return role != null
                ? Ok(role)
                : NotFound(new
                {
                    Message = $"Role with ID {id} not found",
                    Solution = "Verify the role exists in the database"
                });
        }
    }
}
