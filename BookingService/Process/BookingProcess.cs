using BookingService.Repository;
using FMSLibrary.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BookingService.Process
{
    public class BookingProcess
    {
        private readonly IBooking repo;
        private readonly HttpClient http;
        public BookingProcess (IBooking repo,HttpClient http)
        {
            this.repo = repo;
            this.http = http;
        }
        //Add booking function here we dont need to add passenger sepperatly
        public async Task<Booking> AddBooking(Booking booking)
        {
            if (booking == null || booking.UserId == 0 || booking.FlightId == 0)
            {
                throw new Exception("Invalid booking data");
            }
            //foreach (var passenger in booking.Passengers)
            //{
            //    passenger.BookingId = booking.BookingId; // Ensure passengers are linked
            //}

            var res = await repo.AddBooking(booking);
            //if(repo.GetBookingById(booking.BookingId)is not null) throw new Exception("")

            // Validate if data was inserted
            if (res == null || res.BookingId == 0)
            {
                throw new Exception("Data not added");
            }
            return res;

        }
        //Here we got Booking details using booking ID
        public async Task<Booking> GetBookingById(int bookingId)
        {
            return await repo.GetBookingById(bookingId);
        }
        //Here we update status of CheckIn in when user call checkIn servicesy

        public async Task<(bool success, string message,IEnumerable<Passenger>)> UpdateCheckInStatus(int bookingId)
        {
            var booking = await repo.GetBookingById(bookingId);
            if (booking == null) return (false, "Booking ID not found.",new List<Passenger>());
            if (booking.Status == "Checked-In") return (false, "Already Checked-in.", new List<Passenger>());

            // Fetch flight details
            var response = await http.GetAsync($"http://localhost:7002/api/Flights/{booking.FlightId}");
            if (!response.IsSuccessStatusCode) return (false, "Flight not found.", new List<Passenger>());

            var flightData = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var seatStr = flightData.RootElement.GetProperty("availableSeats").ToString();

            if (!int.TryParse(seatStr, out int availableSeats))
                return (false, "Invalid seat count from flight data.", new List<Passenger>());

            // Assign seats to passengers
            var rnd = new Random();
            int assignedSeats = 0;
            if (booking.Passengers.Count == 0) return (false, "No passenger", new List<Passenger>());
            foreach (var passenger in booking.Passengers)
            {
                if (availableSeats > 0)
                {
                    passenger.SeatNo = rnd.Next(10, 22).ToString() + (char)(rnd.Next(65, 75)); // Example: "12C"
                    availableSeats--;
                    assignedSeats++;
                }
                else
                {
                    return (false, "No seats available.", new List<Passenger>());
                }
            }

            // Update Flight's available seats
            //var updateFlightData = new { AvailableSeats = availableSeats };
            //var jsonContent = new StringContent(JsonSerializer.Serialize(updateFlightData), Encoding.UTF8, "application/json");

            var flightUpdateResponse = await http.PutAsync($"http://localhost:7002/api/Flights/update/{booking.FlightId}?AvailableSeat={availableSeats}", null);
            if (!flightUpdateResponse.IsSuccessStatusCode)
                return (false, "Failed to update flight seat count.", new List<Passenger>());

            // Update Booking Status
            booking.Status = "Checked-In";
            bool updated = await repo.UpdateCheckInStatus(booking);
            var res=await repo.GetAllPassengerByBookingId(booking.BookingId);

            return updated ? (true, "Check-in successful.",res) : (false, "Failed to update booking status.", new List<Passenger>());
        }

        public async Task<IEnumerable<Passenger>> GetAllPassenger(int bookingId)
        {
            return await repo.GetAllPassengerByBookingId(bookingId);
        }
        //public async Task<bool> AddPassenger(Passenger passenger)
        //{
        //    return await repo.AddPassenger(passenger);
        //}
        public async Task<Tuple<Flight,List<Passenger>,Booking>> GetAllBookingDetailsWithFlight(int bookingId)
        {
            var booking = await repo.GetBookingById(bookingId);
            //if(booking is null) throw new Exception()
            Flight? flightData = null;
            //if (booking == null) throw new Exception("Booking is not found");
            var response = await http.GetAsync($"http://localhost:7002/api/Flights/{booking.FlightId}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Json String", jsonString);
                try
                {
                    flightData = JsonConvert.DeserializeObject<Flight>(jsonString);
                }
                catch (Newtonsoft.Json.JsonException ex)
                {
                    Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                }
            }
            if (flightData is null) throw new Exception("No flight associated with this booking number");
            //Console.WriteLine(response);
            var passengerName = booking.Passengers.ToList();
            return Tuple.Create(flightData, passengerName,booking);
             
        }
       
            
            
        
    }
}
