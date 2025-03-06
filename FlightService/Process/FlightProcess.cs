using FlightService.Repository;
using FMSLibrary.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace FlightService.Process
{
    public class FlightProcess
    {
        IFlight repo;
        public FlightProcess(IFlight repo)
        {
            this.repo = repo;
        }
        public async Task<bool> AddFlight(Flight flight)
        {
            return await repo.AddFlight(flight);
        }
        public async Task<IEnumerable<Flight>> GetFlightByDepartureDate(string from, string to, DateOnly dateOfTravel)
        {
            var res= await repo.GetFlightByDepartureDate(from, to, dateOfTravel);
            if (!res.Any()) throw new Exception("Flight not found");
            return res;
        }
        public async Task<bool> RemoveFlight(int flightId)
        {
            return await repo.RemoveFlight(flightId);
        }
        public async Task<Flight> UpdateAvailableSeat(int flightId, int availableSeat)
        {
            return await repo.UpdateAvailableSeat(flightId, availableSeat);
        }
        public async Task<Flight> UpdateFlight(int flightId, Flight flight)
        {
            return await repo.UpdateFlight(flightId, flight);
        }
        public async Task<Flight> GetFlightById(int flightId)
        {
            if (flightId < 0) throw new Exception("Id can't be less than zero");
            return await repo.GetFlightById(flightId);
        }
    }
}
