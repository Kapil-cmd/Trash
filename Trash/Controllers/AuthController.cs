using Application;
using Core;
using Microsoft.AspNetCore.Mvc;
namespace Trash.Controllers
{
    [ApiController]
    [Route("apiv1/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public IActionResult Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userService.Login(model);
                if(response.Status == "000")
                {
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
            {
                return Ok("PLEASE FILL THE FORM PROPERLY!!!");
            }
        }
        [HttpPost]
        public IActionResult Register([FromBody]RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userService.RegisterUser(model);
                if(response.Status == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                return Ok("USER NOT REGISTERED!!!");
            }
        }
        [HttpGet]
        public IActionResult Ciphar(string text)
        {
            var result = StaticMethod.getNetAplhabet(text);
            return Ok(result);
        }
    }
}
