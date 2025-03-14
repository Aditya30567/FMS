using FMSLibrary.Models;

namespace FareService.Repostiory
{
    public interface IFare
    {
        Task<bool> AddFare(Fare fare);
        
        Task<Fare> UpdateFare(int fareId, Fare fare);
        Task<Fare> UpdateConveninceFee(int fareId, int conveninceFees);
        Task<Fare> GetFareById(int fareId);
        Task<Fare> GetFareByFlightById(int flightId);
        Task<IEnumerable<Fare>> GetAllFare();
    }
}
