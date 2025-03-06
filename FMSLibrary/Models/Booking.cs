using System;
using System.Collections.Generic;

namespace FMSLibrary.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int FlightId { get; set; }

    public int FareId { get; set; }

    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
