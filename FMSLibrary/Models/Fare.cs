using System;
using System.Collections.Generic;

namespace FMSLibrary.Models;

public partial class Fare
{
    public int FareId { get; set; }

    public int FlightId { get; set; }

    public decimal BasePrice { get; set; }

    public decimal ConvenienceFee { get; set; }
}
