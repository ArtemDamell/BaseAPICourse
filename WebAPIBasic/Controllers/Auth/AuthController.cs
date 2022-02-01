using Microsoft.AspNetCore.Mvc;
using WebAPIBasic.Auth;

namespace WebAPIBasic.Controllers.Auth
{
    // 127. Создать в проекте WebAPI в папке Controllers новую подпапку Auth, в ней контроллер AuthController
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost]
        [Route("/authenticate")]
        // 127.1 Назначаем метод аутентификации
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var result = await Task.FromResult(_customUserManager.Authenticate(username, password));
            return result;
        }

        // 127.2
        [HttpGet]
        [Route("/verifytoken")]
        public async Task<bool> VerifyTokenAsync(string token)
        {
            var result = await Task.FromResult(_customTokenManager.VerifyToken(token));
            return result;
        }

        // 127.3
        [HttpGet]
        [Route("/getuserinfo")]
        public async Task<string> GetUserInfoByTokenAsync(string token)
        {
            var result = await Task.FromResult(_customTokenManager.GetUserInformationByToken(token));
            return result;
        }
    }
}
