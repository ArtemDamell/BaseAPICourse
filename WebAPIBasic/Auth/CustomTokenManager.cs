namespace WebAPIBasic.Auth
{
    // 128. Создать в корне проекта папку Auth, в ней 2 класса: CustomerUserManager, CustomerTokenManager
    public class CustomTokenManager : ICustomTokenManager
    {
        // 134. Добавляем логику в класс CustomeTokenManager, добавив и туда коллекцию с демо-данными
        List<Token> _tokens = new();
        // --> 134.1 Создать класс Token в папке Auth

        // 134.2 Реализовываем все методы по порядку
        public string CreateToken(string userName)
        {
            var token = new Token(userName);
            _tokens.Add(token);
            return token.TokenString;
        }

        public bool VerifyToken(string token)
        {
            var verifyed = _tokens.Any(x => token is not null && 
                                                                            token.Contains(x.TokenString) &&
                                                                            x.ExpiryDate > DateTime.Now);
            return verifyed;
        }

        public string GetUserInformationByToken(string token)
        {
            var findedToken = _tokens.FirstOrDefault(x => token is not null && token.Contains(x.TokenString));
            if (findedToken is not null)
                return findedToken.UserName;

            return string.Empty;
        }
    }
}
