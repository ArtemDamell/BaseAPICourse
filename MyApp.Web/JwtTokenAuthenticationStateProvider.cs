﻿using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyApp.Web
{
    public class JwtTokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IAuthenticationRepository authenticationRepository;

        public JwtTokenAuthenticationStateProvider(ITokenRepository tokenRepository,
            IAuthenticationRepository authenticationRepository)
        {
            this.tokenRepository = tokenRepository;
            this.authenticationRepository = authenticationRepository;
        }

        /// <summary>
        /// Gets the authentication state of the user.
        /// </summary>
        /// <returns>The authentication state of the user.</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var tokenString = await tokenRepository.GetToken();
            if (!string.IsNullOrWhiteSpace(tokenString))
            {
                var tokenJwt = tokenHandler.ReadToken(tokenString.Replace("\"", string.Empty)) as JwtSecurityToken;

                if (tokenJwt != null)
                {
                    var claims = new List<Claim>();
                    claims.AddRange(tokenJwt.Claims);

                    var nameClaim = tokenJwt.Claims.FirstOrDefault(x => x.Type == "unique_name");
                    var roleClaim = tokenJwt.Claims.FirstOrDefault(x => x.Type == "role");
                    if (nameClaim != null) claims.Add(new Claim(ClaimTypes.Name, nameClaim.Value));
                    if (roleClaim != null) claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));

                    var identity = new ClaimsIdentity(claims, "Custom Token Auth");
                    var principal = new ClaimsPrincipal(identity);

                    return new AuthenticationState(principal);
                }
                else
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
