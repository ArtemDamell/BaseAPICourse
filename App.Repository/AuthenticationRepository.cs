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
            var token = await _webApiExecuter.InvokePostReturnString("authenticate", new { username, password });
            _tokenRepository.Token = token;
            return token;
        }

        // --> 140.1 На этом этапе создать в WebApiExecuter новый метод InvokePostReturnString

        // 140.2 Возвращаемся в репозиторий
        public async Task<string> GetUserInfoAsync(string token)
        {
            return await _webApiExecuter.InvokePostReturnString("getuserinfo", new { token });
        }
    }
}
