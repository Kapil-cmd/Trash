using Application;
using Microsoft.AspNetCore.Mvc;

namespace Trash.Controllers
{
    [ApiController]
    [Route("apiv1/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetUserList()
        {
            var response = _userService.GetAllUsers();
            if(response.Status == "000")
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
