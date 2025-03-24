using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Service
{
    public class BaseService
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public BaseService(IHttpContextAccessor contextAccessor)
        {
            HttpContextAccessor = contextAccessor;
        }

        public bool CheckUserRole(string requiredRole)
{
    var roleClaim = HttpContextAccessor.HttpContext?.User.Claims
        .FirstOrDefault(x => x.Type == ClaimTypes.Role);

    if (roleClaim == null)
    {
        throw new BaseException("User Role Error", 403, 
            new ErrorResponse("User Role Error", 403, "User does not have a role assigned"));
    }

    return roleClaim.Value.Equals(requiredRole, StringComparison.OrdinalIgnoreCase);
}
    }
}