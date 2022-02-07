using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System.Security.Claims;

namespace MyApp.Web
{
    // 148. Создать в корне проекта MyApp.Web новый класс CustomeTokenAuthenticationStateProvider
    public class CustomeTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        // 155.1 Внедряем зависимости
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;
        public CustomeTokenAuthenticationStateProvider(IAuthenticationRepository authenticationRepository, ITokenRepository tokenRepository)
        {
            _authenticationRepository = authenticationRepository;
            _tokenRepository = tokenRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // 152. Сейчас была проверена ситуация, когда пользователь не авторизован! Авторизуем пользователя через CustomeTokenAuthenticationStateProvider
            //var userName = string.Empty;
            //var userName = "john";
            // 155.2 Внести изменения в класс CustomeTokenAuthenticationStateProvider
            var userName = await _authenticationRepository.GetUserInfoAsync(await _tokenRepository.GetToken());
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var claim = new Claim(ClaimTypes.Name, userName);
                var identity = new ClaimsIdentity(new[] { claim }, "Custome Token Auth");
                var principal = new ClaimsPrincipal(identity);

                //return Task.FromResult(new AuthenticationState(principal));
                return new AuthenticationState(principal);
            }
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
