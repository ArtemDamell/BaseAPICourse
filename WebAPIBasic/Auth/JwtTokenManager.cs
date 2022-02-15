using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPIBasic.Auth
{
    // 164. В папке Auth проекта API создать новый класс JwtTokenManager
    public class JwtTokenManager : ICustomTokenManager
    {
        // 164.1 Внедряем зависимость IConfiguration
        private readonly IConfiguration _configuration;

        // 164.2 Внедряем зависимость токен хэндлера
        private JwtSecurityTokenHandler _tokenHandler;
        private byte[] _secretKey;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenHandler = new JwtSecurityTokenHandler();
            _secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtSecretKey"));
        }
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return securityToken is not null;
        }
    }
}
