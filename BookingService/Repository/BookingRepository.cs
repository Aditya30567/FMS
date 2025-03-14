using FMSLibrary.Models;
using FMSLibrary.UserDefinedException;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repository
{
    public class BookingRepository:IBooking
    {
        private readonly BookingDbContext context;
        public BookingRepository(BookingDbContext context)
        {
            this.context = context;
        }
        public async Task<Booking> AddBooking(Booking booking)
        {
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();
            return booking;
        }
        public async Task<Booking> GetBookingById(int bookingId)
        {
            //if (bookingId < 0) throw new Exception("Invalid booking Id type");
            var res = await context.Bookings.Include(c=>c.Passengers).FirstOrDefaultAsync(c => c.BookingId == bookingId);
            if (res == null) throw new IdNotFoundException("Booking Id not found");
            return res;

        }
        public async Task<bool> UpdateCheckInStatus(Booking booking)
        {
            context.Bookings.Update(booking);
            return await context.SaveChangesAsync()>0;
        }

        public async Task<IEnumerable<Passenger>> GetAllPassengerByBookingId(int bookingId)
        {
            return await context.Passengers.Where(c=>c.BookingId==bookingId).ToListAsync();
        }
        //public async Task<bool> AddPassenger(Passenger passenger)
        //{
        //    context.Passengers.Add(passenger);
        //    return await context.SaveChangesAsync()>0;
        //} 
    }
}
