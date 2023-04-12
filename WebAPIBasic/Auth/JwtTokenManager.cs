using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPIBasic.Auth
{
    public class JwtTokenManager : ICustomTokenManager
    {
        private readonly IConfiguration _configuration;
        private JwtSecurityTokenHandler _tokenHandler;
        private byte[] _secretKey;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenHandler = new JwtSecurityTokenHandler();
            _secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtSecretKey"));
        }

        /// <summary>
        /// Creates a token for the given user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The token.</returns>
        public string CreateToken(string userName)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName),
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Retrieves the user information from the given token.
        /// </summary>
        /// <param name="token">The token to use for retrieving the user information.</param>
        /// <returns>The user information associated with the given token.</returns>
        public string? GetUserInformationByToken(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var jwtToken = _tokenHandler.ReadToken(token.Replace("\"", string.Empty)) as JwtSecurityToken;
            var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name");
            if (claim is not null)
                return claim.Value;

            return null;
        }

        /// <summary>
        /// Verifies the given token is valid or not
        /// </summary>
        /// <param name="token">The token to be verified</param>
        /// <returns>True if the token is valid, false otherwise</returns>
        public bool VerifyToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            SecurityToken securityToken;

            try
            {
                _tokenHandler.ValidateToken(token.Replace("\"", string.Empty),
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                },
                out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return securityToken is not null;
        }
    }
}
