using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoList.Infrastrucutre.DataBaseContext;

namespace TodoList.Infrastrucutre.JwtService
{
    public class JwtService
    {
        private readonly JwtSettings _jwt;
        public JwtService(IOptions<JwtSettings> jwt)
        {
            _jwt = jwt.Value;
        }

        public string GenerateJwt(User user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret!)); //get secret key in bytes it use to encrypt and decrypt token
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //describe token signture security key and hashing algorithms
            var userClaims = new List<Claim>()  // claims information about user 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.FirstName!),
                new Claim(ClaimTypes.Surname,user.SecondName!),
            };
            var tokenDecribtor = new SecurityTokenDescriptor()  // describe jwt token claims, expiredate ,audiance ,issuer ,token signature
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddHours(_jwt.ExpiryMinutes),
                Audience = _jwt.Audience,
                Issuer = _jwt.Issuer,
                SigningCredentials = signingCredentials,
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwt = jwtTokenHandler.CreateToken(tokenDecribtor); //create jwt token and pass SignInCredentiels parameter
            return jwtTokenHandler.WriteToken(jwt); //jwt will be serielize in jwe or jws  formate


        }
    }
}
