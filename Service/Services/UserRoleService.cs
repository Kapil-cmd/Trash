
using Core;

namespace Application
{
    public class UserRoleService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        public UserRoleService(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }
        public BaseResponseModel<string> AddRole(AddRoleViewModel model)
        {
            var response = new BaseResponseModel<string>();
            try
            {

            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<string> UpdateRole(UpdateRoleViewModel model);
        public BaseResponseModel<Role> GetRoleDetails(long roleId);
        public BaseResponseModel<string> AssignUserRole(AssignRoleViewModel model);
        public BaseResponseModel<string> DeleteUserRole(RemoveRoleViewModel model);
        public BaseResponseModel<string> AssignRolePermission(AssignedRolePermission model);
        public BaseResponseModel<UserPermission> GetUserPermission(long userId);
        public BaseResponseModel<string> RemoveRolePermission(RemoveRolePermission model);
        public BaseResponseModel<List<RolePermission>> GetRolePermisison(long RoleId);
        public BaseResponseModel<List<PermissionViewModel>> PermissionList();
        public BaseResponseModel<List<RoleViewModel>> RoleList();
    }
}
