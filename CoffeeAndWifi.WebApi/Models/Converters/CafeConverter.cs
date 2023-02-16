using CoffeeAndWifi.WebApi.Models.Domains;
using CoffeeAndWifi.WebApi.Models.Dtos;

namespace CoffeeAndWifi.WebApi.Models.Converters
{
    public static class CafeConverter
    {
        public static CafeDto ToDto(this Cafe model)
        {
            return new CafeDto
            {
                Id = model.Id,
                CafeName = model.CafeName,
                OpeningTime = model.OpeningTime,
                ClosingTime = model.ClosingTime,
                CoffeeRatingId = model.CoffeeRatingId,
                WifiStrengthRatingId = model.WifiStrengthRatingId,
                PowerSocketsRatingId = model.PowerSocketsRatingId,
                LocationUrl = model.LocationUrl,
                Street = model.Street,
                City = model.City,
                PostalCode = model.PostalCode,
                CoffeeRating = model.CoffeeRating.Rating!.ToString(),
                WifiRating = model.WifiStrengthRating.Rating!.ToString(),
                PwrSocketRating = model.PowerSocketsRating.Rating!.ToString()

            };
        }

        public static IEnumerable<CafeDto> ToDtos(this IEnumerable<Cafe> model)
        {
            if (model == null)
                return Enumerable.Empty<CafeDto>();
            return model.Select(x => x.ToDto());
        }

        public static Cafe ToDao(this CafeDto model)
        {
            return new Cafe
            {
                Id = model.Id,
                CafeName = model.CafeName,
                OpeningTime = model.OpeningTime,
                ClosingTime = model.ClosingTime,
                CoffeeRatingId = model.CoffeeRatingId,
                WifiStrengthRatingId = model.WifiStrengthRatingId,
                PowerSocketsRatingId = model.PowerSocketsRatingId,
                LocationUrl = model.LocationUrl,
                Street = model.Street,
                City = model.City,
                PostalCode = model.PostalCode,
            };

        }
    }
}
