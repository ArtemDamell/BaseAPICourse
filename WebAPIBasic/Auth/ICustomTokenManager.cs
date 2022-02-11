namespace WebAPIBasic.Auth
{
    public interface ICustomTokenManager
    {
        string CreateToken(string userName);
        string? GetUserInformationByToken(string? token);
        bool VerifyToken(string token);
    }
}