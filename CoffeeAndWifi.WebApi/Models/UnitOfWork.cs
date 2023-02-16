using CoffeeAndWifi.WebApi.Models.Domains;
using CoffeeAndWifi.WebApi.Models.Repositories;

namespace CoffeeAndWifi.WebApi.Models
{
    public class UnitOfWork
    {
        private readonly CoffeeWifiContext _context;
        private readonly IConfiguration _configuration;

        public UnitOfWork(CoffeeWifiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            Cafe = new CafeRepository(context);
            User = new UserRepository(context, configuration);
            Role = new RoleRepository(context);
        }

        public CafeRepository Cafe { get; }
        public UserRepository User { get; }
        public RoleRepository Role { get; }


        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
