using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System.Security.Claims;

namespace MyApp.Web
{
    public class CustomeTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;

        public CustomeTokenAuthenticationStateProvider(IAuthenticationRepository authenticationRepository, ITokenRepository tokenRepository)
        {
            _authenticationRepository = authenticationRepository;
            _tokenRepository = tokenRepository;
        }

        /// <summary>
        /// Gets the authentication state of the user.
        /// </summary>
        /// <returns>The authentication state of the user.</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userName = await _authenticationRepository.GetUserInfoAsync(await _tokenRepository.GetToken());
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var claims = new List<Claim>();
                var claim = new Claim(ClaimTypes.Name, userName);
                claims.Add(claim);

                var identity = new ClaimsIdentity(new[] { claim }, "Custome Token Auth");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
