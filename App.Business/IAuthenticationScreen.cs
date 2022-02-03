
namespace MyApp.Business
{
    public interface IAuthenticationScreen
    {
        Task<string> GetUserInfoAsync(string token);
        Task<string> LoginAsync(string userName, string password);
    }
}