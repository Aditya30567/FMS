using CityService.Interface;
using CityService.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CityService.Process
{
    public class CityProcess
    {

        private readonly ICity _repo; 
        public CityProcess(ICity repo) => _repo = repo;

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await _repo.GetAllData();
        }
        public async Task<bool> AddCity(City city)
        {
            if(_repo.GetCityById(city.CityId) != null)
            {
                throw new KeyNotFoundException("This key already exists."); 
            }
            return await  _repo.AddCity(city);
        }
        public async Task<City> UpdateCity(int cityId,City city)
        {
            if (_repo.GetCityById(cityId) == null)
            {
                throw new ArgumentNullException(nameof(city));
            }
            return await _repo.UpdateCity(cityId,city);
        }
        public async Task<City> GetCityById(int cityId)
        {
            return await _repo.GetCityById(cityId);
        }
        
        public async Task<bool> DeleteCity(int cityId)
        {
            return await _repo.DeleteCity(cityId);
        }
       public async Task<City> UpdateAirportCharge(int cityId, int airportCharge)
        {
            return await _repo.UpdateAirportCharge(cityId, airportCharge);
        }
       
       
    }
}
