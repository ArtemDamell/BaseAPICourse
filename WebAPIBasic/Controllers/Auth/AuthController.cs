using Microsoft.AspNetCore.Mvc;
using WebAPIBasic.Auth;

namespace WebAPIBasic.Controllers.Auth
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomUserManager _customUserManager;
        private readonly ICustomTokenManager _customTokenManager;

        public AuthController(ICustomUserManager customUserManager, ICustomTokenManager customTokenManager)
        {
            _customUserManager = customUserManager;
            _customTokenManager = customTokenManager;
        }

        /// <summary>
        /// Authenticates a user with the given credentials.
        /// </summary>
        /// <param name="userCredential">The user credentials.</param>
        /// <returns>A task that represents the asynchronous operation, containing the authentication result.</returns>
        [HttpPost]
        [Route("/authenticate")]
        public Task<string?> AuthenticateAsync(UserCredential userCredential)
        {
            var result = Task.FromResult(_customUserManager.Authenticate(userCredential.userName, userCredential.password));
            return result;
        }

        /// <summary>
        /// Verifies the given token.
        /// </summary>
        /// <param name="token">The token to verify.</param>
        /// <returns>A boolean indicating whether the token is valid.</returns>
        [HttpGet]
        [Route("/verifytoken")]
        public Task<bool> VerifyTokenAsync(Token token)
        {
            var result = Task.FromResult(_customTokenManager.VerifyToken(token.token));
            return result;
        }

        /// <summary>
        /// Gets the user information by token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The user information.</returns>
        [HttpPost]
        [Route("/getuserinfo")]
        public Task<string?> GetUserInfoByTokenAsync(Token token)
        {
            var result = Task.FromResult(_customTokenManager.GetUserInformationByToken(token.token));
            return result;
        }
    }

    public class UserCredential
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class Token
    {
        public string? token { get; set; }
    }
}
