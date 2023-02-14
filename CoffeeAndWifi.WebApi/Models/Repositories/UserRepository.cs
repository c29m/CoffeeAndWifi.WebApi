using CoffeeAndWifi.WebApi.Models.Domains;
using CoffeeAndWifi.WebApi.Models.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeAndWifi.WebApi.Models.Repositories
{
    public class UserRepository
    {
        private readonly CoffeeWifiContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(CoffeeWifiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Verify(UserDto userDto)
        {
            User existingUser;
            try
            {
                existingUser = _context.Users.First(x => x.Username == userDto.Username && x.RoleId == userDto.RoleId)!;
            }
            catch { throw new Exception("User not found"); }
                          
            if(!BCrypt.Net.BCrypt.Verify( userDto.Password, existingUser.PasswordHash))
                throw new Exception("Password is incorrect");

        }

        public void Delete(int id)
        {
            var userToDelete = _context.Users.First(x => x.Id == id);
            _context.Users.Remove(userToDelete);
        }


        public string CreateToken(User user, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }
    }
}
