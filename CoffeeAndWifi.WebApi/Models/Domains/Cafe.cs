using System;
using System.Collections.Generic;

namespace CoffeeAndWifi.WebApi.Models.Domains;

public partial class Cafe
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

    public virtual CoffeeRating CoffeeRating { get; set; } = null!;

    public virtual PwrSocketsRating PowerSocketsRating { get; set; } = null!;

    public virtual WifiRating WifiStrengthRating { get; set; } = null!;
}
