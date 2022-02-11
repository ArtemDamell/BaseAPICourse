using Core.StaticData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIBasic.Auth;

namespace WebAPIBasic.Filters
{
    // 135. Создать в папке Filters проекта WebAPI новый FilterAttribute (класс) CustomeTokenAuthFilterAttribute
    public class CustomeTokenAuthFilterAttribute: Attribute, IAuthorizationFilter
    {
        private readonly ICustomTokenManager _customTokenManager;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(SD.TokenHeader, out var token))
                context.Result = new UnauthorizedResult();

            //var tokenManager = context.HttpContext.RequestServices.GetService<ICustomTokenManager>();
            var tokenManager = context.HttpContext.RequestServices.GetService(typeof(ICustomTokenManager)) as ICustomTokenManager;
            if (tokenManager is null || !tokenManager.VerifyToken(token))
                context.Result = new UnauthorizedResult();
        }
    }
}
