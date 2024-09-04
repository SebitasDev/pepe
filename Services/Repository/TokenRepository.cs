using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using RiwiTalent.Models;

namespace backend.Services.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly string key;
        private readonly string Issuer;
        private readonly string Audience;
        public TokenRepository(IConfiguration configuration)
        {
            key = Environment.GetEnvironmentVariable("Key");
            Issuer = Environment.GetEnvironmentVariable("Issuer");
            Audience = Environment.GetEnvironmentVariable("Audience");
        }

        public string GetToken(User user)
        {
           
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(key));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var tokenOptions = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.Aes192CbcHmacSha384)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenOptions);
            var tokenString = tokenHandler.WriteToken(securityToken);

            return tokenString;
            
        }
    }
}