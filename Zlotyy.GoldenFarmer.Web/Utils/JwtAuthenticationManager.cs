using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Zlotyy.GoldenFarmer.Web.Utils
{
    public interface IJwtAuthenticationManager
    {
        string CreateToken(string userId);
    }

    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly AppSettings _settings;
        private const int EXPIRES_MINUTES = 60;

        public JwtAuthenticationManager(IOptions<AppSettings> options)
        {
            _settings = options.Value;
        }

        public string CreateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = GetSecretKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.Now.AddMinutes(EXPIRES_MINUTES),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private byte[] GetSecretKey()
        {
            return Encoding.ASCII.GetBytes(_settings.Secret);
        }
    }
}
