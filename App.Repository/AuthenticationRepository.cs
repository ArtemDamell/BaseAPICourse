using MyApp.Repository.ApiClient;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationRepository(IWebApiExecuter webApiExecuter, ITokenRepository tokenRepository)
        {
            _webApiExecuter = webApiExecuter;
            _tokenRepository = tokenRepository;
        }

        /// <summary>
        /// Login the user with the given username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The token of the user if the login was successful, otherwise null.</returns>
        public async Task<string?> LoginAsync(string username, string password)
        {
            var token = await _webApiExecuter.InvokePostReturnString("authenticate", new { username = username, password = password });
            await _tokenRepository.SetToken(token);

            if (string.IsNullOrWhiteSpace(token) || token.Equals("\"\""))
                return null;

            return token;
        }

        /// <summary>
        /// Gets the user information asynchronously.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The user name.</returns>
        public async Task<string?> GetUserInfoAsync(string? token)
        {
            var userName = await _webApiExecuter.InvokePostReturnString("getuserinfo", new { token = token });
            if (string.IsNullOrWhiteSpace(userName) || userName.Equals("\"\""))
                return null;
            return userName;
        }
    }
}
