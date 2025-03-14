using FlightService.Repository;
using FMSLibrary.Models;
using FMSLibrary.UserDefinedException;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Newtonsoft.Json;

namespace FlightService.Process
{
    public class FlightProcess
    {
         IFlight repo;
        private readonly HttpClient http;
        public FlightProcess(IFlight repo, HttpClient http)
        {
            this.repo = repo;
            this.http = http;
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
        public async Task<SearchModel> GetFlightByStation(string from, string to, DateOnly dateOfTravel)
        {
            SearchModel model = new SearchModel();
            var res = await repo.GetFlightByDepartureDate(from, to, dateOfTravel);
            if (!res.Any()) throw new IdNotFoundException("Flight not found");
            var response = await http.GetAsync($"http://localhost:7003/api/Fares/allFare");
            if(!response.IsSuccessStatusCode) throw new Exception("Find error during fetching fare");
            var data = await response.Content.ReadAsStringAsync();
            var mainData=JsonConvert.DeserializeObject<List<Fare>>(data);
            var flihgtData=res.Select(c=>c.FlightId).ToList();
            var resultData =mainData?.Where(c => flihgtData.Contains(c.FlightId)).ToList() ?? new List<Fare>();

            model.Flights = res.ToList();
            model.Fares = resultData;
            return model;


        }
    }
}
