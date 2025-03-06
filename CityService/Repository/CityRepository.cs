using CityService.Interface;
using FMSLibrary.Models;
using FMSLibrary.UserDefinedException;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CityService.Repository
{
    public class CityRepository : ICity

    {
        CityServiceDbContext dbContext;
        public CityRepository(CityServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<City>> GetAllData()
        {
            var res = await dbContext.Cities.ToListAsync();
            return res;
        }
        public async Task<City> GetCityById(int cityId)
        {
            try
            {
                var res =await dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
                if (res == null) throw new IdNotFoundException("Not found with this id");
                return res;
            }
            catch (TaskCanceledException ex)
            {
                // Log the exception details
                Console.WriteLine($"Task was canceled: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> AddCity(City city)
        {
            try
            {
                if (city == null) throw new ArgumentException("Data cant be null");
                var res = dbContext.Cities.FirstOrDefault(c => c.CityId == city.CityId);
                if (res != null) throw new Exception("Id already Exist");
                dbContext.Cities.Add(city);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCity(int cityId)
        {
            try
            {
                var res = await dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
                if (res == null) throw new IdNotFoundException("Id not found");
                //dbContext.Cities.Remove(res);
                res.Status = false;
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<City> UpdateAirportCharge(int cityId, int airportCharge)
        {
            try
            {
                if (airportCharge == 0) throw new ArgumentNullException("Not found");
                var res = await dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
                if (res == null) throw new IdNotFoundException("Id not found");
                res.AirportCharge = airportCharge;
                await dbContext.SaveChangesAsync();
                return res;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<City> UpdateCity(int cityId, City city)
        {

            try
            {
                var res = await dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
                if (res == null) throw new IdNotFoundException("Id not found");
                res.State = city.State;
                res.CityCode = city.CityCode;
                res.CityName = city.CityName;
                await dbContext.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
