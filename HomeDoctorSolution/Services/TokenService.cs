using HomeDoctorSolution.Constants;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Util.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace HomeDoctorSolution.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateToken(AccountToken account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimNames.USER_NAME, account.Username),
            new Claim(ClaimNames.ID, account.Id.ToString()),
            new Claim(ClaimNames.ROLE_ID, account.RoleId.ToString()),
            new Claim(ClaimTypes.Email, account.Email),

        };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMonths(int.Parse(_config["Jwt:ExpireTime"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Craeted: 10/08/2023
        /// Description: Generate token with expiry time
        /// </summary>
        /// <param name="account">payload info</param>
        /// <param name="expiryTime">expiry date</param>
        /// <returns></returns>
        public string GenerateToken(AccountToken account, DateTime expiryTime)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimNames.USER_NAME, account.Username),
            new Claim(ClaimNames.ID, account.Id.ToString()),
            new Claim(ClaimNames.ROLE_ID, account.RoleId.ToString()),
            new Claim(ClaimTypes.Email, account.Email),
        };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: expiryTime,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken ParseToken(string tokenString)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            return token;
        }

        public bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = true,
                RequireExpirationTime = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])), // The same key as the one that generate the token
            };
        }
    }
}
