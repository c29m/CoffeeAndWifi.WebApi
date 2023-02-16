using CoffeeAndWifi.WebApi.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace CoffeeAndWifi.WebApi.Models.Repositories
{
    public class CafeRepository
    {
        private readonly CoffeeWifiContext _context;

        public CafeRepository(CoffeeWifiContext context)
        {
            _context = context;
        }

        //public IEnumerable<Cafe> Get()
        //{
        //    var cafes = _context.Cafes;
        //    if (cafes.Any())
        //    {
        //        foreach (var cafe in cafes)
        //        {
        //            cafe.CoffeeRating = GetCoffeeRating(cafe.CoffeeRatingId);
        //            cafe.WifiStrengthRating = GetWifiRating(cafe.WifiStrengthRatingId);
        //            cafe.PowerSocketsRating = GetPwrSocketRating(cafe.PowerSocketsRatingId);
        //        }
        //        return cafes;
        //    }
        //    throw new Exception("Cafes not found");
        //}
                

        public Cafe Get(int id)
        {
            var cafe = _context.Cafes.FirstOrDefault(x => x.Id == id);
            
            if (cafe is not null)
            {
                cafe.CoffeeRating = GetCoffeeRating(cafe.CoffeeRatingId);
                cafe.WifiStrengthRating = GetWifiRating(cafe.WifiStrengthRatingId);
                cafe.PowerSocketsRating = GetPwrSocketRating(cafe.PowerSocketsRatingId);
                return cafe;
            }
            throw new Exception("Cafe not found");
        }

        public IEnumerable<Cafe> Get(string city)
        {
            var cafesInCity = _context.Cafes.Where(x => x.City.ToLower() == city.ToLower());
            //var cafesInCity = _context.Cafes;
            if (cafesInCity.Count() > 1)
            {
                foreach (var cafe in cafesInCity)
                {
                    cafe.CoffeeRating = GetCoffeeRating(cafe.CoffeeRatingId);
                    cafe.WifiStrengthRating = GetWifiRating(cafe.WifiStrengthRatingId);
                    cafe.PowerSocketsRating = GetPwrSocketRating(cafe.PowerSocketsRatingId);
                }
                return cafesInCity;
            }
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

        public IEnumerable<Cafe> Get()
        {
            var cafes = _context.Cafes.ToList();
            if (cafes.Count > 1)
            {
                foreach (var cafe in cafes)
                {
                    cafe.CoffeeRating = GetCoffeeRating(cafe.CoffeeRatingId);
                    cafe.WifiStrengthRating = GetWifiRating(cafe.WifiStrengthRatingId);
                    cafe.PowerSocketsRating = GetPwrSocketRating(cafe.PowerSocketsRatingId); 
                }
                return cafes;
            }
            throw new Exception("Cafes not found");
        }


        public void Delete(int id)
        {
            var cafeToDelete = _context.Cafes.First(x => x.Id == id);
            _context.Cafes.Remove(cafeToDelete);
        }

        private PwrSocketsRating GetPwrSocketRating(int powerSocketsRatingId)
           => _context.PwrSocketsRatings.Single(x => x.Id == powerSocketsRatingId)!;

        private WifiRating GetWifiRating(int wifiStrengthRatingId)
            => _context.WifiRatings.Single(x => x.Id == wifiStrengthRatingId)!;

        private CoffeeRating GetCoffeeRating(int coffeeRatingId)
            => _context.CoffeeRatings.Single(x => x.Id == coffeeRatingId)!;

    }
}
