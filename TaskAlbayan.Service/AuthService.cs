using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.DB.Models;

namespace albayan.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public TokenResponseDto GenerateJwtToken(User user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, ((int)user.Role).ToString())
        };

                var expiresAt = DateTime.UtcNow.AddDays(1);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expiresAt,
                    signingCredentials: credentials
                );

                return new TokenResponseDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Role = (int)user.Role,
                    ExpiresAt = expiresAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating JWT token.", ex);
            }
        }
    }
}