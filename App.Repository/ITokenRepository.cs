using System.Threading.Tasks;

namespace MyApp.Repository
{
    public interface ITokenRepository
    {
        // 153.3 Удаляем свойство токен
        //string Token { get; set; }

        Task<string?> GetToken();
        Task SetToken(string token);
    }
}