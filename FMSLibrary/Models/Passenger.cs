using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FMSLibrary.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public int BookingId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string SeatNo { get; set; } = null!;

    public bool IsActive { get; set; }

    [JsonIgnore]
    public virtual Booking? Booking { get; set; } 
}
