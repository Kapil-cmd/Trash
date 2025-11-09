
using Application;
using Microsoft.AspNetCore.Mvc;

namespace Trash.Controllers
{
    [ApiController]
    [Route("apiV1/[controller]/[action]")]
    public class ListController : ControllerBase
    {
        private readonly ListService _listService;
        public ListController(ListService listService)
        {
            _listService = listService;
        }
        [HttpGet]
        public IActionResult GetItemTypeList()
        {
            var response = _listService.GetItemTypeList();
            return Ok(response);
        }
    }
}
