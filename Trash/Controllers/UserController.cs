using Application;
using Core;
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
            if (response.Status == "000")
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpGet]
        public IActionResult GetUserInfo(long userId)
        {
            var response = _userService.GetUserDetails(userId);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult UpdateUserInfo(UpdateUserDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userService.UpdateUserInfo(model);
                return Ok(response);
            }
            else
            {
                return Ok("PLEASE FILL THE FORM PROPERLY!!!!");
            }
        }
    }
}
