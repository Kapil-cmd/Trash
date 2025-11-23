
using Core;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Infrastructure
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly string spName = "PROC_ROLE";
        private readonly DbConnectionFactory _dbConnectionFactory;
        public RolePermissionRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public SpResponse<string> AddRole(AddRoleViewModel model)
        {
            var response = new SpResponse<string>();
            try
            {
                using (var connection = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Flag", spName);
                    command.Parameters.AddWithValue("@RoleName", model.RoleName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedDateTime", DateTime.Now);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErrorMessage"].ToString();
                        }
                        else
                        {
                            response.ErrorCode = "1";
                            response.Message = "UNABLE TO ADD ROLE!!!";
                        }
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<string> AssignRolePermission(AssignedRolePermission model)
        {
            var response = new SpResponse<string>();
            try
            {
                var permission = JsonConvert.SerializeObject(model.PermissionList);
                using (var connection = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "AssignPermssion");
                    command.Parameters.AddWithValue("@RoleId", model.RoleId);
                    command.Parameters.AddWithValue("@Permission", permission);
                    command.Parameters.AddWithValue("@CreatedBy", model.AssignedBy ?? (object)DBNull.Value);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErrorMessage"].ToString();
                        }
                        else
                        {
                            response.ErrorCode = "1";
                            response.Message = "UNABLE TO ASSIGN PERMISSION!!!";
                        }
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<string> AssignUserRole(AssignRoleViewModel model)
        {
            var response = new SpResponse<string>();
            try
            {
                using (var connection = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "AssignRole");
                    command.Parameters.AddWithValue("@RoleId", model.RoleId);
                    command.Parameters.AddWithValue("@UserId", model.UserId);
                    command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy ?? (object)DBNull.Value);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErrorMessage"].ToString();
                        }
                        else
                        {
                            response.ErrorCode = "1";
                            response.Message = "UNABLE TO ASSIGNUSERROLE!!!";
                        }
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<string> DeleteUserRole(RemoveRoleViewModel model)
        {
            var response = new SpResponse<string>();
            try
            {
                using (var con = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "RemoveUserRole");
                    command.Parameters.AddWithValue("@UserId", model.UserId);
                    command.Parameters.AddWithValue("@RoleId", model.RoleId);

                    con.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErrorMessage"].ToString();
                        }
                        else
                        {
                            response.ErrorCode = "1";
                            response.Message = "UNABLE TO REMOVE ROLE FROM USER!!!";
                        }
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<Role> GetRoleDetails(long roleId)
        {
            var response = new SpResponse<Role>();
            try
            {
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<List<RolePermission>> GetRolePermisison(long RoleId)
        {
            var response = new SpResponse<List<RolePermission>>();
            try
            {
                var model = new RolePermission();
                using (var con = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "GetRolePermission");
                    command.Parameters.AddWithValue("@RoleId", RoleId);
                    con.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var permission = new PermissionViewModel();
                            permission.ActionName = reader["ActionName"].ToString();
                            permission.PermissionId = Convert.ToInt64(reader["PermissionId"]);
                            permission.ControllerName = reader["ControllerName"].ToString();
                            permission.SlugName = reader["SlugName"].ToString();

                            model.PermissionList.Add(permission);
                            model.RoleId = RoleId;
                            response.Message = "PERMISSION FETCHED SUCESSFULLY!!!";
                            response.ErrorCode = "000";
                        }
                        else
                        {
                            response.ErrorCode = "1";
                            response.Message = "UNABLE TO FETCH PERMISSION!!!";
                        }
                        return response;
                    }
;
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<UserPermission> GetUserPermission(long userId)
        {
            var response = new SpResponse<UserPermission>();
            try
            {
                var model = new UserPermission();

                using (var con = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "GetUserPermission");
                    command.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var permission = new PermissionViewModel
                            {
                                ActionName = reader["ActionName"].ToString(),
                                ControllerName = reader["ControllerName"].ToString(),
                                PermissionId = Convert.ToInt64(reader["PermissionId"]),
                                SlugName = reader["SlugName"].ToString(),
                            };
                            model.PermissionList.Add(permission);
                        }
                        else
                        {
                            response.ErrorCode = "1";
                            response.Message = "UNABLE TO GET USER PERMISSION!!!";
                        }
                        model.UserId = userId;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<List<PermissionViewModel>> PermissionList()
        {
            var response = new SpResponse<List<PermissionViewModel>>();
            try
            {
                var list = new List<PermissionViewModel>();
                using (var con = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "PermissionList");

                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new PermissionViewModel()
                            {
                                ActionName = reader["ActionName"].ToString(),
                                PermissionId = Convert.ToInt64(reader["PermissionId"]),
                                ControllerName = reader["ControllerName"].ToString(),
                                SlugName = reader["SlugName"].ToString()
                            };
                            list.Add(model);
                        }
                        return response;
                    }
                }

            }catch(Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<string> RemoveRolePermission(RemoveRolePermission model)
        {
            var response = new SpResponse<string>();
            try
            {
                using (var con = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "RemovePermission");
                    command.Parameters.AddWithValue("@RoleId", model.RoleId);
                    command.Parameters.AddWithValue("@PermissionId", model.PermissionId);

                    con.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErrorMessage"].ToString();
                        }
                        else
                        {
                            response.ErrorCode = "1";
                            response.Message = "UNABLE TO REMOVE ROLE PERMISSION!!!";
                        }
                        return response;
                    }

                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<List<RoleViewModel>> RoleList()
        {
            var response = new SpResponse<List<RoleViewModel>>();
            try
            {
                var list = new List<RoleViewModel>();
                using (var con = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "RoleList");
                     con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new RoleViewModel()
                            {
                                RoleId = Convert.ToInt64(reader["RoleId"]),
                                RoleName = reader["RoleName"].ToString()
                            };
                            list.Add(model);
                        }
                        return response;
                    }
                }
            }catch(Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "UNABLE TO GET ROLE LIST!!!";
                return response;
            }
        }

        public SpResponse<string> UpdateRole(UpdateRoleViewModel model)
        {
            var response = new SpResponse<string>();
            try
            {
                using (var con = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Flag", "UpdateRole");
                    command.Parameters.AddWithValue("@RoleId", model.RoleId);
                    command.Parameters.AddWithValue("@RoleName", model.RoleName ?? (object)DBNull.Value);

                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErrorMessage"].ToString();
                        }
                        else
                        {
                            response.ErrorCode = "1";
                            response.Message = "UNABLE TO UPDATE ROLE!!!";
                        }
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRES WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
    }
}
