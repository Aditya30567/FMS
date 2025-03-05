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
    }
}
