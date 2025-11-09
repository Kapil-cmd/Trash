using Application;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Trash
{
    [ApiController]
    [Route("apiv1/[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;
        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] AddItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _itemService.AddItem(model);
                return Ok(response);
            }
            else
            {
                return Ok("PLEASE FILL THE FORM PROPERLY!!!");
            }
        }
        [HttpGet]
        public IActionResult GetItemList()
        {
            var response = _itemService.ItemList();
            return Ok(response);
        }
    }
}
