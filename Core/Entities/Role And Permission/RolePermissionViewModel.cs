namespace Core
{
    public class RolePermissionViewModel
    {
    }
    public class Role
    {
        public long RoleId {  get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
    public class AddRoleViewModel
    {
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
    public class UpdateRoleViewModel
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class AssignRoleViewModel
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public string CreatedBy { get; set; }
    }
    public class RemoveRoleViewModel
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }
    }
    public class AssignedRolePermission
    {
        public AssignedRolePermission() 
        {
            PermissionList = new List<PermissionViewModel>();
        }
        public long RoleId { get; set; }

        public List<PermissionViewModel> PermissionList { get; set; }
        public string AssignedBy { get; set; }
    }
    public class UserPermission
    {
        public UserPermission()
        {
            PermissionList = new List<PermissionViewModel>();
        }
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public List<PermissionViewModel> PermissionList { get; set; }
    }
    public class RolePermission
    {
        public RolePermission() 
        {
            PermissionList = new List<PermissionViewModel>();
        }
        public long RoleId { get; set; }

        public List<PermissionViewModel> PermissionList { get; set; }
    }
    public class RemoveRolePermission
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}
