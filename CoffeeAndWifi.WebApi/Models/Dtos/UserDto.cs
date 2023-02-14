namespace CoffeeAndWifi.WebApi.Models.Dtos
{
    public class UserDto
    {

        public required string Username { get; set; }

        public required string Password { get; set; }

        public int RoleId { get; set; }
    }

}
