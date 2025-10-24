using Application;
using Core;
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
        public IActionResult Login([FromBody]Login model)
        {
                Dictionary<string,string> result = new Dictionary<string,string>();
            if(model.UserName =="admin" && model.Password == "admin")
            {
                result.Add("username", model.UserName);
                result.Add("password", model.Password);
                result.Add("Status", "000");
                result.Add("Message", "success");
                return Ok(result);
            }
            else
            {
                result.Add("Status", "200");
                result.Add("Message", "Unauthorized access!!!");
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
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
