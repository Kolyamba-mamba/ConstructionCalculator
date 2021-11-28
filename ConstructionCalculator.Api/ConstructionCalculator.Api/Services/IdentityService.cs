using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Services
{
    public class IdentityService : IIdentityService
    {
        public async Task<AuthResult> AuthenticationUserAsunc(string login, string password, IRepository<User> repository)
        {
            var authResult = new AuthResult();
            var account = await repository.Get().FirstOrDefaultAsync(a => a.Email == login && a.Password == password);
            if (account != null)
            {
                authResult.Token = GenerateJwtToken(account.Email, account.Id.ToString());
                authResult.IsSuccess = true;
            }
            else
            {
                authResult.Token = null;
                authResult.IsSuccess = false;
            }
            return authResult;
        }

        private string GenerateJwtToken(string login, string guid)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                new Claim(ClaimsIdentity.DefaultIssuer, guid)
            };

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
