using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace IK.SCP.App.Common.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                bool isAnonymous = context.ActionDescriptor.EndpointMetadata
                            .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
                if (isAnonymous)
                {
                    return;
                }

                var sr = new
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    success = false,
                    messages = new string[] { "Unauthorized" }
                };
                context.Result = new JsonResult(sr) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            var auth = context.HttpContext.Request.Headers["Authorization"];
        }
    }
}
