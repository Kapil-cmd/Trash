using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
    public class UpdateRoleViewModel
    {
        [Required]
        public long RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string Status { get; set; }
    }
    public class AssignRoleViewModel
    {
        [Required]
        public long RoleId { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
    public class RemoveRoleViewModel
    {
        [Required]
        public long RoleId { get; set; }
        [Required]
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
        [Required]
        public long RoleId { get; set; }
        [Required]
        public long PermissionId { get; set; }
    }
}
