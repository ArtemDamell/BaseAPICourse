namespace WebAPIBasic.Auth
{
    // 128. Создать в корне проекта папку Auth, в ней 2 класса: CustomerUserManager, CustomerTokenManager
    public class CustomUserManager : ICustomUserManager
    {
        private readonly ICustomTokenManager _customTokenManager;

        // 130. Внедряем в классы зависимости друг друга
        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            _customTokenManager = customTokenManager;
        }

        public string Authenticate(string userName, string password)
        {
            // 1. Validate user

            // 2. Generate token
            var token = _customTokenManager.CreateToken(userName);
            return token;
        }
    }
}
