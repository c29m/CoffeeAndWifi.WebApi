using System;
using System.Collections.Generic;

namespace CoffeeAndWifi.WebApi.Models.Domains;

public partial class Role
{
    public int Id { get; set; }

    public string UserRole { get; set; } = null!;
}
