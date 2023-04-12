using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IJSRuntime _js;
        public TokenRepository(IJSRuntime js)
        {
            _js = js;
        }

        /// <summary>
        /// Sets the token in session storage.
        /// </summary>
        public async Task SetToken(string token) => await _js.InvokeVoidAsync("sessionStorage.setItem", "token", token);

        /// <summary>
        /// Asynchronously gets the token from session storage.
        /// </summary>
        /// <returns>The token from session storage.</returns>
        public async Task<string?> GetToken() => await _js.InvokeAsync<string?>("sessionStorage.getItem", "token");
    }
}
