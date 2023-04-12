using Core.StaticData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIBasic.Auth;

namespace WebAPIBasic.Filters
{
    public class CustomeTokenAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Checks the request header for a valid token and verifies it using the ICustomTokenManager service.
        /// If the token is not valid, an UnauthorizedResult is returned.
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(SD.TokenHeader, out var token))
                context.Result = new UnauthorizedResult();

            var tokenManager = context.HttpContext.RequestServices.GetService(typeof(ICustomTokenManager)) as ICustomTokenManager;
            if (tokenManager is null || !tokenManager.VerifyToken(token))
                context.Result = new UnauthorizedResult();
        }
    }
}
