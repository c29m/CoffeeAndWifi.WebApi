using System;
using System.Collections.Generic;

namespace CoffeeAndWifi.WebApi.Models.Domains;

public partial class CoffeeRating
{
    public int Id { get; set; }

    public string Rating { get; set; } = null!;

    public virtual ICollection<Cafe> Cafes { get; } = new List<Cafe>();
}
