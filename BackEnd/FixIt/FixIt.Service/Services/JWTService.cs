using FixIt.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Services
{
    public class JWTService
    {
        private readonly IConfiguration config;

        public JWTService(IConfiguration config)
        {
            this.config = config;
        }
        public string GenerateToken(User user)
        {

            #region Claims
            List<Claim> userData = new List<Claim>();
            userData.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userData.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            userData.Add(new Claim(ClaimTypes.Name, user.FullName));
            userData.Add(new Claim(ClaimTypes.Email, user.Email));
            userData.Add(new Claim(ClaimTypes.Role, user.Role));
            #endregion


            #region Secret key
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:Key"]));
            #endregion

            #region Create Token
            //signingCre
            var signCre = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            //Create Token
            var Token = new JwtSecurityToken(
                audience: config["Jwt:AudienceIP"],
                issuer: config["Jwt:IssuerIP"],
                claims: userData,
                signingCredentials: signCre,
                expires: DateTime.Now.AddDays(1)
                );

            var StringToken = new JwtSecurityTokenHandler().WriteToken(Token);
            #endregion

            return StringToken.ToString();
        }
    }
}
