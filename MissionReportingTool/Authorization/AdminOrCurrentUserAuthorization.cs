using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using MissionReportingTool.Entitites;
using System.Security.Claims;

namespace MissionReportingTool.Authorization
{
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
