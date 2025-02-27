using HomeDoctorSolution.Util.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace HomeDoctorSolution.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AccountToken account);
        bool ValidateToken(string authToken);
        JwtSecurityToken ParseToken(string tokenString);
        string GenerateToken(AccountToken account, DateTime expiryTime);
    }
}
