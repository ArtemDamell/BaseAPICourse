using Microsoft.AspNetCore.Mvc;
using WebAPIBasic.Auth;

namespace WebAPIBasic.Controllers.Auth
{
    // 127. Создать в проекте WebAPI в папке Controllers новую подпапку Auth, в ней контроллер AuthController
    [ApiController]
    public class AuthController : ControllerBase
    {
        // 132. Внедряем зависимость в контроллер AuthController
        private readonly ICustomUserManager _customUserManager;
        private readonly ICustomTokenManager _customTokenManager;

        
        public AuthController(ICustomUserManager customUserManager, ICustomTokenManager customTokenManager)
        {
            _customUserManager = customUserManager;
            _customTokenManager = customTokenManager;
        }
        // 132 ****************************************************

        //[HttpPost]
        //[Route("/authenticate")]
        //// 127.1 Назначаем метод аутентификации
        //public async Task<string> AuthenticateAsync(string username, string password)
        //{
        //    var result = await Task.FromResult(_customUserManager.Authenticate(username, password));
        //    return result;
        //}

        // 146.2 Получить ошибку, перейти в контроллер AuthController и добавить дополнительную логику
        [HttpPost]
        [Route("/authenticate")]
        // 127.1 Назначаем метод аутентификации
        public Task<string?> AuthenticateAsync(UserCredential userCredential)
        {
            var result = Task.FromResult(_customUserManager.Authenticate(userCredential.userName, userCredential.password));
            return result;
        }

        // 127.2
        [HttpGet]
        [Route("/verifytoken")]
        public Task<bool> VerifyTokenAsync(Token token)
        {
            var result = Task.FromResult(_customTokenManager.VerifyToken(token.token));
            return result;
        }

        // 127.3
        [HttpPost]
        [Route("/getuserinfo")]
        public Task<string?> GetUserInfoByTokenAsync(Token token)
        {
            var result = Task.FromResult(_customTokenManager.GetUserInformationByToken(token.token));
            return result;
        }
    }

    // 146.1 Получить ошибку, перейти в контроллер AuthController и добавить дополнительную логику
    public class UserCredential
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class Token
    {
        public string? token { get; set; }
    }
}
