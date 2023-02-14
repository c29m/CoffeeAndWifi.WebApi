using CoffeeAndWifi.WebApi.Models.Domains;
using CoffeeAndWifi.WebApi.Models.Dtos;

namespace CoffeeAndWifi.WebApi.Models.Converters
{
    public static class UserConverter
    {
        public static UserDto ToDto(this User model)
        {
            return new UserDto
            {
                //Id = model.Id,
                Username = model.Username,
                Password = model.PasswordHash,
                RoleId = model.RoleId,
            };
        }

        public static User ToDao(this UserDto model)
        {
            return new User
            {
                //Id = model.Id,
                Username = model.Username,
                PasswordHash = model.Password,
                RoleId = model.RoleId,
            };

        }
    }
}
