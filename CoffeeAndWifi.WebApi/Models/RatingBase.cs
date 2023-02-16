using CoffeeAndWifi.WebApi.Models.Domains;

namespace CoffeeAndWifi.WebApi.Models
{
    public class RatingBase
    {   
        public int Id { get; set; }
        public string? Rating { get; set; }
        public virtual ICollection<Cafe>? Cafes { get; }// = new List<Cafe>();
    }
}
