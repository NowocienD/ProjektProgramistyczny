using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GradebookBackend.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly ITokenSettings tokenSettings;

        private readonly SymmetricSecurityKey signingKey;

        public TokenGeneratorService(ITokenSettings tokenSettings)
        {
            this.tokenSettings = tokenSettings;
            signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));
        }

        public string GenerateToken(int userId)
        {
            if (signingKey is null)
            {
                throw new ArgumentNullException(nameof(signingKey));
            }

            var token = new JwtSecurityToken(
                claims: new Claim[] { new Claim(nameof(userId), userId.ToString()) },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(5)).DateTime,
                signingCredentials: new SigningCredentials(
                    signingKey,
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
