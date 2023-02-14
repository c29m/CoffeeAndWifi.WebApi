using CoffeeAndWifi.WebApi.Models.Domains;
using System.Security.Claims;

namespace CoffeeAndWifi.WebApi.Models.Repositories
{
    public class RoleRepository
    {
        private readonly CoffeeWifiContext _context;

        public RoleRepository(CoffeeWifiContext context )
        {
            _context = context;
        }

        public string GetRole(int roleId)
        {
            return _context.Roles.Single(x => x.Id == roleId).UserRole;
        }
    }
}
