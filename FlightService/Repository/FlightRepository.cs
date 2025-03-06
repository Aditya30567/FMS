using FMSLibrary.Models;
using FMSLibrary.UserDefinedException;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Repository
{
    public class FlightRepository : IFlight
    {
        FlightDbContext dbContext;
        public FlightRepository(FlightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddFlight(Flight flight)
        {
            try
            {
                if (flight == null) throw new ArgumentException($"{flight} Data can't be null");
                var res = await dbContext.Flights.AnyAsync(c => c.FlightId == flight.FlightId &&c.IsActive);
                if (res) throw new IdNotFoundException("Id already Exist");
                dbContext.Flights.Add(flight);
                await dbContext.SaveChangesAsync();
                return true;
            }catch(Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Flight>> GetFlightByDepartureDate(string from, string to, DateOnly dateOfTravel)
        {
            try
            {
                if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to)) throw new ArgumentException("Data can't be null");
                var res=await dbContext.Flights
                    .Where(c=>c.FromCity==from&&c.ToCity==to&&c.DepartureDate==dateOfTravel&&c.IsActive)
                    .ToListAsync();
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> RemoveFlight(int flightId)
        {
            try
            {
                if (flightId < 0) throw new ArgumentOutOfRangeException("Id can't be < 0");
                var res = await dbContext.Flights
                    .FirstOrDefaultAsync(c => c.FlightId == flightId&&c.IsActive);
                if (res is null) throw new IdNotFoundException("Id not found");
                //dbContext.Flights.Remove(res);
                res.IsActive = false;
                await dbContext.SaveChangesAsync();
                return true;
            }catch(Exception) { throw; }
        }

        public async Task<Flight> UpdateAvailableSeat(int flightId, int availableSeat)
        {
            try
            {
                if (flightId < 0 || availableSeat < 0) throw new ArgumentException("Data can't be null");
                var res=await dbContext.Flights.FirstOrDefaultAsync(c=>c.FlightId== flightId&&c.IsActive);
                if (res is null) throw new IdNotFoundException("Id not found");
                 res.AvailableSeats=availableSeat;
                await dbContext.SaveChangesAsync();
                return res;
            }catch(Exception) { throw; }
        }

        public async Task<Flight> UpdateFlight(int flightId, Flight flight)
        {
            try
            { 

                if (flightId != flight.FlightId) throw new Exception("Id not match");
                if (flight is null) throw new ArgumentException("Data can't be null");
                var res=await dbContext.Flights.FirstOrDefaultAsync(c=>c.FlightId==flightId&&c.IsActive);
                if(res is null) throw new IdNotFoundException("Id not found");
                res.FromCity = flight.FromCity;
                res.ToCity = flight.ToCity;
                res.FlightNo = flight.FlightNo;
                res.DepartureDate = flight.DepartureDate;
                res.DepartureTime = flight.DepartureTime;
                res.ArrivalTime = res.ArrivalTime;
                await dbContext.SaveChangesAsync();
                return res;
            }catch(Exception) { throw; }
        }
        public async Task<Flight> GetFlightById(int flightId)
        {
            try
            {
                var res = await dbContext.Flights.FirstOrDefaultAsync(c => c.FlightId == flightId&&c.IsActive);
                if (res is null) throw new IdNotFoundException("Id not found");
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
