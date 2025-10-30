using Core;
using Microsoft.AspNetCore.Mvc;

namespace Trash
{
    [ApiController]
    [Route("apiv1/[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm]AddItemViewModel model)
        {
            return Ok();
        }
    }
}
