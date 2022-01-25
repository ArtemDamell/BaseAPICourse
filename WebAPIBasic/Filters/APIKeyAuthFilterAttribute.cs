using Core.StaticData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIBasic.Filters
{
    // 118. Для добавления подписи для пользователся, добавим в API проект в папку Filters новый класс APIKeyAuthFilterAttribute
    public class APIKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        //private const string ApiKeyHeader = "ApiKey";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 123.1 Внедряем функционал Multiply clients. На этом этапе выносим все константы в отдельный файл SD проекта Core
            if (!context.HttpContext.Request.Headers.TryGetValue(SD.ClientIdHeader, out var clientId))
                context.Result = new UnauthorizedResult();

            // Проверяем, имеется ли токен в заголовках запроса
            if (!context.HttpContext.Request.Headers.TryGetValue(SD.ApiKeyHeader, out var clientApiKey))
                context.Result = new UnauthorizedResult();

            // Внедряем зависимость сервиса в фильтр
            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;

            // Получаем секретный ключ из файла AppSettings.json
            //var apiKey = config.GetValue<string>(SD.ApiKeyHeader);

            // 123.2 
            var apiKey = config.GetValue<string>($"ApiKey:{clientId}");

            if (!apiKey.Equals(clientApiKey))
                context.Result = new UnauthorizedResult();


        }
    }
}
