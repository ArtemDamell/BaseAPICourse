using MyApp.Repository;

namespace MyApp.Business
{
    public class AuthenticationScreen : IAuthenticationScreen
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationScreen(IAuthenticationRepository authenticationRepository, ITokenRepository tokenRepository)
        {
            _authenticationRepository = authenticationRepository;
            _tokenRepository = tokenRepository;
        }
        /// <summary>
        /// Logs in a user with the given username and password and returns a token.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A token for the user.</returns>
        public async Task<string?> LoginAsync(string userName, string password)
        {
            var token = await _authenticationRepository.LoginAsync(userName, password);
            return token;
        }

        /// <summary>
        /// Retrieves user information from the authentication repository.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <returns>A task that represents the asynchronous operation, containing the user information.</returns>
        public Task<string?> GetUserInfoAsync(string? token) => _authenticationRepository.GetUserInfoAsync(token);

        /// <summary>
        /// Logs out the current user by setting the token to an empty string.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public Task Logout() => _tokenRepository.SetToken(string.Empty);
    }
}
