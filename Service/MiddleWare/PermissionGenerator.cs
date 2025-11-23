
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
