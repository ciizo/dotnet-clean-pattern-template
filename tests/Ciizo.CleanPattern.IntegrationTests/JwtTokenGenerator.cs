using Ciizo.CleanPattern.Domain.Business.Common.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Ciizo.CleanPattern.IntegrationTests
{
    public static class JwtTokenGenerator
    {
        public static string GenerateJwtToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("ciizojwtsigningkey12345678900000");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "ciizo",
                Audience = "ciizo",
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object> {
                    { Api.Auth.ClaimTypes.UserType, nameof(UserTypes.Admin) },
                }
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}