using MyApp.Repository;

namespace MyApp.Business
{
    // 139. Создать новый класс в проекте Business AuthenticationScreen
    public class AuthenticationScreen : IAuthenticationScreen
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationScreen(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        public async Task<string> LoginAsync(string userName, string password)
        {
            return await _authenticationRepository.LoginAsync(userName, password);
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            return await _authenticationRepository.GetUserInfoAsync(token);
        }

        // После этого не забываем сделать Extract Interface
    }
}
