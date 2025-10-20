using Microsoft.AspNetCore.Mvc;

namespace Trash.Controllers
{
    [ApiController]
    [Route("apiv1/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
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
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok("USER REGISTERED!!!");
            }
            else
            {
                return Ok("USER NOT REGISTERED!!!");
            }
        }    
    }
}
