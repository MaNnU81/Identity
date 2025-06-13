using Identity.Api.Interfaces;
using Identity.Api.model;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService)); ;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {

            var users = await _userService.GetAllUsers();




            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }


        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserCreateModel userCreateModel)
        {
            try
            {
               
                var userId = await _userService.CreateUser(userCreateModel);

                if (userId == null)
                {
                    return BadRequest("Text obbligatorio");
                }



                return Created($"/api/users/{userId}", userId);
            }

            catch (Exception ex)
            {
               
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByFilter([FromQuery] UsersFilterModel filter)
        {
            

            List<UserViewModel> result = await _userService.GetUsersByFilter(filter);

            if (result == null || !result.Any())
            {
                
                return NoContent();
            }
            else
            {
               
                return Ok(result);
            }
        }



    }
}
