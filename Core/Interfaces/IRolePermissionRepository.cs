
namespace Core
{
    public interface IRolePermissionRepository
    {
        public SpResponse<string> AddRole(AddRoleViewModel model);
        public SpResponse<string> UpdateRole(UpdateRoleViewModel model);
        public SpResponse<Role> GetRoleDetails(long roleId);
        public SpResponse<string> AssignUserRole(AssignRoleViewModel model);
        public SpResponse<string> DeleteUserRole(RemoveRoleViewModel model);
        public SpResponse<string> AssignRolePermission(AssignedRolePermission model);
        public SpResponse<UserPermission> GetUserPermission(long userId);
        public SpResponse<string> RemoveRolePermission(RemoveRolePermission model);
        public SpResponse<List<RolePermission>> GetRolePermisison(long RoleId);
        public SpResponse<List<PermissionViewModel>> PermissionList();
        public SpResponse<List<RoleViewModel>> RoleList();
    }
}
