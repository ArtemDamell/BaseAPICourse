namespace WebAPIBasic.Auth
{
    // 128. Создать в корне проекта папку Auth, в ней 2 класса: CustomerUserManager, CustomerTokenManager
    public class CustomUserManager : ICustomUserManager
    {
        // 133. Создать демо данные для проверки функционала в CustomeUserManager
        Dictionary<string, string> _credentials = new Dictionary<string, string>()
        {
            { "bob", "password"},
            { "john", "password2"}
        };

        private readonly ICustomTokenManager _customTokenManager;

        // 130. Внедряем в классы зависимости друг друга
        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            _customTokenManager = customTokenManager;
        }

        public string Authenticate(string userName, string password)
        {
            // 1. Validate user
            // 133.1
            //if (!_credentials[userName].Equals(password))
            //    return string.Empty;
            if (!string.IsNullOrWhiteSpace(userName) && !_credentials[userName].Equals(password))
                return string.Empty;

            // 2. Generate token
            var token = _customTokenManager.CreateToken(userName);
            return token;
        }
    }
}
