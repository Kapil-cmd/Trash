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
        [AuthorizePermission("AddItem")]
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
        [AuthorizePermission("ItemList")]
        [HttpGet]
        public IActionResult GetItemList()
        {
            var response = _itemService.ItemList();
            return Ok(response);
        }
        [AuthorizePermission("UpdateItem")]
        [HttpPost]
        public async Task<IActionResult> UpdateItemStatus(UpdateItemStatus model)
        {
            if (ModelState.IsValid)
            {
                var response = await _itemService.UpdateItemStatus(model);
                return Ok();
            }
            else
            {
                return Ok();
            }
        }
    }
}
