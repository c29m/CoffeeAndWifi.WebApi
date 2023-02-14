using CoffeeAndWifi.WebApi.Models.Domains;

namespace CoffeeAndWifi.WebApi.Models.Repositories
{
    public class CafeRepository
    {
        private readonly CoffeeWifiContext _context;

        public CafeRepository(CoffeeWifiContext context)
        {
            _context = context;
        }

        public IEnumerable<Cafe> Get() 
        {
            var cafes = _context.Cafes;
            if (cafes is not null)
                return cafes;
            throw new Exception("Cafes not found");
        }

        public Cafe Get(int id)
        {
            var cafe = _context.Cafes.FirstOrDefault(x => x.Id == id);
            if (cafe is not null)
                return cafe;
            throw new Exception("Cafe not found");
        }

        public IEnumerable<Cafe> Get(string city)
        {
            var cafesInCity = _context.Cafes.Where(x => x.City.ToLower() == city.ToLower());
            if (cafesInCity.Any())
                return cafesInCity;
            throw new Exception($"Cafes not found in {city}");
        }

        public void Add(Cafe cafe)
        {
            _context.Cafes.Add(cafe);
        }

        public void Update(Cafe cafe)
        {
            var cafeToUpdate = _context.Cafes.First(x => x.Id == cafe.Id);

            cafeToUpdate.CafeName = cafe.CafeName;
            cafeToUpdate.LocationUrl = cafe.LocationUrl;
            cafeToUpdate.OpeningTime = cafe.OpeningTime;
            cafeToUpdate.ClosingTime = cafe.ClosingTime;
            cafeToUpdate.CoffeeRatingId = cafe.CoffeeRatingId;
            cafeToUpdate.WifiStrengthRatingId = cafe.WifiStrengthRatingId;
            cafeToUpdate.PowerSocketsRatingId = cafe.PowerSocketsRatingId;
            cafeToUpdate.Street = cafe.Street;
            cafeToUpdate.City = cafe.City;
            cafeToUpdate.PostalCode = cafe.PostalCode;

        }

        public void Delete(int id)
        {
            var cafeToDelete = _context.Cafes.First(x => x.Id == id);

            _context.Cafes.Remove(cafeToDelete);

        }

    }
}
