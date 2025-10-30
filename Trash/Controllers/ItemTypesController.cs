using Application;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
        public IActionResult GetItemTypeList()
        {
            List<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem() { Text = "--SELECT ITEM TYPES--", Value = "" });
            item.Add(new SelectListItem() { Text = "Electronics", Value = "1" });
            item.Add(new SelectListItem() { Text = "Furniture", Value = "2" });
            item.Add(new SelectListItem() { Text = "HouseHold", Value = "3" });
            
            return Ok(item);
        }
    }
}
