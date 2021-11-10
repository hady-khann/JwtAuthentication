using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestJWT.Interfaces;

namespace TestJWT.Infrastructure
{
    public class JwtAuthenticationManager : IJWTAuthentication
    {

        private readonly string key;

        public readonly IDictionary<string, string> UsersDicList = new Dictionary<string, string>
        {
            { "Test1" , "Password1"},
            { "Test2" , "Password2"},
            { "Test3" , "Password3"},
            { "Test4" , "Password4"},
            { "Test5" , "Password5"},
        };

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string UserName, string Password)
        {
            if (UsersDicList.Any(x => x.Key == UserName && x.Value == Password))
            {

                var tokenkey = Encoding.ASCII.GetBytes(key);
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, UserName)
                    }),

                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials
                    (
                        new SymmetricSecurityKey(tokenkey),
                        SecurityAlgorithms.HmacSha256Signature
                        )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return null;
            }
        }
    }
}
