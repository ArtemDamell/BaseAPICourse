namespace WebAPIBasic.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        Dictionary<string, string> _credentials = new Dictionary<string, string>()
        {
            { "bob", "password"},
            { "john", "password2"}
        };

        private readonly ICustomTokenManager _customTokenManager;
        public CustomUserManager(ICustomTokenManager customTokenManager) => _customTokenManager = customTokenManager;

        /// <summary>
        /// Authenticates the user with the given username and password and returns a token if successful.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A token if authentication is successful, otherwise an empty string.</returns>
        public string? Authenticate(string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !_credentials[userName].Equals(password))
                return string.Empty;

            var token = _customTokenManager.CreateToken(userName);
            return token;
        }
    }
}
