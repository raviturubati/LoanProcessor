
using LoanProcessor.Auth.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcessor.Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _key;

        public UserRepository(string secret)
        {
            this._key = secret;
        }
        public async Task<User> Authenticate(string userName, string password)
        {
            var temUser =  users.Where(x => x.UserName.Equals(userName)).FirstOrDefault();
            if (temUser == null || !temUser.UserName.Equals(userName))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { 
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, temUser.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token =  tokenHandler.CreateToken(tokenDescriptor);
            temUser.Token =  tokenHandler.WriteToken(token);
            return temUser;
        }

       

        List<User> users = new List<User>()
        {
            new User
            {
                UserName = "Ravi1",
                Password = "password",
                Role = "admin"
            },
            new User
            {
                UserName = "Prakash",
                Password = "password",
                Role = "user"
            }
        };
    }
   
}
