using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using MissionReportingTool.Entitites;
using System.Security.Claims;

namespace MissionReportingTool.Authorization
{
    /// <summary>
    /// This attribute can be used for endpoints that should allow only the admin or the current user to access it. The endpoint should have and id path parameter that represents the user id.
    /// </summary>
    public class AdminOrCurrentUserAuthorization : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var routeData = httpContext.GetRouteData();
            var requestedId = routeData?.Values["id"]?.ToString();
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.FindFirst("userId")?.Value;
            var role = identity?.FindFirst(identity.RoleClaimType)?.Value;

            if (role != Role.ADMIN.ToString() && userId != requestedId)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
