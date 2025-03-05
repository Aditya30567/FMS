using CityService.Models;

namespace CityService.Interface
{
    public interface ICity
    {
        Task<bool> AddCity(City city);
        Task<City> UpdateCity(int cityId, City city);
        Task<bool>  DeleteCity(int cityId);
        Task<City> UpdateAirportCharge(int cityId, int airportCharge);
        Task<IEnumerable<City>> GetAllData();
        Task<City> GetCityById(int cityId);
    }
}
