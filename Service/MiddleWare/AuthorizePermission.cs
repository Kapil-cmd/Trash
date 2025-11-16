using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Application
{
    public class AuthorizePermission : TypeFilterAttribute
    {
        private string actionName = string.Empty;
        public AuthorizePermission(string claimValue = null, string module = null) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(actionName, claimValue ?? ""), new Claim(actionName, module ?? "ADMIN") };
        }
        public class ClaimRequirementFilter : IAsyncActionFilter
        {
            private readonly Claim _claim;
            private readonly Claim _module;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public ClaimRequirementFilter(
            Claim claim, Claim module, IHttpContextAccessor httpContextAccessor)
            {
                _claim = claim;
                _module = module;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var distributor = (ControllerActionDescriptor)context.ActionDescriptor;
                var actionName = distributor.ActionName;
                var controllerName = distributor.ControllerName;
                var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var token = authHeader.Substring("Bearer".Length).Trim();
                var values = StaticMethods.ParseToken(token);
                if (string.IsNullOrWhiteSpace(token))
                {
                    context.Result = new OkObjectResult(new BaseResponseModel<string>("404", "UNAUTHORIZED"));
                    return;
                }
                if(values == null)
                {
                    context.Result = new OkObjectResult(new BaseResponseModel<string>(values.Status, values.Message));
                    return;
                }
                if(values.Status == "405")
                {
                    context.Result = new OkObjectResult(new BaseResponseModel<string>(values.Status, values.Message));
                    return;
                }
                if (values.Data.UserName.ToLower() == "superadmin")
                {
                    await next();
                    return;
                }
                var slug = _claim.Value;
                var userId = values.Data.UserId;

                var connectionString = DefaultConfiguration.StaticConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value;
                using var con = new SqlConnection(connectionString);
                con.Open();
                var roleQuery = @"SELECT * FROM Ctbl_UserRole WHERE UserId = @userId";
                using var cmd = new SqlCommand(roleQuery, con);
                cmd.Parameters.AddWithValue("@userId", userId); 

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var roleId = reader["RoleId"]; 
                    var roleName = reader["RoleName"]; 

                    Console.WriteLine($"RoleId: {roleId}, RoleName: {roleName}");
                }
                cmd.Parameters.AddWithValue("@userId", userId);
            }
        }
    }
}
