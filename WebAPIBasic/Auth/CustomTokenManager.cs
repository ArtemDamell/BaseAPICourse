namespace WebAPIBasic.Auth
{
    public class CustomTokenManager : ICustomTokenManager
    {
        List<Token> _tokens = new();

        /// <summary>
        /// Creates a new token for the given user name and adds it to the list of tokens.
        /// </summary>
        /// <param name="userName">The user name to create the token for.</param>
        /// <returns>The token string of the newly created token.</returns>
        public string CreateToken(string userName)
        {
            var token = new Token(userName);
            _tokens.Add(token);
            return token.TokenString;
        }

        /// <summary>
        /// Verifies if the given token is valid.
        /// </summary>
        /// <param name="token">The token to be verified.</param>
        /// <returns>True if the token is valid, false otherwise.</returns>
        public bool VerifyToken(string token)
        {
            var verifyed = _tokens.Any(x => token is not null &&
                                                                            token.Contains(x.TokenString) &&
                                                                            x.ExpiryDate > DateTime.Now);
            return verifyed;
        }

        /// <summary>
        /// Gets the user information by token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The user name.</returns>
        public string? GetUserInformationByToken(string? token)
        {
            var findedToken = _tokens.FirstOrDefault(x => token is not null && token.Contains(x.TokenString));
            if (findedToken is not null)
                return findedToken.UserName;

            return string.Empty;
        }
    }
}
