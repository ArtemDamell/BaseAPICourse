using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    // 141. Для хранения токена нам потребуется новый репозиторий. Создать TokenRepository
    public class TokenRepository : ITokenRepository
    {
        //public string Token { get; set; }

        // 153.2 Для взаимодействие с браузером пользователя, внедряем зависимость JS
        private readonly IJSRuntime _js;
        public TokenRepository(IJSRuntime js)
        {
            _js = js;
        }

        // 153.1 Реализовать 2 метода в TokenRepository SetToken(string token) и GetToken()
        public async Task SetToken(string token)
        {
            // sessionStorage.setItem - это JavaScript
            await _js.InvokeVoidAsync("sessionStorage.setItem", "token", token);
            //Token = token;
        }

        public async Task<string?> GetToken() => await _js.InvokeAsync<string?>("sessionStorage.getItem", "token");//return Token;
    }
    // Не забываем экстрактировать интерфейс. После, продолжаем реализовывать логику AuthenticationRepository, добавив в зависимости новый класс репозитория
}
