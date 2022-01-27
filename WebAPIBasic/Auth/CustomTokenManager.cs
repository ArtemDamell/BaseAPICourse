namespace WebAPIBasic.Auth
{
    // 128. Создать в корне проекта папку Auth, в ней 2 класса: CustomerUserManager, CustomerTokenManager
    public class CustomTokenManager : ICustomTokenManager
    {
        public string CreateToken(string userName)
        {
            return "";
        }

        public bool VerifyToken(string token)
        {
            return false;
        }

        public string GetUserInformationByToken(string token)
        {
            return "";
        }
    }
}
