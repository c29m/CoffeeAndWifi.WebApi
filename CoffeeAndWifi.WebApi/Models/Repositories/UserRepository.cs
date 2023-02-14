using CoffeeAndWifi.WebApi.Models.Domains;
using CoffeeAndWifi.WebApi.Models.Dtos;

namespace CoffeeAndWifi.WebApi.Models.Repositories
{
    public class UserRepository
    {
        private readonly CoffeeWifiContext _context;

        public UserRepository(CoffeeWifiContext context)
        {
            _context = context;
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
    }
}
