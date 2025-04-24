using Microsoft.IdentityModel.Tokens;
using Server.Application.Interface;
using Server.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Application.Service
{
    public class TokenService : IJToken
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenrateToken(UserDetail user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)

            };
            var secretKey = _configuration["JWT:SecretKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new Exception("JWT SecretKey is missing from configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "InstagramClone", // Optional: hardcoded or add to config if needed
                audience: "InstagramCloneUser",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
