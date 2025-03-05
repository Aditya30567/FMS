using System;
using System.Collections.Generic;

namespace CityService.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityCode { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public string State { get; set; } = null!;

    public decimal AirportCharge { get; set; }

    public bool Status { get; set; } 
}
