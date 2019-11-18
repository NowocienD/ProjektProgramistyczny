using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace GradeBook.API
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string GenerateToken(int username, SymmetricSecurityKey signingKey)
        {            
            if (signingKey is null)
            {
                throw new ArgumentNullException(nameof(signingKey));
            }

            var token = new JwtSecurityToken(
                claims: new Claim[] { new Claim(ClaimTypes.Name, username.ToString()) },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(5)).DateTime,
                signingCredentials: new SigningCredentials(
                    signingKey,
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
