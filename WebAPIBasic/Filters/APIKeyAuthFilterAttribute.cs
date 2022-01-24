using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIBasic.Filters
{
    // 118. Для добавления подписи для пользователся, добавим в API проект в папку Filters новый класс APIKeyAuthFilterAttribute
    public class APIKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "ApiKey";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Проверяем, имеется ли токен в заголовках запроса
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var clientApiKey))
                context.Result = new UnauthorizedResult();

            // Внедряем зависимость сервиса в фильтр
            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

            // Получаем секретный ключ из файла AppSettings.json
            var apiKey = config.GetValue<string>(ApiKeyHeader);

            if (!apiKey.Equals(clientApiKey))
                context.Result = new UnauthorizedResult();


        }
    }
}
