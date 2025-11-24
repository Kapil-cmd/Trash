using Application;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Trash
{
    [ApiController]
    [Route("apiv1/[controller]/[action]")]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService _userRoleService;
        public UserRoleController(UserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }
        [HttpPost]
        public IActionResult AddRole(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userRoleService.AddRole(model);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult UpdateRole(UpdateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userRoleService.UpdateRole(model);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult GetRoleDetails(long roleId)
        {
            var response = _userRoleService.GetRoleDetails(roleId);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult AssignUserRole(AssignRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userRoleService.AssignUserRole(model);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult RemoveUserRole(RemoveRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userRoleService.DeleteUserRole(model);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult AssignRolePermission(AssignedRolePermission model)
        {
            if (ModelState.IsValid)
            {
                var response = _userRoleService.AssignRolePermission(model);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult GetUserPermission(long userId)
        {
            var response = _userRoleService.GetUserPermission(userId);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult RemoveRolePermission(RemoveRolePermission model)
        {
            if (ModelState.IsValid)
            {
                var response = _userRoleService.RemoveRolePermission(model);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult GetRolePermission(long roleId)
        {
            var response = _userRoleService.GetRolePermisison(roleId);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult PermissionList()
        {
            var response = _userRoleService.PermissionList();
            return Ok(response);
        }
        [HttpGet]
        public IActionResult RoleList()
        {
            var response = _userRoleService.RoleList();
            return Ok(response);
        }
    }
}
