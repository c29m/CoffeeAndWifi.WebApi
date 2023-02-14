using CoffeeAndWifi.WebApi.Models.Domains;
using CoffeeAndWifi.WebApi.Models.Repositories;

namespace CoffeeAndWifi.WebApi.Models
{
    public class UnitOfWork
    {
        private readonly CoffeeWifiContext _context;

        public UnitOfWork(CoffeeWifiContext context)
        {
            _context = context;
            Cafe = new CafeRepository(context);
            User = new UserRepository(context);
        }

        public CafeRepository Cafe { get; }
        public UserRepository User { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
