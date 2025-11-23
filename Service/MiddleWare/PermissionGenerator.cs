
using Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Web.Mvc;

public static class PermissionScanner
{
    public static void GeneratePermission(string _connectionString)
    {
        try
        {
            List<Permission> permission = new List<Permission>();

            Assembly asm = Assembly.GetEntryAssembly();

            var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in loadedAssembly)
            {
                var typePages = assembly.GetTypes().Where(x => x.FullName.Contains("Controller"));

                foreach (Type type in typePages)
                {
                    var methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).ToList();
                    foreach (var method in methods)
                    {
                        var actionMethod = method.CustomAttributes.Where(x => x.AttributeType.Name == nameof(AuthorizePermission)).ToList();
                        foreach (var action in actionMethod)
                        {
                            if (action.ConstructorArguments[0].Value == null)
                            {
                                continue;
                            }
                            string slug = action.ConstructorArguments[0].Value.ToString();
                            string actionName = method.Name;
                            string controllerName = method.ReflectedType.Name.Replace("Controller", "");
                            string createdBy = "SuperAdmin";
                            var dateTime = DateTime.Now;
                            using var con = new SqlConnection(_connectionString);
                            con.Open();
                            var checkSql = @"SELECT 1 FROM DTbl_Permission WHERE SlugName = @slug";
                            using var checkCmd = new SqlCommand(checkSql, con);
                            checkCmd.Parameters.AddWithValue("@slug", slug);

                            var exists = checkCmd.ExecuteScalar();

                            if (exists == null)
                            {
                                var sql = @"
                                        INSERT INTO DTbl_Permission
                                            (SlugName, ControllerName, ActionName, CreatedBy, CreatedDateTime)
                                        VALUES
                                            (@slug, @controllerName, @actionName, @CreatedBy, @dateTime)";

                                using var cmd = new SqlCommand(sql, con);
                                cmd.Parameters.AddWithValue("@slug", slug);
                                cmd.Parameters.AddWithValue("@controllerName", controllerName);
                                cmd.Parameters.AddWithValue("@actionName", actionName);
                                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }

                            // 1. Get PermissionId
                            int permissionId;

                            using (var getPermissionCmd = new SqlCommand(
                                "SELECT PermissionId FROM DTbl_Permission WHERE SlugName = @slug", con))
                            {
                                getPermissionCmd.Parameters.AddWithValue("@slug", slug);
                                permissionId = Convert.ToInt32(getPermissionCmd.ExecuteScalar());
                            }


                            int roleId;

                            using (var roleCmd = new SqlCommand(
                                "SELECT RoleId FROM CTbl_Roles WHERE RoleName = 'SuperAdmin'", con))
                            {
                                var result = roleCmd.ExecuteScalar();

                                if (result == null)
                                {
                                    Console.Write("SuperAdmin role not found.");
                                    break;
                                    throw new Exception("SuperAdmin role not found.");
                                }

                                roleId = Convert.ToInt32(result);
                            }


                            var checkPermissionSql = @"
                                  SELECT 1 
                                  FROM CTbl_RolePermission 
                                  WHERE PermissionId = @PermissionId AND RoleId = @RoleId";

                            using (var checkPermissionCmd = new SqlCommand(checkPermissionSql, con))
                            {
                                checkPermissionCmd.Parameters.AddWithValue("@PermissionId", permissionId);
                                checkPermissionCmd.Parameters.AddWithValue("@RoleId", roleId);

                                var existsRolePermission = checkPermissionCmd.ExecuteScalar();

                                if (existsRolePermission == null)
                                {
                                    var insertRolePermissionSql = @"
                                        INSERT INTO CTbl_RolePermission
                                            (RoleId, PermissionId, SlugName, AssignedBy, AssignedDate)
                                        VALUES
                                            (@RoleId, @PermissionId, @SlugName, @AssignedBy, @AssignedDate)";

                                    using var insertCmd = new SqlCommand(insertRolePermissionSql, con);
                                    insertCmd.Parameters.AddWithValue("@RoleId", roleId);
                                    insertCmd.Parameters.AddWithValue("@PermissionId", permissionId);
                                    insertCmd.Parameters.AddWithValue("@SlugName", slug);
                                    insertCmd.Parameters.AddWithValue("@AssignedBy", createdBy);
                                    insertCmd.Parameters.AddWithValue("@AssignedDate", DateTime.Now);

                                    insertCmd.ExecuteNonQuery();
                                }
                            }


                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {

        }

    }
    public class Permission
    {
        public string SlugName { get; set; }
        public string MethodName { get; set; }
        public string ControllerNmae { get; set; }
        public string CreatedBy { get; set; }
    }
}
