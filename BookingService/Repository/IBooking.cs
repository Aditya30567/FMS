using FMSLibrary.Models;

namespace BookingService.Repository
{
    public interface IBooking
    {
        Task<Booking> AddBooking(Booking booking);
        Task<Booking> GetBookingById(int bookingId);
        Task<bool> UpdateCheckInStatus(Booking booking);

        Task<IEnumerable<Passenger>> GetAllPassengerByBookingId(int bookingId);
        //Task<bool> AddPassenger(Passenger passenger);
        
    }
}
