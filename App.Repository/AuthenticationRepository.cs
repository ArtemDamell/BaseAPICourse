using MyApp.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    // 140. Создать класс AuthenticationRepository
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationRepository(IWebApiExecuter webApiExecuter, ITokenRepository tokenRepository)
        {
            _webApiExecuter = webApiExecuter;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var token = await _webApiExecuter.InvokePostReturnString("authenticate", new { username = username, password = password });
            // 154.2 Исправляем ошибки после удаления свойства из интерфейса репозитория ITokenRepository
            //_tokenRepository.Token = token;
            await _tokenRepository.SetToken(token);

            if (string.IsNullOrWhiteSpace(token) || token.Equals("\"\""))
                return null;

            return token;
        }

        // --> 140.1 На этом этапе создать в WebApiExecuter новый метод InvokePostReturnString

        // 140.2 Возвращаемся в репозиторий
        public async Task<string?> GetUserInfoAsync(string? token)
        {
            var userName = await _webApiExecuter.InvokePostReturnString("getuserinfo", new { token = token });
            if (string.IsNullOrWhiteSpace(userName) || userName.Equals("\"\""))
                return null;
            return userName;
        }
    }
}
