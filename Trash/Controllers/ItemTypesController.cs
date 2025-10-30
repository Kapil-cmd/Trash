using Application;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Trash.Controllers
{
    [ApiController]
    [Route("apiv1/[controller]/[action]")]
    public class ItemTypesController : ControllerBase
    {
        private readonly ItemTypeService _itemTypeService;
        public ItemTypesController(ItemTypeService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }
        [HttpPost]
        public IActionResult AddItemType(AddItemTypeViewModel model)
        {
            var response = _itemTypeService.AddItemType(model);
            if(response.Status == "000")
            {
                return Ok(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
