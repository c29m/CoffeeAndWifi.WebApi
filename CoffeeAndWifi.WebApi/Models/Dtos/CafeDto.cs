using CoffeeAndWifi.WebApi.Models.Domains;

namespace CoffeeAndWifi.WebApi.Models.Dtos
{
    public class CafeDto
    {
        public int Id { get; set; }

        public string CafeName { get; set; } = null!;

        public string OpeningTime { get; set; } = null!;

        public string ClosingTime { get; set; } = null!;

        public int CoffeeRatingId { get; set; }

        public int WifiStrengthRatingId { get; set; }

        public int PowerSocketsRatingId { get; set; }

        public string LocationUrl { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

        public string? CoffeeRating { get; set; } = null;
        public string? WifiRating { get; set; } = null;
        public string? PwrSocketRating { get; set; } = null;

    }
}
