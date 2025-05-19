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
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // unique token ID
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // ✅ set real user ID
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Name, user.Name)
};



            var secretKey = _configuration["JWT:SecretKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new Exception("JWT SecretKey is missing from configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
        issuer: "InstagramClone",
        audience: "InstagramCloneUser",
        claims: claims,
        expires: DateTime.UtcNow.AddDays(1),
        signingCredentials: creds
    );


            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
