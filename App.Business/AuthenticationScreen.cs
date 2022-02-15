using MyApp.Repository;

namespace MyApp.Business
{
    // 139. Создать новый класс в проекте Business AuthenticationScreen
    public class AuthenticationScreen : IAuthenticationScreen
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationScreen(IAuthenticationRepository authenticationRepository, ITokenRepository tokenRepository)
        {
            _authenticationRepository = authenticationRepository;
            _tokenRepository = tokenRepository;
        }
        public async Task<string?> LoginAsync(string userName, string password)
        {
            var token = await _authenticationRepository.LoginAsync(userName, password);
            return token;
        }

        public async Task<string?> GetUserInfoAsync(string? token)
        {
            return await _authenticationRepository.GetUserInfoAsync(token);
        }

        // После этого не забываем сделать Extract Interface

        // 159.1 В AuthenticationScreen добавить метод выхода из аккаунта LogOut
        public Task Logout()
        {
            return _tokenRepository.SetToken(string.Empty);
        }
    }
}
