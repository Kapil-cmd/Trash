
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
                if (string.IsNullOrEmpty(model.RoleName))
                {
                    response.Status = "1";
                    response.Message = "ROLE NAME IS REQUIRED!!!";
                    return response;
                }
                if (string.IsNullOrWhiteSpace(model.CreatedBy))
                {
                    response.Status = "1";
                    response.Message = "UNABLE TO ADD ROILE!!!";
                    return response;
                }
                model.CreatedDateTime = DateTime.Now;
                var data = _rolePermissionRepository.AddRole(model);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                response.Data = data.Data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<string> UpdateRole(UpdateRoleViewModel model)
        {
            var response = new BaseResponseModel<string>();
            try
            {
                if (string.IsNullOrWhiteSpace(model.RoleName))
                {
                    response.Status = "1";
                    response.Message = "STATUS IS REQUIRED!!!";
                    return response;
                }
                if (model.RoleId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOS THE ROLE!!!";
                    return response;
                }
                if (string.IsNullOrWhiteSpace(model.Status))
                {
                    response.Status = "1";
                    response.Message = "ROLE NAME IS REQUIRED!!!";
                    return response;
                }
                var data = _rolePermissionRepository.UpdateRole(model);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<Role> GetRoleDetails(long roleId)
        {
            var response = new BaseResponseModel<Role>();
            try
            {
                if (roleId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOS THE ROLE!!!";
                    return response;
                }
                var data = _rolePermissionRepository.GetRoleDetails(roleId);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                response.Data = data.Data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<string> AssignUserRole(AssignRoleViewModel model)
        {
            var response = new BaseResponseModel<string>();
            try
            {
                if (model.UserId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOSE THE USER!!!";
                    return response;
                }
                if (model.RoleId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOSE THE ROLE!!!";
                    return response;
                }
                if (string.IsNullOrWhiteSpace(model.CreatedBy))
                {
                    response.Status = "1";
                    response.Message = "ERROR OCCURED WHILE ASSIGNING ROLE!!!";
                    return response;
                }
                var data = _rolePermissionRepository.AssignUserRole(model);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<string> DeleteUserRole(RemoveRoleViewModel model)
        {
            var response = new BaseResponseModel<string>();
            try
            {
                if (model.UserId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOSE THE USER!!!";
                    return response;
                }
                if (model.RoleId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOSE THE ROLE!!!";
                    return response;
                }
                var data = _rolePermissionRepository.DeleteUserRole(model);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<string> AssignRolePermission(AssignedRolePermission model)
        {
            var response = new BaseResponseModel<string>();
            try
            {
                if (model.PermissionList == null || model.PermissionList.Count() == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOSE THE USER!!!";
                    return response;
                }
                if (model.RoleId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOSE THE ROLE!!!";
                    return response;
                }
                if (string.IsNullOrWhiteSpace(model.AssignedBy))
                {
                    response.Status = "1";
                    response.Message = "UNABLE TO ASSIGNED PERMISSION!!!";
                    return response;
                }
                var data = _rolePermissionRepository.AssignRolePermission(model);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<UserPermission> GetUserPermission(long userId)
        {
            var response = new BaseResponseModel<UserPermission>();
            try
            {
                if (userId == 0)
                {
                    response.Status = "1";
                    response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                    return response;
                }
                var data = _rolePermissionRepository.GetUserPermission(userId);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                response.Data = data.Data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<string> RemoveRolePermission(RemoveRolePermission model)
        {
            var response = new BaseResponseModel<string>();
            try
            {
                if (model.PermissionId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLAEASE CHOOSE THE PERMISSION!!!";
                    return response;
                }
                if (model.RoleId == 0)
                {
                    response.Status = "1";
                    response.Message = "PLEASE CHOOSE THE ROLE!!";
                    return response;
                }
                var data = _rolePermissionRepository.RemoveRolePermission(model);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<List<RolePermission>> GetRolePermisison(long RoleId)
        {
            var response = new BaseResponseModel<List<RolePermission>>();
            try
            {
                if (RoleId == 0)
                {
                    response.Status = "1";
                    response.Message = "UNABLE TO GET PERMISSION LIST!!!";
                    return response;
                }
                var data = _rolePermissionRepository.GetRolePermisison(RoleId);
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                response.Data = data.Data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
        public BaseResponseModel<List<PermissionViewModel>> PermissionList()
        {
            var response = new BaseResponseModel<List<PermissionViewModel>>();
            try
            {
                var data = _rolePermissionRepository.PermissionList();
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                response.Data = data.Data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;

            }
        }
        public BaseResponseModel<List<RoleViewModel>> RoleList()
        {
            var response = new BaseResponseModel<List<RoleViewModel>>();
            try
            {
                var data = _rolePermissionRepository.RoleList();
                response.Status = data.ErrorCode;
                response.Message = data.Message;
                response.Data = data.Data;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;

            }
        }
    }
}
