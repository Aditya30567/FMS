using FareService.Repostiory;
using FMSLibrary.Models;

namespace FareService.Process
{
    public class FareProcess
    {
        private readonly IFare repo;
        public FareProcess(IFare repo)
        {
            this.repo=repo;
        }
        public async Task<bool> AddFare(Fare fare)
        {
            return await repo.AddFare(fare);
        }
        public async Task<Fare> UpdateFare(int fareId, Fare fare)
        {
            return await repo.UpdateFare(fareId, fare);
        }
        public async Task<Fare> UpdateConveninceFee(int fareId, int conveninceFees)
        {
            return await repo.UpdateConveninceFee(fareId, conveninceFees);
        }
        public async Task<Fare> GetFareByFlightById(int flightId)
        {
            return await repo.GetFareByFlightById(flightId);
        }
        public async Task<Fare> GetFareById(int fareId)
        {
            return await repo.GetFareById(fareId);
        }
    }
}
