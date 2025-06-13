using Identity.Api.Interfaces;
using Identity.Api.model;
using Identity.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("all")]
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersById([FromRoute] int id)
        {
            var chirp = await _userService.GetUsersById(id);

            if (chirp == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(chirp);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutChirp([FromRoute] int id, [FromBody] UsersUpdateModel user)
        {
            var result = await _userService.UpdateUser(id, user);

            if (result == false)
            {
                return BadRequest("Users non esistente!");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            int? result = await _userService.DeleteUser(id);

            if (result == null)
            {
                return BadRequest("Chirp non esistente!");
            }
            if (result == -1)
            {
                return BadRequest("Attenzione eliminare prima tutti i commenti associati alla Chirp!");
            }

            return Ok(result);
        }

    }
}
