using Core.StaticData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIBasic.Filters
{
    public class APIKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Checks the request headers for the ClientId and ApiKey and compares them to the values stored in the configuration. If the values do not match, an UnauthorizedResult is returned.
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(SD.ClientIdHeader, out var clientId))
                context.Result = new UnauthorizedResult();

            if (!context.HttpContext.Request.Headers.TryGetValue(SD.ApiKeyHeader, out var clientApiKey))
                context.Result = new UnauthorizedResult();

            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

            var apiKey = config.GetValue<string>($"ApiKey:{clientId}");

            if (!apiKey.Equals(clientApiKey))
                context.Result = new UnauthorizedResult();
        }
    }
}
